using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NhaHang.Infrastructure.Data;
using NhaHang.Module.Core.Extensions;
using NhaHang.Module.Core.Models;
using NhaHang.Module.Core.Services;
using NhaHang.Module.Orders.Areas.Orders.ViewModels;
using NhaHang.Module.Orders.Models;
using NhaHang.Module.Orders.Services;

//using NhaHang.Module.ShippingPrices.Services;
using NhaHang.Module.ShoppingCart.Models;
using NhaHang.Module.ShoppingCart.Services;

namespace NhaHang.Module.Orders.Areas.Orders.Controllers
{
    [Area("Orders")]
    [Route("checkout")]
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CheckoutController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IRepositoryWithTypedId<Country, string> _countryRepository;
        private readonly IRepository<StateOrProvince> _stateOrProvinceRepository;
        private readonly IRepository<UserAddress> _userAddressRepository;

        // private readonly IShippingPriceService _shippingPriceService;
        private readonly ICartService _cartService;

        private readonly IWorkContext _workContext;
        private readonly IRepository<Cart> _cartRepository;
        private readonly IMediaService _mediaService;
        private readonly ICurrencyService _currencyService;
        private readonly IRepository<Order> _orderRepository;

        public CheckoutController(
            IRepository<StateOrProvince> stateOrProvinceRepository,
            IRepositoryWithTypedId<Country, string> countryRepository,
            IRepository<UserAddress> userAddressRepository,
            // IShippingPriceService shippingPriceService,
            IOrderService orderService,
            ICartService cartService,
            IWorkContext workContext,
            IMediaService mediaService,
            ICurrencyService currencyService,
            IRepository<Order> orderRepository,
            IRepository<Cart> cartRepository)
        {
            _stateOrProvinceRepository = stateOrProvinceRepository;
            _countryRepository = countryRepository;
            _userAddressRepository = userAddressRepository;
            //_shippingPriceService = shippingPriceService;
            _orderService = orderService;
            _cartService = cartService;
            _workContext = workContext;
            _cartRepository = cartRepository;
            _mediaService = mediaService;
            _currencyService = currencyService;
            _orderRepository = orderRepository;
        }

        [HttpGet("shipping")]
        public async Task<IActionResult> Shipping()
        {
            var currentUser = await _workContext.GetCurrentUser();
            var cart = await _cartService.GetActiveCartDetails(currentUser.Id);
            if (cart == null || !cart.Items.Any())
            {
                return Redirect("~/");
            }

            var model = new DeliveryInformationVm();

            PopulateShippingForm(model, currentUser);

            return View(model);
        }

        [HttpPost("shipping")]
        public async Task<IActionResult> Shipping(DeliveryInformationVm model)
        {
            var currentUser = await _workContext.GetCurrentUser();
            // TODO Handle error messages
            if ((!model.NewAddressForm.IsValid() && model.ShippingAddressId == 0) ||
                (!model.NewBillingAddressForm.IsValid() && !model.UseShippingAddressAsBillingAddress && model.BillingAddressId == 0))
            {
                PopulateShippingForm(model, currentUser);
                return View(model);
            }

            var cart = await _cartService.GetActiveCart(currentUser.Id);

            if (cart == null)
            {
                throw new ApplicationException($"Không thể tìm thấy giỏ hàng của người dùng {currentUser.Id}");
            }

            cart.ShippingData = JsonConvert.SerializeObject(model);
            await _cartRepository.SaveChangesAsync();
            return Redirect("~/checkout/payment");
        }

        [HttpPost("update-tax-and-shipping-prices")]
        public async Task<IActionResult> UpdateTaxAndShippingPrices([FromBody] TaxAndShippingPriceRequestVm model)
        {
            var currentUser = await _workContext.GetCurrentUser();
            var cart = await _cartService.GetActiveCart(currentUser.Id);
            var orderTaxAndShippingPrice = await _orderService.UpdateTaxAndShippingPrices(cart.Id, model);

            return Ok(orderTaxAndShippingPrice);
        }

        [HttpGet("success")]
        public IActionResult Success(long orderId)
        {
            var order = _orderRepository
                .Query()
                .Include(x => x.ShippingAddress).ThenInclude(x => x.District)
                .Include(x => x.ShippingAddress).ThenInclude(x => x.StateOrProvince)
                .Include(x => x.ShippingAddress).ThenInclude(x => x.Country)
                .Include(x => x.OrderItems).ThenInclude(x => x.Product).ThenInclude(x => x.ThumbnailImage)
                .Include(x => x.Customer)
                .FirstOrDefault(x => x.Id == orderId);

            if (order == null)
            {
                return NotFound();
            }
            if (order != null)
            {
                var model = new OrderDetailVm(_currencyService)
                {
                    Id = order.Id,
                    IsMasterOrder = order.IsMasterOrder,
                    CreatedOn = order.CreatedOn,
                    OrderStatus = (int)order.OrderStatus,
                    OrderStatusString = order.OrderStatus.ToString(),
                    CustomerId = order.CustomerId,
                    CustomerName = order.Customer.FullName,
                    CustomerEmail = order.Customer.Email,
                    ShippingMethod = order.ShippingMethod,
                    PaymentMethod = order.PaymentMethod,
                    PaymentFeeAmount = order.PaymentFeeAmount,
                    Subtotal = order.SubTotal,
                    DiscountAmount = order.DiscountAmount,
                    SubTotalWithDiscount = order.SubTotalWithDiscount,
                    TaxAmount = order.TaxAmount,
                    ShippingAmount = order.ShippingFeeAmount,
                    OrderTotal = order.OrderTotal,
                    OrderNote = order.OrderNote,
                    ShippingAddress = new ShippingAddressVm
                    {
                        AddressLine1 = order.ShippingAddress.AddressLine1,
                        CityName = order.ShippingAddress.City,
                        ZipCode = order.ShippingAddress.ZipCode,
                        ContactName = order.ShippingAddress.ContactName,
                        DistrictName = order.ShippingAddress.District?.Name,
                        StateOrProvinceName = order.ShippingAddress.StateOrProvince.Name,
                        Phone = order.ShippingAddress.Phone
                    },
                    OrderItems = order.OrderItems.Select(x => new OrderItemVm(_currencyService)
                    {
                        Id = x.Id,
                        ProductId = x.Product.Id,
                        ProductName = x.Product.Name,
                        ProductPrice = x.ProductPrice,
                        Quantity = x.Quantity,
                        DiscountAmount = x.DiscountAmount,
                        ProductImage = x.Product.ThumbnailImage.FileName,
                        TaxAmount = x.TaxAmount,
                        TaxPercent = x.TaxPercent,
                    }).ToList()
                };
                foreach (var item in model.OrderItems)
                {
                    item.ProductImage = _mediaService.GetMediaUrl(item.ProductImage);
                }

                return View(model);
            }
            else
            {
                return NotFound();
            }
            //return View(orderId);
        }

        [HttpGet("error")]
        public IActionResult Error(long orderId)
        {
            var order = _orderRepository
                .Query()
                .Include(x => x.ShippingAddress).ThenInclude(x => x.District)
                .Include(x => x.ShippingAddress).ThenInclude(x => x.StateOrProvince)
                .Include(x => x.ShippingAddress).ThenInclude(x => x.Country)
                .Include(x => x.OrderItems).ThenInclude(x => x.Product).ThenInclude(x => x.ThumbnailImage)
                .Include(x => x.Customer)
                .FirstOrDefault(x => x.Id == orderId);

            if (order == null)
            {
                return NotFound();
            }
            if (order != null)
            {
                var model = new OrderDetailVm(_currencyService)
                {
                    Id = order.Id,
                    IsMasterOrder = order.IsMasterOrder,
                    CreatedOn = order.CreatedOn,
                    OrderStatus = (int)order.OrderStatus,
                    OrderStatusString = order.OrderStatus.ToString(),
                    CustomerId = order.CustomerId,
                    CustomerName = order.Customer.FullName,
                    CustomerEmail = order.Customer.Email,
                    ShippingMethod = order.ShippingMethod,
                    PaymentMethod = order.PaymentMethod,
                    PaymentFeeAmount = order.PaymentFeeAmount,
                    Subtotal = order.SubTotal,
                    DiscountAmount = order.DiscountAmount,
                    SubTotalWithDiscount = order.SubTotalWithDiscount,
                    TaxAmount = order.TaxAmount,
                    ShippingAmount = order.ShippingFeeAmount,
                    OrderTotal = order.OrderTotal,
                    OrderNote = order.OrderNote,
                    ShippingAddress = new ShippingAddressVm
                    {
                        AddressLine1 = order.ShippingAddress.AddressLine1,
                        CityName = order.ShippingAddress.City,
                        ZipCode = order.ShippingAddress.ZipCode,
                        ContactName = order.ShippingAddress.ContactName,
                        DistrictName = order.ShippingAddress.District?.Name,
                        StateOrProvinceName = order.ShippingAddress.StateOrProvince.Name,
                        Phone = order.ShippingAddress.Phone
                    }
                    //OrderItems = order.OrderItems.Select(x => new OrderItemVm(_currencyService)
                    //{
                    //    Id = x.Id,
                    //    ProductId = x.Product.Id,
                    //    ProductName = x.Product?.Name,
                    //    ProductPrice = x.ProductPrice,
                    //    Quantity = x.Quantity,
                    //    DiscountAmount = x.DiscountAmount,
                    //    ProductImage = x.Product.ThumbnailImage.FileName,
                    //    TaxAmount = x.TaxAmount,
                    //    TaxPercent = x.TaxPercent,
                    //    VariationOptions = OrderItemVm.GetVariationOption(x.Product)
                    //}).ToList()
                };
                foreach (var item in model.OrderItems)
                {
                    item.ProductImage = _mediaService.GetMediaUrl(item.ProductImage);
                }

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> Cancel()
        {
            var currentUser = await _workContext.GetCurrentUser();
            var cart = await _cartService.GetActiveCart(currentUser.Id);
            if (cart != null && cart.LockedOnCheckout)
            {
                cart.LockedOnCheckout = false;
                await _cartRepository.SaveChangesAsync();
            }

            return Redirect("~/");
        }

        private void PopulateShippingForm(DeliveryInformationVm model, User currentUser)
        {
            model.ExistingShippingAddresses = _userAddressRepository
                .Query()
                .Where(x => (x.AddressType == AddressType.Shipping) && (x.UserId == currentUser.Id))
                .Select(x => new ShippingAddressVm
                {
                    UserAddressId = x.Id,
                    ContactName = x.Address.ContactName,
                    Phone = x.Address.Phone,
                    AddressLine1 = x.Address.AddressLine1,
                    CityName = x.Address.City,
                    ZipCode = x.Address.ZipCode,
                    DistrictName = x.Address.District.Name,
                    StateOrProvinceName = x.Address.StateOrProvince.Name,
                    CountryName = x.Address.Country.Name,
                    IsCityEnabled = x.Address.Country.IsCityEnabled,
                    IsZipCodeEnabled = x.Address.Country.IsZipCodeEnabled,
                    IsDistrictEnabled = x.Address.Country.IsDistrictEnabled
                }).ToList();

            model.ShippingAddressId = currentUser.DefaultShippingAddressId ?? 0;

            model.UseShippingAddressAsBillingAddress = true;

            model.NewAddressForm.ShipableContries = _countryRepository.Query()
                .Where(x => x.IsShippingEnabled)
                .OrderBy(x => x.Name)
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();

            if (model.NewAddressForm.ShipableContries.Count == 1)
            {
                var onlyShipableCountryId = model.NewAddressForm.ShipableContries.First().Value;

                model.NewAddressForm.StateOrProvinces = _stateOrProvinceRepository
                .Query()
                .Where(x => x.CountryId == onlyShipableCountryId)
                .OrderBy(x => x.Name)
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
            }
        }
    }
}