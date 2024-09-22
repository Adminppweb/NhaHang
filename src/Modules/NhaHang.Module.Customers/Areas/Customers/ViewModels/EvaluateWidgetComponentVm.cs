using System.Collections.Generic;
using System.Linq;
using NhaHang.Module.Customers.Models;

namespace NhaHang.Module.Customers.Areas.Customers.ViewModels
{
    public class EvaluateWidgetComponentVm
    {
        public long Id { get; set; }

        public string WidgetName { get; set; }

        public string WidgetSubName { get; set; }

        public string WidgetDescription { get; set; }

        public EvaluateWidgetSetting Setting { get; set; }

        public IList<EvaluateThumbnail> Evaluates { get; set; }
    }
}
