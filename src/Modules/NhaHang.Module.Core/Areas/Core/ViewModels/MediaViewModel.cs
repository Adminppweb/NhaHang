using NhaHang.Module.Core.Models;

namespace NhaHang.Module.Core.Areas.Core.ViewModels
{
    public class MediaViewModel
    {
        public long Id { get; set; }
        public string Caption  { get; set; }
        public MediaType MediaType { get; set; }
        public string Url { get; set; }
        public int Size { get; set; }
        public long? ParentId { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
