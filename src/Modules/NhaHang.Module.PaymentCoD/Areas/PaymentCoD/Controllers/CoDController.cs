﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NhaHang.Infrastructure.Data;
using NhaHang.Module.Core.Extensions;
using NhaHang.Module.Orders.Services;
using NhaHang.Module.PaymentCoD.Models;
using NhaHang.Module.Payments.Models;
using NhaHang.Module.ShoppingCart.Areas.ShoppingCart.ViewModels;
using NhaHang.Module.ShoppingCart.Services;

namespace NhaHang.Module.PaymentCoD.Areas.PaymentCoD.Controllers
{
    [Authorize]
    [Area("PaymentCoD")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CoDController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IWorkContext _workContext;
        private readonly ICartService _cartService;
        private readonly IRepositoryWithTypedId<PaymentProvider, string> _paymentProviderRepository;
        private Lazy<CoDSetting> _setting;

        public CoDController(
            ICartService cartService,
            IOrderService orderService,
            IRepositoryWithTypedId<PaymentProvider, string> paymentProviderRepository,
            IWorkContext workContext)
        {
            _paymentProviderRepository = paymentProviderRepository;
            _cartService = cartService;
            _orderService = orderService;
            _workContext = workContext;
            _setting = new Lazy<CoDSetting>(GetSetting());
        }

        public async Task<IActionResult> CoDCheckout()
        {
            var currentUser = await _workContext.GetCurrentUser();
            var cart = await _cartService.GetActiveCartDetails(currentUser.Id);
            if(cart == null)
            {
                return NotFound();
            }

            if (!ValidateCoD(cart))
            {
                TempData["Error"] = "Phương thức thanh toán không đủ điều kiện cho đơn hàng này.";
                return Redirect("~/checkout/payment");
            }

            var calculatedFee = CalculateFee(cart);           
            var orderCreateResult = await _orderService.CreateOrder(cart.Id, "CashOnDelivery", calculatedFee);

            if (!orderCreateResult.Success)
            {
                TempData["Error"] = orderCreateResult.Error;
                return Redirect("~/checkout/payment");
            }

            return Redirect($"~/checkout/success?orderId={orderCreateResult.Value.Id}");
        }

        private CoDSetting GetSetting()
        {
            var coDProvider = _paymentProviderRepository.Query().FirstOrDefault(x => x.Id == PaymentProviderHelper.CODProviderId);
            if (string.IsNullOrEmpty(coDProvider.AdditionalSettings))
            {
                return new CoDSetting();
            }

            var coDSetting = JsonConvert.DeserializeObject<CoDSetting>(coDProvider.AdditionalSettings);
            return coDSetting;
        }

        private bool ValidateCoD(CartVm cart)
        {
            if (_setting.Value.MinOrderValue.HasValue && _setting.Value.MinOrderValue.Value > cart.OrderTotal)
            {
                return false;
            }

            if (_setting.Value.MaxOrderValue.HasValue && _setting.Value.MaxOrderValue.Value < cart.OrderTotal)
            {
                return false;
            }

            return true;
        }

        private decimal CalculateFee(CartVm cart)
        {
            var percent = _setting.Value.PaymentFee;
            return (cart.OrderTotal / 100) * percent;
        }
    }
}
