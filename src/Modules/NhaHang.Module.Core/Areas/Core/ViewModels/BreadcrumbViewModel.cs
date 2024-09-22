namespace NhaHang.Module.Core.Areas.Core.ViewModels
{
    public class BreadcrumbViewModel
    {
        public string Text { get; set; }

        public string Url { get; set; }

        public long ParentId { get; set; }

        public NhaHang.Infrastructure.Commons.Breadcrumb TypeBreadcrumb { get; set; }
    }
}
