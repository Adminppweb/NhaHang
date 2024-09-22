using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NhaHang.Infrastructure.Data;
using NhaHang.Module.Catalog.Areas.Catalog.ViewModels;
using NhaHang.Module.Catalog.Models;
using NhaHang.Module.Core.Services;

namespace NhaHang.Module.Catalog.Services
{
    public class ProductService : IProductService
    {
        private const string ProductEntityTypeId = "Product";
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductPriceHistory> _productPriceHistoryRepository;
        private IProductPricingService _productPricingService;
        private readonly IMapper _mapper;
        private readonly IEntityService _entityService;
        private readonly IMediaService _mediaService;
        public ProductService(IRepository<Product> productRepository, IEntityService entityService, IMapper mapper, 
            IRepository<ProductPriceHistory> _productPriceHistoryRepository,
            IMediaService mediaService, IProductPricingService productPricingService
            )
        {
            _productRepository = productRepository;
            _entityService = entityService;
            _mapper = mapper;
            this._productPriceHistoryRepository = _productPriceHistoryRepository;
            _mediaService = mediaService;
            _productPricingService = productPricingService;
        }

        public void Create(Product product)
        {
            using (var transaction = _productRepository.BeginTransaction())
            {
                product.Slug = _entityService.ToSafeSlug(product.Slug, product.Id, ProductEntityTypeId);
                _productRepository.Add(product);
                _productRepository.SaveChanges();

                _entityService.Add(product.Name, product.Slug, product.Id, ProductEntityTypeId);
                _productRepository.SaveChanges();

                transaction.Commit();
            }
        }

        public void Update(Product product)
        {
            var slug = _entityService.Get(product.Id, ProductEntityTypeId);
            if (product.IsVisibleIndividually)
            {
                product.Slug = _entityService.ToSafeSlug(product.Slug, product.Id, ProductEntityTypeId);
                if (slug != null)
                {
                    _entityService.Update(product.Name, product.Slug, product.Id, ProductEntityTypeId);
                }
                else
                {
                    _entityService.Add(product.Name, product.Slug, product.Id, ProductEntityTypeId);
                }
            }
            else
            {
                if (slug != null)
                {
                    _entityService.Remove(product.Id, ProductEntityTypeId);
                }
            }
            _productRepository.SaveChanges();
        }

        public async Task Delete(Product product)
        {
            product.IsDeleted = true;
            await _entityService.Remove(product.Id, ProductEntityTypeId);
            _productRepository.SaveChanges();
        }

        public async Task<List<ProductThumbnail>> GetPromotionProducts()
        {
            List<ProductThumbnail> productThumnails = null;
            try
            {
                var products = await _productPriceHistoryRepository.Query()
                    .Include(x => x.Product)
                    .Where(x => x.IsActive.HasValue)
                    /*.Where(x => x.SpecialPriceStart!.Value >= DateTime.Now && DateTime.Now <= x.SpecialPriceEnd!.Value)*/
                    .ToListAsync();
                if (products != null)
                {
                   ///// productThumnails = ProductThumbnail.FromProducts(products.Select(x => x.Product).ToList());  /// _mapper.Map(products.Select(x => x.Product), productThumnails);
                    productThumnails = _mapper.Map(products.Select(x => x.Product), productThumnails);
                    foreach(var p in productThumnails) {
                        if (p.ThumbnailImageId.HasValue)
                        {
                            p.ThumbnailUrl = "/user-content/" + _mediaService.GetMediaId(p.ThumbnailImageId.Value).FileName;
                        }
                        p.CalculatedProductPrice = _productPricingService.CalculateProductPrice(p);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return productThumnails;
        }
    }
}
