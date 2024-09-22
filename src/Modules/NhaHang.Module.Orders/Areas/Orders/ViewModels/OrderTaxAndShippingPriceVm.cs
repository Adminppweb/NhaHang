using System.Collections.Generic;
using NhaHang.Module.ShoppingCart.Areas.ShoppingCart.ViewModels;

namespace NhaHang.Module.Orders.Areas.Orders.ViewModels
{
    public class OrderTaxAndShippingPriceVm
    {
        public bool IsProductPriceIncludedTax { get; set; }

        // public IList<ShippingPrice> ShippingPrices { get; set; }

        public string SelectedShippingMethodName { get; set; }

        public CartVm Cart { get; set; }
    }
}