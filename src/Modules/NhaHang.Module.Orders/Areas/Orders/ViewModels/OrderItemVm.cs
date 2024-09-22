using System.Collections.Generic;
using System.Linq;
using NhaHang.Module.Catalog.Models;
using NhaHang.Module.Core.Services;
using NhaHang.Module.ShoppingCart.Areas.ShoppingCart.ViewModels;

namespace NhaHang.Module.Orders.Areas.Orders.ViewModels
{
    public class OrderItemVm
    {
        private readonly ICurrencyService _currencyService;

        public OrderItemVm(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        public long Id { get; set; }

        public long ProductId { get; set; }
        
        public string ProductName { get; set; }
        public string ProductSku { get; set; }
        public string ProductImage { get; set; }

        public string ProductSlug { get; set; }

        public decimal ProductPrice { get; set; }

        public int Quantity { get; set; }

        public int ShippedQuantity { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal TaxPercent { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal Total => Quantity * ProductPrice;

        public decimal TaxIncludedAmount => Total + TaxAmount;

        public decimal RowTotal => Total + TaxAmount - DiscountAmount;

        public string TaxAmountString => _currencyService.FormatCurrency(TaxAmount);

        public string ProductPriceString => _currencyService.FormatCurrency(ProductPrice);

        public string DiscountAmountString => _currencyService.FormatCurrency(DiscountAmount);

        public string TotalString => _currencyService.FormatCurrency(Total);

        public string TaxIncludedAmountString => _currencyService.FormatCurrency(TaxIncludedAmount);

        public string RowTotalString => _currencyService.FormatCurrency(RowTotal);
    }
}
