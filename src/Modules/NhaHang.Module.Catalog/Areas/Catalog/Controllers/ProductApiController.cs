using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using NhaHang.Infrastructure.Commons;
using NhaHang.Infrastructure.Data;
using NhaHang.Infrastructure.Helpers;
using NhaHang.Infrastructure.Web.SmartTable;
using NhaHang.Module.Catalog.Areas.Catalog.ViewModels;
using NhaHang.Module.Catalog.Models;
using NhaHang.Module.Catalog.Services;
using NhaHang.Module.Core.Extensions;
using NhaHang.Module.Core.Models;
using NhaHang.Module.Core.Services;

namespace NhaHang.Module.Catalog.Areas.Catalog.Controllers
{
    [Area("Catalog")]
    //[Authorize(Roles = "admin, vendor")]
    [Route("api/products")]
    [ApiController]
    public class ProductApiController : Controller
    {
        private readonly IMediaService _mediaService;
        private readonly IRepository<ProductCategory> _productCategoryRepository;
        private readonly IRepository<ProductLink> _productLinkRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IProductService _productService;
        private readonly IRepository<ProductMedia> _productMediaRepository;
        private readonly IWorkContext _workContext;
        private readonly ITrackingService _trackingService;

        public ProductApiController(
            IRepository<Product> productRepository,
            IMediaService mediaService,
            IProductService productService,
            IRepository<ProductLink> productLinkRepository,
            IRepository<ProductCategory> productCategoryRepository,
            IRepository<ProductMedia> productMediaRepository,
            IWorkContext workContext, ITrackingService _trackingService)
        {
            _productRepository = productRepository;
            _mediaService = mediaService;
            _productService = productService;
            _productLinkRepository = productLinkRepository;
            _productCategoryRepository = productCategoryRepository;
            _productMediaRepository = productMediaRepository;
            _workContext = workContext;
            this._trackingService = _trackingService;
        }

        [HttpGet("quick-search")]
        public async Task<IActionResult> QuickSearch(string name)
        {
            var query = _productRepository.Query()
                .Where(x => !x.IsDeleted && !x.HasOptions && x.IsAllowToOrder);

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }

            var products = await query.Take(5).Select(x => new
            {
                x.Id,
                x.Name
            }).ToListAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var product = _productRepository.Query()
                .Include(x => x.ThumbnailImage)
                .Include(x => x.Medias).ThenInclude(m => m.Media)
                .Include(x => x.ProductLinks).ThenInclude(p => p.LinkedProduct).ThenInclude(m => m.ThumbnailImage)
                .Include(x => x.Categories)
                .FirstOrDefault(x => x.Id == id);

            var currentUser = await _workContext.GetCurrentUser();
            if (!User.IsInRole("admin") && product.VendorId != currentUser.VendorId)
            {
                return BadRequest(new { error = "You don't have permission to manage this product" });
            }

            var productVm = new ProductVm
            {
                Id = product.Id,
                Name = product.Name,
                Slug = product.Slug,
                MetaTitle = product.MetaTitle,
                MetaKeywords = product.MetaKeywords,
                MetaDescription = product.MetaDescription,
                Sku = product.Sku,
                Gtin = product.Gtin,
                ShortDescription = product.ShortDescription,
                Description = product.Description,
                Specification = product.Specification,
                OldPrice = product.OldPrice,
                Price = product.Price,
                SpecialPrice = product.SpecialPrice,
                SpecialPriceStart = product.SpecialPriceStart,
                SpecialPriceEnd = product.SpecialPriceEnd,
                IsFeatured = product.IsFeatured,
                IsPublished = product.IsPublished,
                IsCallForPricing = product.IsCallForPricing,
                IsAllowToOrder = product.IsAllowToOrder,
                CategoryIds = product.Categories.Select(x => x.CategoryId).ToList(),
                ThumbnailImageUrl = _mediaService.GetThumbnailUrl(product.ThumbnailImage),
                BrandId = product.BrandId,
                TaxClassId = product.TaxClassId,
                StockTrackingIsEnabled = product.StockTrackingIsEnabled
            };

            foreach (var productMedia in product.Medias.Where(x => x.Media.MediaType == MediaType.Image))
            {
                productVm.ProductImages.Add(new ProductMediaVm
                {
                    Id = productMedia.Id,
                    MediaUrl = _mediaService.GetThumbnailUrl(productMedia.Media)
                });
            }

            foreach (var productMedia in product.Medias.Where(x => x.Media.MediaType == MediaType.File))
            {
                productVm.ProductDocuments.Add(new ProductMediaVm
                {
                    Id = productMedia.Id,
                    Caption = productMedia.Media.Caption,
                    MediaUrl = _mediaService.GetMediaUrl(productMedia.Media)
                });
            }
            foreach (var relatedProduct in product.ProductLinks.Where(x => x.LinkType == ProductLinkType.Related).Select(x => x.LinkedProduct).Where(x => !x.IsDeleted).OrderBy(x => x.Id))
            {
                productVm.RelatedProducts.Add(new ProductLinkVm
                {
                    Id = relatedProduct.Id,
                    Name = relatedProduct.Name,
                    IsPublished = relatedProduct.IsPublished
                });
            }

            foreach (var crossSellProduct in product.ProductLinks.Where(x => x.LinkType == ProductLinkType.CrossSell).Select(x => x.LinkedProduct).Where(x => !x.IsDeleted).OrderBy(x => x.Id))
            {
                productVm.CrossSellProducts.Add(new ProductLinkVm
                {
                    Id = crossSellProduct.Id,
                    Name = crossSellProduct.Name,
                    IsPublished = crossSellProduct.IsPublished
                });
            }

            return Json(productVm);
        }

        [HttpPost("grid")]
        public async Task<IActionResult> List([FromBody] SmartTableParam param)
        {
            var query = _productRepository.Query()
                .Include(x => x.Categories)
                    .ThenInclude(x => x.Category)
                .Where(x => !x.IsDeleted);
            var currentUser = await _workContext.GetCurrentUser();
            if (!User.IsInRole("admin"))
            {
                query = query.Where(x => x.VendorId == currentUser.VendorId);
            }

            if (param.Search.PredicateObject != null)
            {
                dynamic search = param.Search.PredicateObject;
                if (search.Name != null)
                {
                    string name = search.Name;
                    query = query.Where(x => x.Name.Contains(name));
                }

                if (search.HasOptions != null)
                {
                    bool hasOptions = search.HasOptions;
                    query = query.Where(x => x.HasOptions == hasOptions);
                }

                if (search.IsVisibleIndividually != null)
                {
                    bool isVisibleIndividually = search.IsVisibleIndividually;
                    query = query.Where(x => x.IsVisibleIndividually == isVisibleIndividually);
                }

                if (search.BrandId != null)
                {
                    long id = search.BrandId;
                    query = query.Where(x => x.Brand.Id == id);
                }

                if (search.CategoryId != null)
                {
                    long categoryId = search.CategoryId;
                    query = query.Where(x => x.Categories.Any(g => g.CategoryId == categoryId));
                }

                if (search.IsPublished != null)
                {
                    bool isPublished = search.IsPublished;
                    query = query.Where(x => x.IsPublished == isPublished);
                }

                if (search.CreatedOn != null)
                {
                    if (search.CreatedOn.before != null)
                    {
                        DateTimeOffset before = search.CreatedOn.before;
                        query = query.Where(x => x.CreatedOn <= before);
                    }

                    if (search.CreatedOn.after != null)
                    {
                        DateTimeOffset after = search.CreatedOn.after;
                        query = query.Where(x => x.CreatedOn >= after);
                    }
                }
            }

            var gridData = query.ToSmartTableResult(
                param,
                x => new ProductListItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    Sku = x.Sku,
                    OldPrice = x.OldPrice,
                    Price = x.Price,
                    HasOptions = x.HasOptions,
                    IsVisibleIndividually = x.IsVisibleIndividually,
                    IsFeatured = x.IsFeatured,
                    IsAllowToOrder = x.IsAllowToOrder,
                    IsCallForPricing = x.IsCallForPricing,
                    StockQuantity = x.StockQuantity,
                    CreatedOn = x.CreatedOn,
                    IsPublished = x.IsPublished,
                    ThumbnailImageUrl = _mediaService.GetThumbnailUrl(x.ThumbnailImage),
                });

            return Json(gridData);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductForm model)
        {
            MapUploadedFile(model);

            CustomJsonResult customJsonResult = new CustomJsonResult();
            try
            {
                if (!ModelState.IsValid)
                {
                    await _trackingService.TrackingError("Post Product", "BadRequest");
                    return Ok("Fail !!!");
                }

                var currentUser = await _workContext.GetCurrentUser();

                var product = new Product
                {
                    Name = model.Product.Name,
                    Slug = model.Product.Slug,
                    MetaTitle = model.Product.MetaTitle,
                    MetaKeywords = model.Product.MetaKeywords,
                    MetaDescription = model.Product.MetaDescription,
                    Sku = model.Product.Sku,
                    Gtin = model.Product.Gtin,
                    ShortDescription = model.Product.ShortDescription,
                    Description = model.Product.Description,
                    Specification = model.Product.Specification,
                    Price = model.Product.Price,
                    OldPrice = model.Product.OldPrice,
                    SpecialPrice = model.Product.SpecialPrice,
                    SpecialPriceStart = model.Product.SpecialPriceStart,
                    SpecialPriceEnd = model.Product.SpecialPriceEnd,
                    IsPublished = model.Product.IsPublished,
                    IsFeatured = model.Product.IsFeatured,
                    IsCallForPricing = model.Product.IsCallForPricing,
                    IsAllowToOrder = model.Product.IsAllowToOrder,
                    BrandId = model.Product.BrandId,
                    TaxClassId = model.Product.TaxClassId,
                    StockTrackingIsEnabled = model.Product.StockTrackingIsEnabled,
                    IsVisibleIndividually = true,
                    CreatedBy = currentUser,
                    LatestUpdatedBy = currentUser
                };

                if (!User.IsInRole("admin"))
                {
                    product.VendorId = currentUser.VendorId;
                }

                foreach (var categoryId in model.Product.CategoryIds)
                {
                    var productCategory = new ProductCategory
                    {
                        CategoryId = categoryId
                    };
                    product.AddCategory(productCategory);
                }

                await SaveProductMedias(model, product);

                MapProductLinkVmToProduct(model, product);

                var productPriceHistory = CreatePriceHistory(currentUser, product);
                product.PriceHistories.Add(productPriceHistory);

                _productService.Create(product);
                customJsonResult.Result = product.Id;
                customJsonResult.StatusCode = 200;
            }
            catch (Exception ex)
            {
                await _trackingService.TrackingError("Post product", ex.Message);
                customJsonResult.StatusCode = 502;
            }
            return Ok(customJsonResult);
        }

        [HttpPost("update-product/{id}")]
        public async Task<IActionResult> Put(long id, ProductForm model)
        {
            MapUploadedFile(model);
            CustomJsonResult customJsonResult = new CustomJsonResult();
            try
            {
                var product = _productRepository.Query()
                .Include(x => x.ThumbnailImage)
                .Include(x => x.Medias).ThenInclude(m => m.Media)
                .Include(x => x.ProductLinks).ThenInclude(x => x.LinkedProduct).ThenInclude(p => p.ThumbnailImage)
                .Include(x => x.Categories)
                .FirstOrDefault(x => x.Id == id);

                if (product == null)
                {
                    return NotFound();
                }

                var currentUser = await _workContext.GetCurrentUser();
                if (!User.IsInRole("admin") && product.VendorId != currentUser.VendorId)
                {
                    return BadRequest(new { error = "You don't have permission to manage this product" });
                }

                var isPriceChanged = false;
                if (product.Price != model.Product.Price ||
                    product.OldPrice != model.Product.OldPrice ||
                    product.SpecialPrice != model.Product.SpecialPrice ||
                    product.SpecialPriceStart != model.Product.SpecialPriceStart ||
                    product.SpecialPriceEnd != model.Product.SpecialPriceEnd)
                {
                    isPriceChanged = true;
                }

                product.Name = model.Product.Name;
                product.Slug = model.Product.Slug;
                product.MetaTitle = model.Product.MetaTitle;
                product.MetaKeywords = model.Product.MetaKeywords;
                product.MetaDescription = model.Product.MetaDescription;
                product.Sku = model.Product.Sku;
                product.Gtin = model.Product.Gtin;
                product.ShortDescription = model.Product.ShortDescription;
                product.Description = model.Product.Description;
                product.Specification = model.Product.Specification;
                product.Price = model.Product.Price;
                product.OldPrice = model.Product.OldPrice;
                product.SpecialPrice = model.Product.SpecialPrice;
                product.SpecialPriceStart = model.Product.SpecialPriceStart;
                product.SpecialPriceEnd = model.Product.SpecialPriceEnd;
                product.BrandId = model.Product.BrandId;
                product.TaxClassId = model.Product.TaxClassId;
                product.IsFeatured = model.Product.IsFeatured;
                product.IsPublished = model.Product.IsPublished;
                product.IsCallForPricing = model.Product.IsCallForPricing;
                product.IsAllowToOrder = model.Product.IsAllowToOrder;
                product.StockTrackingIsEnabled = model.Product.StockTrackingIsEnabled;
                product.LatestUpdatedBy = currentUser;

                if (isPriceChanged)
                {
                    var productPriceHistory = CreatePriceHistory(currentUser, product);
                    product.PriceHistories.Add(productPriceHistory);
                }

                await SaveProductMedias(model, product);

                foreach (var productMediaId in model.Product.DeletedMediaIds)
                {
                    var productMedia = product.Medias.First(x => x.Id == productMediaId);
                    _productMediaRepository.Remove(productMedia);
                    await _mediaService.DeleteMediaAsync(productMedia.Media);
                }

                AddOrDeleteCategories(model, product);
                AddOrDeleteProductLinks(model, product);

                _productService.Update(product);

                customJsonResult.Result = product.Id;
                customJsonResult.StatusCode = 200;
            }
            catch (Exception ex)
            {
                _trackingService.TrackingError("PUT product", ex + string.Empty);
                customJsonResult.StatusCode = 502;
            }
            return Ok(customJsonResult);
        }

        [HttpPost("change-status/{id}")]
        public async Task<IActionResult> ChangeStatus(long id)
        {
            var product = _productRepository.Query().FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var currentUser = await _workContext.GetCurrentUser();
            if (!User.IsInRole("admin") && product.VendorId != currentUser.VendorId)
            {
                return BadRequest(new { error = "You don't have permission to manage this product" });
            }

            product.IsPublished = !product.IsPublished;
            await _productRepository.SaveChangesAsync();

            return Accepted();
        }

        [HttpPost("delete-product/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var product = _productRepository.Query().FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var currentUser = await _workContext.GetCurrentUser();
            if (!User.IsInRole("admin") && product.VendorId != currentUser.VendorId)
            {
                return BadRequest(new { error = "You don't have permission to manage this product" });
            }

            await _productService.Delete(product);

            return NoContent();
        }

        private IEnumerable<string> GetProductImageUrls(long productId)
        {
            var imageUrls = _productMediaRepository.Query()
                .Where(x => x.ProductId == productId)
                .OrderByDescending(x => x.Id)
                .Select(x => x.Media)
                .ToList()
                .Select(x => _mediaService.GetMediaUrl(x));

            return imageUrls;
        }

        private static ProductPriceHistory CreatePriceHistory(User loginUser, Product product)
        {
            return new ProductPriceHistory
            {
                CreatedBy = loginUser,
                Product = product,
                Price = product.Price,
                OldPrice = product.OldPrice,
                SpecialPrice = product.SpecialPrice,
                SpecialPriceStart = product.SpecialPriceStart,
                SpecialPriceEnd = product.SpecialPriceEnd
            };
        }

        private static void MapProductLinkVmToProduct(ProductForm model, Product product)
        {
            foreach (var relatedProductVm in model.Product.RelatedProducts)
            {
                var productLink = new ProductLink
                {
                    LinkType = ProductLinkType.Related,
                    Product = product,
                    LinkedProductId = relatedProductVm.Id
                };

                product.AddProductLinks(productLink);
            }

            foreach (var crossSellProductVm in model.Product.CrossSellProducts)
            {
                var productLink = new ProductLink
                {
                    LinkType = ProductLinkType.CrossSell,
                    Product = product,
                    LinkedProductId = crossSellProductVm.Id
                };

                product.AddProductLinks(productLink);
            }
        }

        private void AddOrDeleteCategories(ProductForm model, Product product)
        {
            foreach (var categoryId in model.Product.CategoryIds)
            {
                if (product.Categories.Any(x => x.CategoryId == categoryId))
                {
                    continue;
                }

                var productCategory = new ProductCategory
                {
                    CategoryId = categoryId
                };
                product.AddCategory(productCategory);
            }

            var deletedProductCategories =
                product.Categories.Where(productCategory => !model.Product.CategoryIds.Contains(productCategory.CategoryId))
                    .ToList();

            foreach (var deletedProductCategory in deletedProductCategories)
            {
                deletedProductCategory.Product = null;
                product.Categories.Remove(deletedProductCategory);
                _productCategoryRepository.Remove(deletedProductCategory);
            }
        }
        // Due to some issue with EF Core, we have to use _productLinkRepository in this case.
        private void AddOrDeleteProductLinks(ProductForm model, Product product)
        {
            foreach (var relatedProductVm in model.Product.RelatedProducts)
            {
                var productLink = product.ProductLinks.Where(x => x.LinkType == ProductLinkType.Related).FirstOrDefault(x => x.LinkedProductId == relatedProductVm.Id);
                if (productLink == null)
                {
                    productLink = new ProductLink
                    {
                        LinkType = ProductLinkType.Related,
                        Product = product,
                        LinkedProductId = relatedProductVm.Id,
                    };

                    _productLinkRepository.Add(productLink);
                }
            }

            foreach (var productLink in product.ProductLinks.Where(x => x.LinkType == ProductLinkType.Related))
            {
                if (model.Product.RelatedProducts.All(x => x.Id != productLink.LinkedProductId))
                {
                    _productLinkRepository.Remove(productLink);
                }
            }

            foreach (var crossSellProductVm in model.Product.CrossSellProducts)
            {
                var productLink = product.ProductLinks.Where(x => x.LinkType == ProductLinkType.CrossSell).FirstOrDefault(x => x.LinkedProductId == crossSellProductVm.Id);
                if (productLink == null)
                {
                    productLink = new ProductLink
                    {
                        LinkType = ProductLinkType.CrossSell,
                        Product = product,
                        LinkedProductId = crossSellProductVm.Id,
                    };

                    _productLinkRepository.Add(productLink);
                }
            }

            foreach (var productLink in product.ProductLinks.Where(x => x.LinkType == ProductLinkType.CrossSell))
            {
                if (model.Product.CrossSellProducts.All(x => x.Id != productLink.LinkedProductId))
                {
                    _productLinkRepository.Remove(productLink);
                }
            }
        }

        private void MapUploadedFile(ProductForm model)
        {
            // Currently model binder cannot map the collection of file productImages[0], productImages[1]
            foreach (var file in Request.Form.Files)
            {
                if (file.Name.Contains("productImages"))
                {
                    model.ProductImages.Add(file);
                }
                else if (file.Name.Contains("productDocuments"))
                {
                    model.ProductDocuments.Add(file);
                }
            }
        }

        private async Task SaveProductMedias(ProductForm model, Product product)
        {
            if (model.ThumbnailImage != null)
            {
                var fileName = await SaveFile(model.ThumbnailImage);
                if (product.ThumbnailImage != null)
                {
                    product.ThumbnailImage.FileName = fileName;
                }
                else
                {
                    product.ThumbnailImage = new Media { FileName = fileName };
                }
            }

            foreach (var file in model.ProductImages)
            {
                var fileName = await SaveFile(file);
                var productMedia = new ProductMedia
                {
                    Product = product,
                    Media = new Media { FileName = fileName, MediaType = MediaType.Image }
                };
                product.AddMedia(productMedia);
            }

            foreach (var file in model.ProductDocuments)
            {
                var fileName = await SaveFile(file);
                var productMedia = new ProductMedia
                {
                    Product = product,
                    Media = new Media { FileName = fileName, MediaType = MediaType.File, Caption = file.FileName }
                };
                product.AddMedia(productMedia);
            }
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Value.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _mediaService.SaveMediaAsync(file.OpenReadStream(), fileName, file.ContentType);
            return fileName;
        }
    }
}