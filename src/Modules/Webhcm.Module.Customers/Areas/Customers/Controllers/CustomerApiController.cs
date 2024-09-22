using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NhaHang.Infrastructure.Data;
using NhaHang.Infrastructure.Web.SmartTable;
using NhaHang.Module.Customers.Areas.Customers.ViewModels;
using NhaHang.Module.Customers.Models;
using NhaHang.Module.Core.Extensions;
using NhaHang.Module.Core.Services;

namespace NhaHang.Module.Customers.Areas.Customers.Controllers
{
    [Area("Customers")]
    [Authorize(Roles = "admin")]
    [Route("api/customers")]
    public class CustomerApiController : Controller
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IMediaService _mediaService;
        private readonly IWorkContext _workContext;

        public CustomerApiController(IRepository<Customer> customerRepository, IMediaService mediaService, IWorkContext workContext)
        {
            _customerRepository = customerRepository;
            _mediaService = mediaService;
            _workContext = workContext;
        }

        [HttpPost("grid")]
        public IActionResult Get([FromBody] SmartTableParam param)
        {
            var query = _customerRepository.Query()
                .Where(x => !x.IsDeleted);

            if (param.Search.PredicateObject != null)
            {
                dynamic search = param.Search.PredicateObject;

                if (search.Name != null)
                {
                    string name = search.Name;
                    query = query.Where(x => x.Name.Contains(name));
                }

                if (search.CreatedOn != null)
                {
                    if (search.CreatedOn.before != null)
                    {
                        DateTimeOffset before = search.CreatedOn.before;
                        query = query.Where(x => x.CreatedOn <= before);
                    }

                    if (search.CreatedOn.after != null)
                    {
                        DateTimeOffset after = search.CreatedOn.after;
                        query = query.Where(x => x.CreatedOn >= after);
                    }
                }
            }

            var customers = query.ToSmartTableResult(
                param,
                x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedOn = x.CreatedOn,
                    Phone = x.Phone,
                    Email = x.Email,
                    Content = x.Content
                });
            return Json(customers);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var customer = _customerRepository.Query()
               .FirstOrDefault(x => x.Id == id);

            var model = new CustomerForm()
            {
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email,
                Content = customer.Content,
                CreatedOn = customer.CreatedOn
            };

            return Json(model);
        }

        [HttpPost("delete-customer/{id}")]
        public IActionResult Delete(long id)
        {
            var customer = _customerRepository.Query().FirstOrDefault(x => x.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            customer.IsDeleted = true;
            _customerRepository.SaveChanges();

            return Ok();
        }
    }
}
