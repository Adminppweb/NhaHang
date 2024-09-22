using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NhaHang.Infrastructure.Web;
using NhaHang.Module.Core.Extensions;
using NhaHang.Module.Core.Services;
using NhaHang.Module.ShoppingCart.Areas.ShoppingCart.ViewModels;
using NhaHang.Module.ShoppingCart.Services;

namespace NhaHang.Module.Orders.Areas.Orders.Components
{
    public class OrderSummaryMobileViewComponent : ViewComponent
    {
        private readonly ICartService _cartService;
        private readonly IWorkContext _workContext;
        private ICurrencyService _currencyService;

        public OrderSummaryMobileViewComponent(ICartService cartService, IWorkContext workContext, ICurrencyService currencyService)
        {
            _cartService = cartService;
            _workContext = workContext;
            _currencyService = currencyService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var curentUser = await _workContext.GetCurrentUser();
            var cart = await _cartService.GetActiveCartDetails(curentUser.Id);
            if (cart == null)
            {
                cart = new CartVm(_currencyService);
            }
            return View(this.GetViewPath(), cart);
        }
    }
}
