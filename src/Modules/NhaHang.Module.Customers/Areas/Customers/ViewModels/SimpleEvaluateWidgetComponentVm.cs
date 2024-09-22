using System.Collections.Generic;

namespace NhaHang.Module.Customers.Areas.Customers.ViewModels
{
    public class SimpleEvaluateWidgetComponentVm
    {
        public long Id { get; set; }

        public string WidgetName { get; set; }

        public string WidgetSubName { get; set; }

        public string WidgetDescription { get; set; }

        public SimpleEvaluateWidgetSetting Setting { get; set; }

        public IList<EvaluateThumbnail> Evaluates { get; set; } = new List<EvaluateThumbnail>();
    }
}
