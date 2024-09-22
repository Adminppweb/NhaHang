using MediatR;
using NhaHang.Module.Orders.Models;

namespace NhaHang.Module.Orders.Events
{
    /// <summary>
    /// This event raised after an order has successfully created and the transtaction already commited
    /// </summary>
    public class AfterOrderCreated : INotification
    {
        public AfterOrderCreated(Order order)
        {
            Order = order;
        }

        public Order Order { get; }
    }
}
