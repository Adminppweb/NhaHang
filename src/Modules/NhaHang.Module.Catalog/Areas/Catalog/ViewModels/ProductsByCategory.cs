using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using NhaHang.Module.Core.Models;

namespace NhaHang.Module.Catalog.Areas.Catalog.ViewModels
{
    public class ProductsByCategory
    {
        public long CategoryId { get; set; }

        public long? ParentCategorId { get; set; }

        public string CategoryName { get; set; }

        public string CategorySlug { get; set; }

        public string CategoryMetaTitle { get; set; }

        public string CategoryMetaKeywords { get; set; }

        public string CategoryMetaDescription { get; set; }

        public string CategoryDescription { get; set; }

        public int TotalProduct { get; set; }

        public IFormFile ThumbnailImage { get; set; }

        public string CategoryThumbnailImageUrl { get; set; }

        public IList<ProductThumbnail> Products { get; set; } = new List<ProductThumbnail>();

        public FilterOption FilterOption { get; set; }

        public SearchOption CurrentSearchOption { get; set; }

        public IList<SelectListItem> AvailableSortOptions => new List<SelectListItem>
        {
            new SelectListItem { Text = "Giá: từ thấp lên cao", Value = "price-asc" },
            new SelectListItem { Text = "Giá: từ cao xuống thấp", Value = "price-desc" }
        };
    }
}
