﻿using Microsoft.AspNetCore.Http;

namespace NhaHang.Module.Customers.Areas.Customers.ViewModels
{
    public class EvaluateLinkVm
	{
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsPublished { get; set; }

        public IFormFile ThumbnailImage { get; set; }

        public string ThumbnailImageUrl { get; set; }
    }
}
