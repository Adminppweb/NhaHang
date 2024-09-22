using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NhaHang.Infrastructure.Commons;
using NhaHang.Infrastructure.Data;
using NhaHang.Infrastructure.Web.SmartTable;
using NhaHang.Module.Core.Areas.Core.ViewModels;
using NhaHang.Module.Core.Models;
using NhaHang.Module.Core.Services;

namespace NhaHang.Module.Core.Areas.Core.Controllers
{
    [Area("Core")]
    [Authorize(Roles = "admin")]
    [Route("api/customergroups")]
    public class CustomerGroupApiController : Controller
    {
        private readonly IRepository<CustomerGroup> _customerGroupRepository;
		private readonly ITrackingService _trackingService;

		public CustomerGroupApiController(
            IRepository<CustomerGroup> customergroupRepository,
			ITrackingService _trackingService)
        {
            _customerGroupRepository = customergroupRepository;
			this._trackingService = _trackingService;
		}

        [HttpPost("grid")]
        public ActionResult List([FromBody] SmartTableParam param)
        {
            var query = _customerGroupRepository.Query()
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

            var customerGroups = query.ToSmartTableResult(
                param,
                x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    IsActive = x.IsActive,
                    CreatedOn = x.CreatedOn
                });

            return Json(customerGroups);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customerGroups = await _customerGroupRepository.Query().Select(x => new
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return Json(customerGroups);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var customerGroup = await _customerGroupRepository.Query().FirstOrDefaultAsync(x => x.Id == id);
            if(customerGroup == null)
            {
                return NotFound();
            }

            var model = new CustomerGroupForm
            {
                Id = customerGroup.Id,
                Name = customerGroup.Name,
                Description = customerGroup.Description,
                IsActive = customerGroup.IsActive
            };

            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerGroupForm model)
        {
			CustomJsonResult customJsonResult = new CustomJsonResult();
			try
			{
				if (!ModelState.IsValid)
				{
					await _trackingService.TrackingError("Post customerGroup", "BadRequest");
					return Ok("Fail !!!");
				}
				var customerGroup = new CustomerGroup
                {
                    Name = model.Name,
                    Description = model.Description,
                    IsActive = model.IsActive
                };

                _customerGroupRepository.Add(customerGroup);
                await _customerGroupRepository.SaveChangesAsync();
				customJsonResult.Result = customerGroup.Id;
				customJsonResult.StatusCode = 200;
			}
			catch (Exception ex)
			{
				await _trackingService.TrackingError("Post customerGroup", ex.Message);
				customJsonResult.StatusCode = 502;
			}
			return Ok(customJsonResult);
		}

        [HttpPost("update-customer-group/{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] CustomerGroupForm model)
        {
			CustomJsonResult customJsonResult = new CustomJsonResult();
			try
			{
				var customerGroup = await _customerGroupRepository.Query().FirstOrDefaultAsync(x => x.Id == id);
                if(customerGroup == null)
                {
                    return NotFound();
                }

                customerGroup.Name = model.Name;
                customerGroup.Description = model.Description;
                customerGroup.IsActive = model.IsActive;
                customerGroup.LatestUpdatedOn = DateTimeOffset.Now;

                await  _customerGroupRepository.SaveChangesAsync();
				customJsonResult.Result = customerGroup.Id;
				customJsonResult.StatusCode = 200;
			}
			catch (Exception ex)
			{
				_trackingService.TrackingError("PUT customerGroup", ex + string.Empty);
				customJsonResult.StatusCode = 502;
			}
			return Ok(customJsonResult);
		}

        [HttpPost("delete-customer-group/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var customerGroup = await _customerGroupRepository.Query().FirstOrDefaultAsync(x => x.Id == id);
            if (customerGroup == null)
            {
                return NotFound();
            }

            customerGroup.IsDeleted = true;
            await  _customerGroupRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}
