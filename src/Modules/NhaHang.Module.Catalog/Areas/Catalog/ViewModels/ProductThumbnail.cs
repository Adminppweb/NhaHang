using System;
using System.Collections.Generic;
using NhaHang.Module.Catalog.Models;
using NhaHang.Module.Catalog.Services;
using NhaHang.Module.Core.Models;

namespace NhaHang.Module.Catalog.Areas.Catalog.ViewModels
{
    public class ProductThumbnail
    {
        private IProductPricingService _productPricingService;
        public ProductThumbnail()
        {
        }
        public ProductThumbnail(IProductPricingService productPricingService)
        {
            _productPricingService = productPricingService;
        }
        public long Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public decimal Price { get; set; }

        public decimal? OldPrice { get; set; }

        public decimal? SpecialPrice { get; set; }

        public bool IsCallForPricing { get; set; }

        public bool IsAllowToOrder { get; set; }

        public int? StockQuantity { get; set; }

        public DateTimeOffset? SpecialPriceStart { get; set; }

        public DateTimeOffset? SpecialPriceEnd { get; set; }
        public long? ThumbnailImageId { get; set; }
        public Media ThumbnailImage { get; set; }

        public string ThumbnailUrl { get; set; }

        public string ShortDescription { get; set; }

        public int ReviewsCount { get; set; }

        public double? RatingAverage { get; set; }

        public CalculatedProductPrice CalculatedProductPrice { get; set; } ///= new CalculatedProductPrice(_currencyService);
        public DateTimeOffset CreatedOn { get; set; }

        public static ProductThumbnail FromProduct(Product product)
        {
            var productThumbnail = new ProductThumbnail
            {
                Id = product.Id,
                Name = product.Name,
                Slug = product.Slug,
                ShortDescription = product.ShortDescription,
                Price = product.Price,
                OldPrice = product.OldPrice,
                SpecialPrice = product.SpecialPrice,
                SpecialPriceStart = product.SpecialPriceStart,
                SpecialPriceEnd = product.SpecialPriceEnd,
                StockQuantity = product.StockQuantity,
                IsAllowToOrder = product.IsAllowToOrder,
                IsCallForPricing = product.IsCallForPricing,
                ThumbnailImage = product.ThumbnailImage,
                ReviewsCount = product.ReviewsCount,
                RatingAverage = product.RatingAverage,
                CreatedOn = product.CreatedOn,
                ThumbnailImageId = product.ThumbnailImageId,
                //CalculatedProductPrice = new CalculatedProductPrice()
                //{
                //    Price = product.Price,
                //    OldPrice = product.OldPrice,

                //}
            };
            return productThumbnail;
        }

        public static List<ProductThumbnail> FromProducts(List<Product> products)
        {
            List<ProductThumbnail> productThumbnails = null;
            if (products != null)
            {
                productThumbnails = new List<ProductThumbnail>();
                foreach (var product in products)
                {
                    var productThumbnail = new ProductThumbnail()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Slug = product.Slug,
                        ShortDescription = product.ShortDescription,
                        Price = product.Price,
                        OldPrice = product.OldPrice,
                        SpecialPrice = product.SpecialPrice,
                        SpecialPriceStart = product.SpecialPriceStart,
                        SpecialPriceEnd = product.SpecialPriceEnd,
                        StockQuantity = product.StockQuantity,
                        IsAllowToOrder = product.IsAllowToOrder,
                        IsCallForPricing = product.IsCallForPricing,
                        ThumbnailImage = product.ThumbnailImage,
                        ReviewsCount = product.ReviewsCount,
                        RatingAverage = product.RatingAverage,
                        CreatedOn = product.CreatedOn,
                        ThumbnailImageId = product.ThumbnailImageId
                        //,
                        //CalculatedProductPrice = new CalculatedProductPrice()
                        //{

                        //    Price = product.Price,
                        //    OldPrice = product.OldPrice,
                        //}
                    };

                    productThumbnails.Add(productThumbnail);
                }
            }

            return productThumbnails;
        }

        public static ProductThumbnail FromProduct(Product product, IProductPricingService _productPricingService)
        {
            var productThumbnail = new ProductThumbnail
            {
                Id = product.Id,
                Name = product.Name,
                Slug = product.Slug,
                ShortDescription = product.ShortDescription,
                Price = product.Price,
                OldPrice = product.OldPrice,
                SpecialPrice = product.SpecialPrice,
                SpecialPriceStart = product.SpecialPriceStart,
                SpecialPriceEnd = product.SpecialPriceEnd,
                StockQuantity = product.StockQuantity,
                IsAllowToOrder = product.IsAllowToOrder,
                IsCallForPricing = product.IsCallForPricing,
                ThumbnailImage = product.ThumbnailImage,
                ReviewsCount = product.ReviewsCount,
                RatingAverage = product.RatingAverage,
                CreatedOn = product.CreatedOn,
                ThumbnailImageId = product.ThumbnailImageId,
            };
            productThumbnail.CalculatedProductPrice = _productPricingService.CalculateProductPrice(product.Price, product.OldPrice, product.SpecialPrice, product.SpecialPriceStart, product.SpecialPriceEnd);
            return productThumbnail;
        }

        public static List<ProductThumbnail> FromProducts(List<Product> products, IProductPricingService _productPricingService)
        {
            List<ProductThumbnail> productThumbnails = null;
            if (products != null)
            {
                productThumbnails = new List<ProductThumbnail>();
                foreach (var product in products)
                {
                    var productThumbnail = new ProductThumbnail()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Slug = product.Slug,
                        ShortDescription = product.ShortDescription,
                        Price = product.Price,
                        OldPrice = product.OldPrice,
                        SpecialPrice = product.SpecialPrice,
                        SpecialPriceStart = product.SpecialPriceStart,
                        SpecialPriceEnd = product.SpecialPriceEnd,
                        StockQuantity = product.StockQuantity,
                        IsAllowToOrder = product.IsAllowToOrder,
                        IsCallForPricing = product.IsCallForPricing,
                        ThumbnailImage = product.ThumbnailImage,
                        ReviewsCount = product.ReviewsCount,
                        RatingAverage = product.RatingAverage,
                        CreatedOn = product.CreatedOn,
                        ThumbnailImageId = product.ThumbnailImageId
                    };
                    productThumbnail.CalculatedProductPrice = _productPricingService.CalculateProductPrice(product.Price, product.OldPrice, product.SpecialPrice, product.SpecialPriceStart, product.SpecialPriceEnd);
                    productThumbnails.Add(productThumbnail);
                }
            }

            return productThumbnails;
        }
    }
}
