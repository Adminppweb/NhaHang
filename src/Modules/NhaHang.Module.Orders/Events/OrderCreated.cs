using MediatR;
using NhaHang.Module.Orders.Models;

namespace NhaHang.Module.Orders.Events
{
    /// <summary>
    /// This event raised after an order has created and the transtaction not commited
    /// </summary>
    public class OrderCreated : INotification
    {
        public OrderCreated(Order order)
        {
            Order = order;
        }

        public Order Order { get; }
    }
}
