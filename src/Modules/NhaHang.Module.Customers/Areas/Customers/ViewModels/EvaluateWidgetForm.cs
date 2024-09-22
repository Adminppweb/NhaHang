using NhaHang.Module.Core.Areas.Core.ViewModels;

namespace NhaHang.Module.Customers.Areas.Customers.ViewModels
{
    public class EvaluateWidgetForm : WidgetFormBase
    {
        public EvaluateWidgetSetting Setting { get; set; }

		public string SubName { get; set; }

		public string Description { get; set; }
	}
}
