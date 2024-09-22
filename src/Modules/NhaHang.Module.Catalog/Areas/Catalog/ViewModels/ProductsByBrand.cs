using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NhaHang.Module.Catalog.Areas.Catalog.ViewModels
{
    public class ProductsByBrand
    {
        public long BrandId { get; set; }

        public string BrandName { get; set; }

        public string BrandSlug { get; set; }

        public string BrandMetaTitle { get; set; }

        public string BrandMetaKeywords { get; set; }

        public string BrandMetaDescription { get; set; }

        public string BrandDescription { get; set; }

        public int TotalProduct { get; set; }

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
