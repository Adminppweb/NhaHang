using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NhaHang.Module.Customers.Areas.Customers.ViewModels
{
    public class EvaluateWidgetSetting
    {
        public int NumberOfEvaluates { get; set; }

        public long? CategoryId { get; set; }

        public bool FeaturedOnly { get; set; }
    }
}
