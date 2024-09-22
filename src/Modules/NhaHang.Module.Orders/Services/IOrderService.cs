using System.Threading.Tasks;
using NhaHang.Infrastructure;
using NhaHang.Module.Core.Models;
using NhaHang.Module.Orders.Areas.Orders.ViewModels;
using NhaHang.Module.Orders.Models;

namespace NhaHang.Module.Orders.Services
{
    public interface IOrderService
    {
        Task<Result<Order>> CreateOrder(long cartId, string paymentMethod, decimal paymentFeeAmount, OrderStatus orderStatus = OrderStatus.New);

        Task<Result<Order>> CreateOrder(long cartId, string paymentMethod, decimal paymentFeeAmount, string shippingMethod, Address billingAddress, Address shippingAddress, OrderStatus orderStatus = OrderStatus.New);

        void CancelOrder(Order order);

        Task<decimal> GetTax(long cartId, string countryId, long stateOrProvinceId, string zipCode);

        Task<OrderTaxAndShippingPriceVm> UpdateTaxAndShippingPrices(long cartId, TaxAndShippingPriceRequestVm model);
    }
}
