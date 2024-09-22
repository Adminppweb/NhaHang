using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using NhaHang.Module.Catalog.Models;
using NhaHang.Module.Core.Areas.Core.ViewModels;
using NhaHang.Module.Core.Models;

namespace NhaHang.Module.Catalog.Areas.Catalog.ViewModels
{
    public class ProductDetail
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string Sku { get; set; }

        public string ShortDescription { get; set; }

        public string MetaTitle { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public CalculatedProductPrice CalculatedProductPrice { get; set; }

        public string Description { get; set; }

        public string Specification { get; set; }

        public bool IsCallForPricing { get; set; }

        public bool IsAllowToOrder { get; set; }

        public bool StockTrackingIsEnabled { get; set; }

        public int StockQuantity { get; set; }

        public int ReviewsCount { get; set; }

        public double? RatingAverage { get; set; }

        public Media ThumbnailImage { get; set; }

        public string ThumbnailUrl { get; set; }

        public bool? UserCurrentIsLike { get; set; }

        public IList<MediaViewModel> Images { get; set; } = new List<MediaViewModel>();

        public IList<ProductDetailCategory> Categories { get; set; } = new List<ProductDetailCategory>();

        public IList<ProductThumbnail> RelatedProducts { get; set; } = new List<ProductThumbnail>();

        public IList<ProductThumbnail> CrossSellProducts { get; set; } = new List<ProductThumbnail>();

        public Brand Brand { get; set; }

    }

}
