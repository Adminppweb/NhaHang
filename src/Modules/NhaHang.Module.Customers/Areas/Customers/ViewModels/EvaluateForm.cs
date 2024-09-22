using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace NhaHang.Module.Customers.Areas.Customers.ViewModels
{
    public class EvaluateForm
    {
        public EvaluateVm Evaluate { get; set; } = new EvaluateVm();

        public IFormFile ThumbnailImage { get; set; }

    }
}
