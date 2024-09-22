using NhaHang.Module.Core.Areas.Core.ViewModels;

namespace NhaHang.Module.Customers.Areas.Customers.ViewModels
{
    public class SimpleEvaluateWidgetForm : WidgetFormBase
    {
        public SimpleEvaluateWidgetSetting Setting { get; set; }

        public string SubName { get; set; }

        public string Description { get; set; }
    }
}
