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
	[Route("api/countries")]
	public class CountryApiController : Controller
	{
		private readonly IRepositoryWithTypedId<Country, string> _countryRepository;
		private readonly ITrackingService _trackingService;

		public CountryApiController(
			IRepositoryWithTypedId<Country,
			string> countryRepository,
			ITrackingService _trackingService)
		{
			_countryRepository = countryRepository;
			this._trackingService = _trackingService;
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] bool? shippingEnabled)
		{
			var query = _countryRepository.Query();
			if (shippingEnabled.HasValue)
			{
				query = query.Where(x => x.IsShippingEnabled == shippingEnabled.Value);
			}
			var countries = await query
				.OrderBy(x => x.Name)
				.Select(x => new
				{
					Id = x.Id,
					Name = x.Name
				}).ToListAsync();
			return Json(countries);
		}

		[HttpPost("grid")]
		public IActionResult List([FromBody] SmartTableParam param)
		{
			var query = _countryRepository.Query();

			if (param.Search.PredicateObject != null)
			{
				dynamic search = param.Search.PredicateObject;

				if (search.Name != null)
				{
					string name = search.Name;
					query = query.Where(x => x.Name.Contains(name));
				}
			}

			var countries = query.ToSmartTableResult(
				param,
				c => new
				{
					c.Id,
					c.Name,
					c.Code3,
					c.IsShippingEnabled,
					c.IsBillingEnabled,
					c.IsCityEnabled,
					c.IsZipCodeEnabled,
					c.IsDistrictEnabled
				});

			return Json(countries);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(string id)
		{
			var country = await _countryRepository.Query().FirstOrDefaultAsync(x => x.Id == id);
			if (country == null)
			{
				return NotFound();
			}

			var model = new CountryForm
			{
				Id = country.Id,
				Name = country.Name,
				Code3 = country.Code3,
				IsBillingEnabled = country.IsBillingEnabled,
				IsShippingEnabled = country.IsShippingEnabled,
				IsCityEnabled = country.IsCityEnabled,
				IsZipCodeEnabled = country.IsZipCodeEnabled,
				IsDistrictEnabled = country.IsDistrictEnabled
			};

			return Json(model);
		}

        [HttpPost("update-country/{id}")]
        [Authorize(Roles = "admin")]
		public async Task<IActionResult> Put(string id, [FromBody] CountryForm model)
		{
			CustomJsonResult customJsonResult = new CustomJsonResult();
			try
			{

				var country = await _countryRepository.Query().FirstOrDefaultAsync(x => x.Id == id);
				if (country == null)
				{
					return NotFound();
				}

				country.Name = model.Name;
				country.Code3 = model.Code3;
				country.IsShippingEnabled = model.IsShippingEnabled;
				country.IsBillingEnabled = model.IsBillingEnabled;
				country.IsCityEnabled = model.IsCityEnabled;
				country.IsZipCodeEnabled = model.IsZipCodeEnabled;
				country.IsDistrictEnabled = model.IsDistrictEnabled;

				await _countryRepository.SaveChangesAsync();

				customJsonResult.Result = country.Id;
				customJsonResult.StatusCode = 200;
			}
			catch (Exception ex)
			{
				_trackingService.TrackingError("PUT country", ex + string.Empty);
				customJsonResult.StatusCode = 502;
			}
			return Ok(customJsonResult);
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> Post([FromBody] CountryForm model)
		{
			CustomJsonResult customJsonResult = new CustomJsonResult();
			try
			{
				if (!ModelState.IsValid)
				{
					await _trackingService.TrackingError("Post Page", "BadRequest");
					return Ok("Fail !!!");
				}
				var country = new Country(model.Id)
				{
					Name = model.Name,
					Code3 = model.Code3,
					IsBillingEnabled = model.IsBillingEnabled,
					IsShippingEnabled = model.IsShippingEnabled,
					IsCityEnabled = model.IsCityEnabled,
					IsZipCodeEnabled = model.IsZipCodeEnabled,
					IsDistrictEnabled = model.IsDistrictEnabled
				};

				_countryRepository.Add(country);
				await _countryRepository.SaveChangesAsync();

				customJsonResult.Result = country.Id;
				customJsonResult.StatusCode = 200;
			}
			catch (Exception ex)
			{
				await _trackingService.TrackingError("Post country", ex.Message);
				customJsonResult.StatusCode = 502;
			}
			return Ok(customJsonResult);
		}


        [HttpPost("delete-country/{id}")]
        [Authorize(Roles = "admin")]
		public async Task<IActionResult> Delete(string id)
		{
			var country = await _countryRepository.Query().FirstOrDefaultAsync(x => x.Id == id);
			if (country == null)
			{
				return NotFound();
			}

			try
			{
				_countryRepository.Remove(country);
				await _countryRepository.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				return BadRequest(new { Error = $"The country {country.Name} can't not be deleted because it is referenced by other tables" });
			}

			return NoContent();
		}
	}
}
