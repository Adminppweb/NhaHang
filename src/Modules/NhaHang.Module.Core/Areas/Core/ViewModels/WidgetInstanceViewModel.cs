using static Humanizer.On;

namespace NhaHang.Module.Core.Areas.Core.ViewModels
{
    public class WidgetInstanceViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string ViewComponentName { get; set; }

        public long WidgetZoneId { get; set; }

        public string Data { get; set; }

        public string HtmlData { get; set; }

        public string WidgetId { get; set; }

        public string SubName { get; set; }

        public string Description { get; set; }
    }
}
