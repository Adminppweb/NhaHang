using System.Collections.Generic;
using System.Linq;
using NhaHang.Module.Catalog.Models;
using NhaHang.Module.Core.Services;

namespace NhaHang.Module.ShoppingCart.Areas.ShoppingCart.ViewModels
{
    public class CartItemVm
    {
        private readonly ICurrencyService _currencyService;

        public CartItemVm(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        public long Id { get; set; }

        public long ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductSlug { get; set; }

        public string ProductSku { get; set; }

        public string ProductImage { get; set; }

        public decimal ProductPrice { get; set; }

        public decimal ProductPriceOld { get; set; }

        public string ProductPriceString => _currencyService.FormatCurrency(ProductPrice);

        public string ProductPriceOldString => _currencyService.FormatCurrency(ProductPriceOld);

        public int ProductStockQuantity { get; set; }

        public bool ProductStockTrackingIsEnabled { get; set; }

        public bool IsProductAvailabeToOrder { get; set; }

        public int Quantity { get; set; }

        public decimal Total => Quantity * ProductPrice;

        public string TotalString => _currencyService.FormatCurrency(Total);
    }
}
