using Microsoft.AspNetCore.Mvc;
using NhaHang.Infrastructure.Web;

namespace NhaHang.Module.PaymentCoD.Areas.PaymentCoD.Components
{
    public class CoDLandingViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(this.GetViewPath());
        }
    }
}
