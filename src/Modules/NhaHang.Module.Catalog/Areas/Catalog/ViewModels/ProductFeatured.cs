using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace NhaHang.Module.Catalog.Areas.Catalog.ViewModels
{
    public class ProductFeatured
    {
        public IList<ProductThumbnail> Products { get; set; } = new List<ProductThumbnail>();
    }
}
