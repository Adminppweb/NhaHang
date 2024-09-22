using NhaHang.Module.Core.Areas.Core.ViewModels;

namespace NhaHang.Module.Catalog.Areas.Catalog.ViewModels
{
    public class SimpleProductWidgetForm : WidgetFormBase
    {
        public SimpleProductWidgetSetting Setting { get; set; }

        public string SubName { get; set; }

        public string Description { get; set; }
    }
}
