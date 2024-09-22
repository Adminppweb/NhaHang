﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NhaHang.Infrastructure.Web;
using NhaHang.Module.Core.Extensions;
using NhaHang.Module.Core.Services;
using NhaHang.Module.ShoppingCart.Areas.ShoppingCart.ViewModels;
using NhaHang.Module.ShoppingCart.Services;

namespace NhaHang.Module.ShoppingCart.Areas.ShoppingCart.Components
{
    public class CartBadgeMobileViewComponent : ViewComponent
    {
        private ICartService _cartService;
        private IWorkContext _workContext;
        private ICurrencyService _currencyService;

        public CartBadgeMobileViewComponent(ICartService cartService, IWorkContext workContext, ICurrencyService currencyService)
        {
            _cartService = cartService;
            _workContext = workContext;
            _currencyService = currencyService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentUser = await _workContext.GetCurrentUser();
            var cart = await _cartService.GetActiveCartDetails(currentUser.Id);
            if(cart == null)
            {
                cart = new CartVm(_currencyService);
            }
            
            return View(this.GetViewPath(), cart.Items.Count);
        }
    }
}
