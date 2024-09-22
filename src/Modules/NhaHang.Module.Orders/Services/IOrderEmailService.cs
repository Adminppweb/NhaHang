using System.Threading.Tasks;
using NhaHang.Module.Core.Models;
using NhaHang.Module.Orders.Models;

namespace NhaHang.Module.Orders.Services
{
    public interface IOrderEmailService
    {
        Task SendEmailToUser(User user, Order order);
    }
}
