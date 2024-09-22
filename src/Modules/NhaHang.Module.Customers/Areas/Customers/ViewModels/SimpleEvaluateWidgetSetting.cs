using System.Collections.Generic;

namespace NhaHang.Module.Customers.Areas.Customers.ViewModels
{
    public class SimpleEvaluateWidgetSetting
    {
        public IList<EvaluateLinkVm> Evaluates { get; set; } = new List<EvaluateLinkVm>();
    }
}
