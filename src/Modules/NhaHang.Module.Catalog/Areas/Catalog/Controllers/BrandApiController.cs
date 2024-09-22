using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NhaHang.Infrastructure.Commons;
using NhaHang.Infrastructure.Data;
using NhaHang.Module.Catalog.Areas.Catalog.ViewModels;
using NhaHang.Module.Catalog.Models;
using NhaHang.Module.Catalog.Services;
using NhaHang.Module.Core.Services;

namespace NhaHang.Module.Catalog.Areas.Catalog.Controllers
{
    [Area("Catalog")]
    [Authorize(Roles = "admin, vendor")]
    [Route("api/brands")]
    [ApiController]
    public class BrandApiController : ControllerBase
    {
        private readonly IRepository<Brand> _brandRepository;
        private readonly IBrandService _brandService;
        private readonly ITrackingService _trackingService;

        public BrandApiController(IRepository<Brand> brandRepository,
            IBrandService brandService, ITrackingService _trackingService)
        {
            _brandRepository = brandRepository;
            _brandService = brandService;
            this._trackingService = _trackingService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<BrandVm>>> Get()
        {
            var brands = await _brandRepository.Query()
            .Where(x => !x.IsDeleted)
            .Select(x => new BrandVm
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                IsPublished = x.IsPublished
            }).ToListAsync();

            return brands;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BrandVm>> Get(long id)
        {
            var brand = await _brandRepository.Query().FirstOrDefaultAsync(x => x.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            var model = new BrandVm
            {
                Id = brand.Id,
                Name = brand.Name,
                Slug = brand.Slug,
                MetaTitle = brand.MetaTitle,
                MetaKeywords = brand.MetaKeywords,
                MetaDescription = brand.MetaDescription,
                DisplayOrder = brand.DisplayOrder,
                Description = brand.Description,
                IncludeInMenu = brand.IncludeInMenu,
                IsPublished = brand.IsPublished
            };

            return model;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Post([FromBody] BrandForm model)
        {
            CustomJsonResult customJsonResult = new CustomJsonResult();
            try
            {
                if (!ModelState.IsValid)
                {
                    await _trackingService.TrackingError("Post Brand", "BadRequest");
                    return Ok("Fail !!!");
                }
                var brand = new Brand
                {
                    Name = model.Name,
                    Slug = model.Slug,
                    MetaTitle = model.MetaTitle,
                    MetaKeywords = model.MetaKeywords,
                    MetaDescription = model.MetaDescription,
                    DisplayOrder = model.DisplayOrder,
                    Description = model.Description,
                    IncludeInMenu = model.IncludeInMenu,
                    IsPublished = model.IsPublished
                };

                await _brandService.Create(brand);
                customJsonResult.Result = brand.Id;
                customJsonResult.StatusCode = 200;
            }
            catch (Exception ex)
            {
                await _trackingService.TrackingError("Post Brand", ex.Message);
                customJsonResult.StatusCode = 502;
            }
            return Ok(customJsonResult);
        }

        [HttpPost("update-brand/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Put(long id, [FromBody] BrandForm model)
        {
            CustomJsonResult customJsonResult = new CustomJsonResult();
            try
            {
                var brand = _brandRepository.Query().FirstOrDefault(x => x.Id == id);
                if (brand == null)
                {
                    return NotFound();
                }

                brand.Name = model.Name;
                brand.Slug = model.Slug;
                brand.MetaTitle = model.MetaTitle;
                brand.MetaKeywords = model.MetaKeywords;
                brand.MetaDescription = model.MetaDescription;
                brand.DisplayOrder = model.DisplayOrder;
                brand.Description = model.Description;
                brand.IncludeInMenu = model.IncludeInMenu;
                brand.IsPublished = model.IsPublished;

                await _brandService.Update(brand);
                customJsonResult.Result = brand.Id;
                customJsonResult.StatusCode = 200;
            }
            catch (Exception ex)
            {
                _trackingService.TrackingError("PUT brand", ex + string.Empty);
                customJsonResult.StatusCode = 502;
            }
            return Ok(customJsonResult);
        }

        [HttpPost("delete-brand/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(long id)
        {
            var brand = _brandRepository.Query().FirstOrDefault(x => x.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            await _brandService.Delete(brand);
            return NoContent();
        }
    }
}