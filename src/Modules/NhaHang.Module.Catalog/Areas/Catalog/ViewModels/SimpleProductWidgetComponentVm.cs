﻿using System.Collections.Generic;

namespace NhaHang.Module.Catalog.Areas.Catalog.ViewModels
{
    public class SimpleProductWidgetComponentVm
    {
        public long Id { get; set; }

        public string WidgetName { get; set; }

        public string WidgetSubName { get; set; }

        public string WidgetDescription { get; set; }

        public SimpleProductWidgetSetting Setting { get; set; }

        public IList<ProductThumbnail> Products { get; set; } = new List<ProductThumbnail>();
    }
}
