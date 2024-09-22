using Microsoft.AspNetCore.Http;

namespace NhaHang.Module.Catalog.Areas.Catalog.ViewModels
{
    public class CategoryLinkVm
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsPublished { get; set; }

        public IFormFile ThumbnailImage { get; set; }

        public string ThumbnailImageUrl { get; set; }
    }
}
