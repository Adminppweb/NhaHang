using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NhaHang.Infrastructure.Data;
using NhaHang.Module.Core.Services;
using NhaHang.Module.Payments.Models;

namespace NhaHang.Module.Payments.Areas.Payments.Controllers
{
    [Area("Payments")]
    [Authorize(Roles = "admin")]
    [Route("api/payments")]
    [ApiController]
    public class PaymentApiController : Controller
    {
        private readonly IRepository<Payment> _paymentRepository;
        private readonly ICurrencyService _currencyService;

        public PaymentApiController(IRepository<Payment> paymentRepository, ICurrencyService currencyService)
        {
            _paymentRepository = paymentRepository;
            _currencyService = currencyService;
        }

        [HttpGet("/api/orders/{orderId}/payments")]
        public async Task<IActionResult> GetByOrder(long orderId)
        {
            var payments = await _paymentRepository.Query()
                .Where(x => x.OrderId == orderId)
                .Select(x => new
                {
                    x.Id,
                    x.Amount,
                    AmountString = _currencyService.FormatCurrency(x.Amount),
                    x.PaymentFee,
                    PaymentFeeString = _currencyService.FormatCurrency(x.PaymentFee),
                    x.OrderId,
                    x.GatewayTransactionId,
                    Status = x.Status.ToString(),
                    x.CreatedOn
                }).ToListAsync();

            return Ok(payments);
        }
    }
}