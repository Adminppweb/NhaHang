using System.Diagnostics.Metrics;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
	[Route("api/states-provinces")]
	public class StateOrProvinceApiController : Controller
	{
		private readonly IRepository<StateOrProvince> _stateOrProvinceRepository;
		private readonly IRepositoryWithTypedId<Country, string> _countryRepository;
		private readonly ITrackingService _trackingService;

		public StateOrProvinceApiController(
			IRepository<StateOrProvince> stateOrProvinceRepository,
			IRepositoryWithTypedId<Country, string> countryRepository,
			ITrackingService _trackingService)
		{
			_stateOrProvinceRepository = stateOrProvinceRepository;
			_countryRepository = countryRepository;
			this._trackingService = _trackingService;
		}

		[HttpGet("/api/countries/{countryId}/states-provinces")]
		public async Task<IActionResult> GetStatesOrProvinces(string countryId)
		{
			var statesOrProvinces = await _stateOrProvinceRepository.Query()
				.Where(x => x.CountryId == countryId)
				.OrderBy(x => x.Name)
				.Select(x => new
				{
					Id = x.Id,
					Name = x.Name
				})
				.ToListAsync();

			return Ok(statesOrProvinces);
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var statesOrProvinces = await _stateOrProvinceRepository.Query()
				.OrderBy(x => x.Name)
				.Select(x => new
				{
					x.Id,
					x.Name
				})
				.ToListAsync();

			return Ok(statesOrProvinces);
		}

		[HttpPost("grid")]
		public IActionResult List(string countryId, [FromBody] SmartTableParam param)
		{
			var query = _stateOrProvinceRepository.Query()
				.Where(sp => sp.CountryId == countryId);

			if (param.Search.PredicateObject != null)
			{
				dynamic search = param.Search.PredicateObject;

				if (search.Name != null)
				{
					string name = search.Name;
					query = query.Where(x => x.Name.Contains(name));
				}
			}

			var stateProvinces = query.ToSmartTableResult(
				param,
				 sp => new
				 {
					 sp.Id,
					 sp.Name,
					 sp.Code,
					 sp.CountryId
				 });

			return Json(stateProvinces);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(long id)
		{
			var stateProvince = await _stateOrProvinceRepository.Query().FirstOrDefaultAsync(x => x.Id == id);
			if (stateProvince == null)
			{
				return NotFound();
			}

			var model = new StateOrProvinceForm
			{
				Id = stateProvince.Id,
				Name = stateProvince.Name,
				Code = stateProvince.Code,
				CountryId = stateProvince.CountryId,
				Type = stateProvince.Type
			};

			return Json(model);
		}

        [HttpPost("update-state-province/{id}")]
        [Authorize(Roles = "admin")]
		public async Task<IActionResult> Put(long id, [FromBody] StateOrProvinceForm model)
		{
			CustomJsonResult customJsonResult = new CustomJsonResult();
			try
			{

				var stateProvince = await _stateOrProvinceRepository.Query().FirstOrDefaultAsync(x => x.Id == id);
				if (stateProvince == null)
				{
					return NotFound();
				}

				stateProvince.Name = model.Name;
				stateProvince.Code = model.Code;
				stateProvince.Type = model.Type;

				await _stateOrProvinceRepository.SaveChangesAsync();
				customJsonResult.Result = stateProvince.Id;
				customJsonResult.StatusCode = 200;
			}
			catch (Exception ex)
			{
				_trackingService.TrackingError("PUT stateProvince", ex + string.Empty);
				customJsonResult.StatusCode = 502;
			}
			return Ok(customJsonResult);
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> Post([FromBody] StateOrProvinceForm model)
		{
			CustomJsonResult customJsonResult = new CustomJsonResult();
			try
			{
				if (!ModelState.IsValid)
				{
					await _trackingService.TrackingError("Post StateOrProvince", "BadRequest");
					return Ok("Fail !!!");
				}
				var country = await _countryRepository.Query().FirstOrDefaultAsync(x => x.Id == model.CountryId);
				if (country == null)
				{
					return NotFound();
				}

				var stateProvince = new StateOrProvince
				{
					Name = model.Name,
					Code = model.Code,
					CountryId = country.Id,
					Country = country,
					Type = model.Type
				};
				_stateOrProvinceRepository.Add(stateProvince);
				await _stateOrProvinceRepository.SaveChangesAsync();
				customJsonResult.Result = stateProvince.Id;
				customJsonResult.StatusCode = 200;
			}
			catch (Exception ex)
			{
				await _trackingService.TrackingError("Post stateProvince", ex.Message);
				customJsonResult.StatusCode = 502;
			}
			return Ok(customJsonResult);
		}

        [HttpPost("delete-state-province/{id}")]
        [Authorize(Roles = "admin")]
		public async Task<IActionResult> Delete(long id)
		{
			var stateProvince = await _stateOrProvinceRepository.Query().FirstOrDefaultAsync(x => x.Id == id);
			if (stateProvince == null)
			{
				return NotFound();
			}

			try
			{
				_stateOrProvinceRepository.Remove(stateProvince);
				await _stateOrProvinceRepository.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				return BadRequest(new { Error = $"Không thể xóa tiểu bang hoặc tỉnh {stateProvince.Name} vì nó được tham chiếu bởi các bảng khác" });
			}

			return NoContent();
		}
	}
}
