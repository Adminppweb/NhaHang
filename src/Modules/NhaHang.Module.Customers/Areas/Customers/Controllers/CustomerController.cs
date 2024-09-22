using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NhaHang.Infrastructure.Data;
using NhaHang.Module.Customers.Areas.Customers.ViewModels;
using NhaHang.Module.Customers.Models;
using NhaHang.Module.Core.Extensions;
using NhaHang.Module.Core.Services;

namespace NhaHang.Module.Customers.Areas.Customers.Controllers
{
    [Area("Customers")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CustomerController : Controller
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IWorkContext _workContext;
        private readonly IEmailSender _emailSender;
        private readonly IContentLocalizationService _contentLocalizationService;

        public CustomerController(
            IRepository<Customer> customerRepository,
            IWorkContext workContext,
            IEmailSender emailSender,
            IContentLocalizationService contentLocalizationService)
        {
            _customerRepository = customerRepository;
            _workContext = workContext;
            _emailSender = emailSender;
            _contentLocalizationService = contentLocalizationService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new CustomerVm();

            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await _workContext.GetCurrentUser();

                model.Name = currentUser.FullName;
                model.Phone = currentUser.PhoneNumber;
            }

            return View(model);
        }

        [HttpPost("customer")]
        public async Task<IActionResult> SubmitCustomer(CustomerVm model)
        {
            if (ModelState.IsValid)
            {
                var customer = new Models.Customer()
                {
                    Name = model.Name,
                    Phone = model.Phone,
                    Email = model.Email,
                    Content = model.Content
                };
                _customerRepository.Add(customer);
                await _customerRepository.SaveChangesAsync();
                var emailBody = $"Thông tin khách hàng cần tư vấn:\n\n" +
                                $"Tên: {model.Name}\n" +
                                $"Điện thoại: {model.Phone}\n" +
                                $"Email: {model.Email}\n" +
                                $"Yêu cầu: {model.Content}\n";
                await _emailSender.SendEmailAsync("phamvancuong1992th@gmail.com", "Khách hàng cần tư vấn", emailBody);
                return View("SubmitCustomerResult", model);
            }
            return View("Index", model);
        }
    }
}
