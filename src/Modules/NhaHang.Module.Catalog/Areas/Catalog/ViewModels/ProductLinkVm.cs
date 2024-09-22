using Microsoft.AspNetCore.Http;

namespace NhaHang.Module.Catalog.Areas.Catalog.ViewModels
{
    public class ProductLinkVm
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsPublished { get; set; }

        public decimal Price { get; set; }

        public IFormFile ThumbnailImage { get; set; }

        public string ThumbnailImageUrl { get; set; }
    }
}
