using System.Threading.Tasks;
using NhaHang.Infrastructure.Web;
using NhaHang.Module.Core.Models;
using NhaHang.Module.Core.Services;
using NhaHang.Module.Orders.Models;

namespace NhaHang.Module.Orders.Services
{
    public class OrderEmailService : IOrderEmailService
    {
        private readonly IEmailSender _emailSender;
        private readonly IRazorViewRenderer _viewRender;

        public OrderEmailService(IEmailSender emailSender, IRazorViewRenderer viewRender)
        {
            _emailSender = emailSender;
            _viewRender = viewRender;
        }

        public async Task SendEmailToUser(User user, Order order)
        {
            var emailBody = await _viewRender.RenderViewToStringAsync("/Areas/Orders/Views/EmailTemplates/OrderEmailToCustomer.cshtml", order);
            var emailSubject = $"Thông tin đơn hàng #{order.Id}";
            await _emailSender.SendEmailAsync(user.Email, emailSubject, emailBody, true);
        }
    }
}
