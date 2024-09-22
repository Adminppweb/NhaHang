using MediatR;
using NhaHang.Module.Orders.Areas.Orders.ViewModels;

namespace NhaHang.Module.Orders.Events
{
    public class OrderDetailGot : INotification
    {
        public OrderDetailVm OrderDetailVm { get; set; }
    }
}
