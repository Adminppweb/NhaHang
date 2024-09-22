using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NhaHang.Infrastructure.Commons;
using NhaHang.Infrastructure.Data;
using NhaHang.Infrastructure.Web.SmartTable;
using NhaHang.Module.Catalog.Areas.Catalog.ViewModels;
using NhaHang.Module.Catalog.Models;
using NhaHang.Module.Core.Extensions;
using NhaHang.Module.Core.Models;
using NhaHang.Module.Core.Services;

namespace NhaHang.Module.Catalog.Services
{
    public class CategoryService : ICategoryService
    {
        private const string CategoryEntityTypeId = "Category";
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductCategory> _productCategoryRepository;
        private readonly IRepository<Entity> _entiyRepository;
        private readonly IEntityService _entityService;
        private readonly IMediaService _mediaService;
        private readonly IProductPricingService _productPricingService;
        private readonly IMapper _mapper;
        private readonly IContentLocalizationService _contentLocalizationService;
        private readonly IWorkContext _workContext;
        public CategoryService(IRepository<Category> categoryRepository, IEntityService entityService,
            IMediaService mediaService,
            IRepository<Product> productRepository, 
            IMapper mapper,
            IRepository<ProductCategory> productCategoryRepository,
            IContentLocalizationService contentLocalizationService, 

            IWorkContext _workContext, IRepository<Entity> entiyRepository, IProductPricingService productPricingService)
        {
            _categoryRepository = categoryRepository;
            _entityService = entityService;
            _mediaService = mediaService;
            _productRepository = productRepository;
            _mapper = mapper;
            _productCategoryRepository = productCategoryRepository;
            _contentLocalizationService = contentLocalizationService;
            _entiyRepository = entiyRepository;
            _productPricingService = productPricingService;
            this._workContext = _workContext;
        }

        private void MapCategoryFilter(List<Filter> filters, Category category)
        {
            if (category != null)
            {
                filters.Add(new Filter { TypeFilter = TypeFilter.Category, Value = category.Id + string.Empty });
                //if (category.Children != null)
                //{
                //    foreach (var children in category.Children)
                //    {
                //        MapCategoryFilter(filters, children);
                //    }
                //}
                // var categories = _categoryRepository.Query().Where(x => x.Id == filters.Select().).ToList();
            }
        }

        private List<long> GetCategoryIds(List<Category> categories)
        {
            List<long> categoryIds = new List<long>();
            foreach (var category in categories)
            {
                categoryIds.Add(category.Id);
                if (category != null)
                {
                    var getCategory = _categoryRepository.Query().Include(c => c.Children).Where(x => category.Id == x.Id).First();
                    if (getCategory.Children.ToList() != null)
                    {
                        categoryIds.AddRange(GetCategoryIds(getCategory.Children.ToList()));
                    }
                }
            }
            return categoryIds;
        }


        /// <summary>
        /// Filter Category
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        private List<long> GetProductIdFilter(List<Filter> filters)
        {
            List<Product> products = new List<Product>();
            var productCategory = new List<ProductCategory>();
            List<long> ProductId = new List<long>();
            try
            {
                string slug = _workContext.GetSlugCurrent();
                long categoryIdSlug = -1;
                //Check Slug Category
                if (!string.IsNullOrEmpty(slug))
                {
                    var categorySlug = _categoryRepository.Query().Where(x => x.Slug == slug).FirstOrDefault();
                    if (categorySlug != null)
                    {
                        categoryIdSlug = categorySlug.Id;
                    }
                }

                productCategory = _productCategoryRepository.Query().Include(x => x.Category).Include(x => x.Product).ToList();
                var getCategoryNotParentId = _categoryRepository.Query().Where(x => x.ParentId == null).Select(x => x.Id).ToList();
                if (filters.Count == 0)
                {
                    ProductId = productCategory.Where(x => categoryIdSlug == -1 || x.CategoryId == categoryIdSlug).Select(x => x.ProductId).ToList();
                }
                else
                {
                    ProductId = productCategory.Where(x => filters.Select(x => x.Value).Contains(x.CategoryId + string.Empty) && (categoryIdSlug == -1 || x.CategoryId == categoryIdSlug) && !getCategoryNotParentId.Contains(x.CategoryId)).Select(x => x.ProductId).ToList();
                }
            }
            catch (Exception ex)
            {
            }
            return ProductId;
        }
        private List<long> GetProductIdAllowCategoryFilter(List<Filter> filters)
        {
            List<long> ProductId = null;
            try
            {
                var categoryParrent = _categoryRepository.Query().Where(x => filters.Select(f => long.Parse(f.Value)).Contains(x.Id)).ToList();
                var CategoryIds = GetCategoryIds(categoryParrent);
                var productIds = _productCategoryRepository.Query().Where(x => CategoryIds.Contains(x.CategoryId)).Select(x => x.ProductId).ToList();
                return productIds;
            }
            catch (Exception ex)
            {
            }
            return ProductId;
        }
        public async Task<List<ProductThumbnail>> GetProductFilter(List<Filter> shopCartFilterVm = null)
        {
            List<ProductThumbnail> ProductThumbnails = new List<ProductThumbnail>();
            string slug = _workContext.GetSlugCurrent();
            try
            {
                if (shopCartFilterVm == null)
                {
                    shopCartFilterVm = new List<Filter>();
                }
                List<long> ProductId = new List<long>();
                if (shopCartFilterVm.Count == 0)
                {
                    var productIds = GetProductIdFilter(shopCartFilterVm);
                    if (productIds != null)
                    {
                        var products = _productRepository.Query().Include(x => x.ThumbnailImage).Include(x => x.Categories).Where(x => productIds.Contains(x.Id)).ToList();
                        var allProductSlug = products.Select(x => x.Slug).ToList();
                        var productInEntity = _entiyRepository.Query().Where(x => allProductSlug.Contains(x.Slug)).Select(x => x.Slug);
                        products = products.Where(x => productInEntity.Contains(x.Slug)).ToList();
                        if (products != null)
                        {
                            ProductThumbnails = ProductThumbnail.FromProducts(products, _productPricingService);
                            //products.Select(x => new ProductThumbnail(_currencyService)
                            //{

                            //    Id = x.Id,
                            //    Name = x.Name,
                            //    ThumbnailImage = x.ThumbnailImage,
                            //    Slug = x.Slug,
                            //    ThumbnailUrl = x.ThumbnailImage != null ? "/user-content/" + x.ThumbnailImage!.FileName : "",
                            //    ShortDescription = x.ShortDescription,
                            //    SpecialPrice = x.SpecialPrice,
                            //    OldPrice = x.OldPrice,
                            //    Price = x.Price,
                            //    CreatedOn = x.CreatedOn
                            //}).ToList();
                            ///_mapper.Map<List<ProductThumbnail>>(products);

                        }
                    }
                    return ProductThumbnails;
                }
                else if (shopCartFilterVm.Count > 0)
                {

                    if (shopCartFilterVm.Any(x => x.TypeFilter == TypeFilter.Category))
                    {
                        //var Values = shopCartFilterVm.Where(x => x.TypeFilter == TypeFilter.Category && !string.IsNullOrEmpty(x.Value)).Select(a => a.Value).ToList();
                        //if (Values != null)
                        //{
                        //    //var categoriesId = _categoryRepository.Query().Where(x => x.ParentId == null).Select(x => x.Id).ToList();
                        //    var ProdcuctCategory = _productCategoryRepository.Query().Where(x => /*!categoriesId.Contains(x.Id) &&*/ Values.Contains(x.CategoryId + string.Empty)).Include(x => x.Product).ToList();
                        //    if (ProdcuctCategory != null)
                        //    {
                        //        //var productCategoryId = ProdcuctCategory.Where(x => Values.Contains(x.CategoryId + string.Empty)).Select(x => x.ProductId).ToList();
                        //        //if (productCategoryId != null)
                        //        //{

                        //        //    ProductId.AddRange(productCategoryId);
                        //        //}
                        //        //ProductId.AddRange(ProdcuctIds);
                        //    }
                        //}    
                        var productIds = GetProductIdAllowCategoryFilter(shopCartFilterVm.Where(x => x.TypeFilter == TypeFilter.Category).ToList());
                        if (productIds != null)
                        {
                            ProductId.AddRange(productIds);
                        }
                    }

                    if (shopCartFilterVm.Any(x => x.TypeFilter == TypeFilter.Rating))
                    {
                        var Values = shopCartFilterVm.Where(x => x.TypeFilter == TypeFilter.Rating && !string.IsNullOrEmpty(x.Value)).Select(a => Convert.ToInt64(a.Value)).ToList();
                        if (Values != null)
                        {
                            if (ProductId.Count > 0)
                            {
                                Values = Values.Where(x => ProductId.Contains(x)).ToList();
                            }
                            if (Values != null)
                            {
                                ProductId = new List<long>();
                                ProductId.AddRange(Values);
                            }

                        }

                    }

                    if (shopCartFilterVm.Any(x => x.TypeFilter == TypeFilter.Price))
                    {

                        var Values = shopCartFilterVm.Where(x => x.TypeFilter == TypeFilter.Price && !string.IsNullOrEmpty(x.Value)).Select(a => a.Value).ToList();
                        if (Values != null)
                        {
                            foreach (var price in Values)
                            {
                                var arrayPrice = price.Split("-");
                                var minPirce = arrayPrice[0];
                                var maxPirce = arrayPrice[1];
                                var productIds = _productRepository.Query().Where(x => Convert.ToDecimal(minPirce) * 1000 <= x.Price && x.Price <= Convert.ToDecimal(maxPirce) * 1000).Select(x => x.Id).ToList();
                                if (productIds != null)
                                {
                                    if (ProductId.Count > 0)
                                    {
                                        productIds = productIds.Where(x => ProductId.Contains(x)).ToList();
                                    }
                                    if (productIds != null)
                                    {
                                        ProductId = new List<long>();
                                        ProductId.AddRange(productIds);
                                    }
                                }
                            }

                        }
                    }

                    //Get OptionId
                    //Get All ProductOption => OptionId

                    //// Filter Color: Giống Attribute
                    if (ProductId.Count > 0)
                    {
                        var products = _productRepository.Query().Include(x => x.ThumbnailImage).Where(x => /*x.IsPublished && x.IsAllowToOrder &&*/ ProductId.Contains(x.Id)).ToList();
                        var allProductSlug = products.Select(x => x.Slug).ToList();
                        var productInEntity = _entiyRepository.Query().Where(x => allProductSlug.Contains(x.Slug)).Select(x => x.Slug);
                        products = products.Where(x => productInEntity.Contains(x.Slug)).ToList();
                        if (products != null)
                        {
                            ProductThumbnails = ProductThumbnail.FromProducts(products, _productPricingService);
                            //ProductThumbnails = products.Select(x => new ProductThumbnail(_currencyService)
                            //{

                            //    Id = x.Id,
                            //    Name = x.Name,
                            //    ThumbnailImage = x.ThumbnailImage,
                            //    Slug = x.Slug,
                            //    ThumbnailUrl = x.ThumbnailImage != null ? "/user-content/" + x.ThumbnailImage!.FileName : "",
                            //    ShortDescription = x.ShortDescription,
                            //    SpecialPrice = x.SpecialPrice,
                            //    OldPrice = x.OldPrice,
                            //    Price = x.Price,
                            //    CreatedOn = x.CreatedOn
                            //}).ToList();
                            ///ProductThumbnails = _mapper.Map<List<ProductThumbnail>>(products);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
            }
            return ProductThumbnails;
        }

        public async Task<List<ProductThumbnail>> GetlayoutProductFilter(List<Filter> shopCartFilterVm = null)
        {
            List<ProductThumbnail> ProductThumbnails = new List<ProductThumbnail>();
            string slug = _workContext.GetSlugCurrent();
            try
            {
                if (shopCartFilterVm == null)
                {
                    shopCartFilterVm = new List<Filter>();
                }
                List<long> ProductId = new List<long>();
                if (shopCartFilterVm.Count == 0)
                {
                    //var productIds = GetProductIdFilter(shopCartFilterVm);
                    //if (productIds != null)
                    //{
                    //    var products = _productRepository.Query().Include(x => x.ThumbnailImage).Include(x => x.Categories).Where(x => productIds.Contains(x.Id)).ToList();
                    //    var allProductSlug = products.Select(x => x.Slug).ToList();
                    //    var productInEntity = _entiyRepository.Query().Where(x => allProductSlug.Contains(x.Slug)).Select(x => x.Slug);
                    //    products = products.Where(x => productInEntity.Contains(x.Slug)).ToList();
                    //    if (products != null)
                    //    {
                    //        ProductThumbnails = ProductThumbnail.FromProducts(products, _productPricingService);
                    //    }
                    //}
                    return ProductThumbnails;
                }
                else if (shopCartFilterVm.Count > 0)
                {

                    if (shopCartFilterVm.Any(x => x.TypeFilter == TypeFilter.Category))
                    {  
                        var productIds = GetProductIdAllowCategoryFilter(shopCartFilterVm.Where(x => x.TypeFilter == TypeFilter.Category).ToList());
                        if (productIds != null)
                        {
                            ProductId.AddRange(productIds);
                        }
                    }

                    if (ProductId.Count > 0)
                    {
                        var products = _productRepository.Query().Include(x => x.ThumbnailImage).Where(x => /*x.IsPublished && x.IsAllowToOrder &&*/ ProductId.Contains(x.Id)).ToList();
                        var allProductSlug = products.Select(x => x.Slug).ToList();
                        var productInEntity = _entiyRepository.Query().Where(x => allProductSlug.Contains(x.Slug)).Select(x => x.Slug);
                        products = products.Where(x => productInEntity.Contains(x.Slug)).ToList();
                        if (products != null)
                        {
                            ProductThumbnails = ProductThumbnail.FromProducts(products, _productPricingService);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
            }
            return ProductThumbnails;
        }

        public async Task<IList<CategoryListItem>> GetAll()
        {
            var categories = await _categoryRepository.Query().Where(x => !x.IsDeleted).ToListAsync();
            var categoriesList = new List<CategoryListItem>();
            foreach (var category in categories)
            {
                var idImages = category.ThumbnailImageId.HasValue ? category.ThumbnailImageId.Value : 0;
                var categoryListItem = new CategoryListItem
                {
                    Id = category.Id,
                    IsPublished = category.IsPublished,
                    IncludeInMenu = category.IncludeInMenu,
                    Name = category.Name,
                    DisplayOrder = category.DisplayOrder,
                    ParentId = category.ParentId,

                    ThumbnailImage = (idImages == 0 ? null : _mediaService.GetMediaId(idImages))
                };

                var parentCategory = category.Parent;
                while (parentCategory != null)
                {
                    categoryListItem.Name = $"{parentCategory.Name} >> {categoryListItem.Name}";
                    parentCategory = parentCategory.Parent;
                }

                categoriesList.Add(categoryListItem);
            }

            return categoriesList.OrderBy(x => x.Name).ToList();
        }

        public async Task Create(Category category)
        {
            using (var transaction = _categoryRepository.BeginTransaction())
            {
                category.Slug = _entityService.ToSafeSlug(category.Slug, category.Id, CategoryEntityTypeId);
                _categoryRepository.Add(category);
                await _categoryRepository.SaveChangesAsync();

                _entityService.Add(category.Name, category.Slug, category.Id, CategoryEntityTypeId);
                await _categoryRepository.SaveChangesAsync();

                transaction.Commit();
            }
        }

        public async Task Update(Category category)
        {
            category.Slug = _entityService.ToSafeSlug(category.Slug, category.Id, CategoryEntityTypeId);
            _entityService.Update(category.Name, category.Slug, category.Id, CategoryEntityTypeId);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task Delete(Category category)
        {
            await _entityService.Remove(category.Id, CategoryEntityTypeId);
            category.IsDeleted = true;
            _categoryRepository.SaveChanges();
        }


    }
}
