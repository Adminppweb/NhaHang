using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using NhaHang.Infrastructure.Data;
using NhaHang.Infrastructure.Helpers;
using NhaHang.Infrastructure.Web.SmartTable;
using NhaHang.Module.Customers.Areas.Customers.ViewModels;
using NhaHang.Module.Customers.Models;
using NhaHang.Module.Customers.Services;
using NhaHang.Module.Core.Extensions;
using NhaHang.Module.Core.Models;
using NhaHang.Module.Core.Services;
using NhaHang.Infrastructure.Commons;

namespace NhaHang.Module.Customers.Areas.Customers.Controllers
{
    [Area("Customers")]
    [Authorize(Roles = "admin, vendor")]
    [Route("api/evaluates")]
    public class EvaluateApiController : Controller
    {
        private readonly IMediaService _mediaService;
        private readonly IRepository<Evaluate> _evaluateRepository;
        private readonly IEvaluateService _evaluateService;
        private readonly IWorkContext _workContext;
        private readonly IStringLocalizer _localizer;
        private readonly ITrackingService _trackingService;

        public EvaluateApiController(
            IRepository<Evaluate> evaluateRepository,
            IMediaService mediaService,
            IEvaluateService evaluateService,
            IWorkContext workContext,
            IStringLocalizerFactory stringLocalizerFactory, ITrackingService _trackingService)
        {
            _evaluateRepository = evaluateRepository;
            _mediaService = mediaService;
            _evaluateService = evaluateService;
            _workContext = workContext;
            _localizer = stringLocalizerFactory.Create(null);
            this._trackingService = _trackingService;
        }

        [HttpGet("quick-search")]
        public async Task<IActionResult> QuickSearch(string name)
        {
            var query = _evaluateRepository.Query()
                .Where(x => !x.IsDeleted);

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }

            var evaluates = await query.Take(5).Select(x => new
            {
                x.Id,
                x.Name
            }).ToListAsync();

            return Ok(evaluates);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var evaluate = _evaluateRepository.Query()
                .Include(x => x.ThumbnailImage)
                .FirstOrDefault(x => x.Id == id);

            var currentUser = await _workContext.GetCurrentUser();
            if (!User.IsInRole("admin"))
            {
                return BadRequest(new { error = _localizer["You don't have permission to manage this fields item"].Value });
            }

            var evaluateVm = new EvaluateVm
            {
                Id = evaluate.Id,
                Name = evaluate.Name,
                Slug = evaluate.Slug,
                Phone = evaluate.Phone,
                Email = evaluate.Email,
                Address = evaluate.Address,
                Position = evaluate.Position,
                Company = evaluate.Company,
                Content = evaluate.Content,
                IdYoutube = evaluate.IdYoutube,
                SourceYou = evaluate.SourceYou,
                LinkImage = evaluate.LinkImage,
                IsPublished = evaluate.IsPublished,
                ThumbnailImageUrl = _mediaService.GetThumbnailUrl(evaluate.ThumbnailImage),
            };
            return Json(evaluateVm);
        }

        [HttpPost("grid")]
        public async Task<IActionResult> List([FromBody] SmartTableParam param)
        {
            var query = _evaluateRepository.Query().Where(x => !x.IsDeleted);
            var currentUser = await _workContext.GetCurrentUser();

            if (param.Search.PredicateObject != null)
            {
                dynamic search = param.Search.PredicateObject;
                if (search.Name != null)
                {
                    string name = search.Name;
                    query = query.Where(x => x.Name.Contains(name));
                }

                if (search.IsPublished != null)
                {
                    bool isPublished = search.IsPublished;
                    query = query.Where(x => x.IsPublished == isPublished);
                }
            }

            var gridData = query.ToSmartTableResult(
                param,
                x => new EvaluateListItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    LinkImage = x.LinkImage,
                    ThumbnailImageUrl = _mediaService.GetMediaUrl(x.ThumbnailImage),
                    IsPublished = x.IsPublished
                });

            return Json(gridData);
        }

        [HttpPost]
        public async Task<IActionResult> Post(EvaluateForm model)
        {

            CustomJsonResult customJsonResult = new CustomJsonResult();
            try
            {
                if (!ModelState.IsValid)
                {
                    await _trackingService.TrackingError("Post Evaluate", "BadRequest");
                    return Ok("Fail !!!");
                }
                var currentUser = await _workContext.GetCurrentUser();

                var evaluate = new Evaluate
                {
                    Name = model.Evaluate.Name,
                    Slug = model.Evaluate.Slug,
                    Phone = model.Evaluate.Phone,
                    Email = model.Evaluate.Email,
                    Address = model.Evaluate.Address,
                    Position = model.Evaluate.Position,
                    Company = model.Evaluate.Company,
                    Content = model.Evaluate.Content,
                    IdYoutube = model.Evaluate.IdYoutube,
                    SourceYou = model.Evaluate.SourceYou,
                    LinkImage = model.Evaluate.LinkImage,
                    IsPublished = model.Evaluate.IsPublished
                };

                await SaveEvaluateMedias(model, evaluate);
                _evaluateService.Create(evaluate);
                customJsonResult.Result = evaluate.Id;
                customJsonResult.StatusCode = 200;
            }
            catch (Exception ex)
            {
                await _trackingService.TrackingError("Post evaluate", ex.Message);
                customJsonResult.StatusCode = 502;
            }
            return Ok(customJsonResult);
        }

        [HttpPost("update-evaluate/{id}")]
        public async Task<IActionResult> Put(long id, EvaluateForm model)
        {

            CustomJsonResult customJsonResult = new CustomJsonResult();
            try
            {

                var evaluate = _evaluateRepository.Query()
                .Include(x => x.ThumbnailImage)
                .FirstOrDefault(x => x.Id == id);

                if (evaluate == null)
                {
                    return NotFound();
                }

                var currentUser = await _workContext.GetCurrentUser();
                if (!User.IsInRole("admin"))
                {
                    return BadRequest(new { error = _localizer["You don't have permission to manage this fields item"].Value });
                }

                evaluate.Name = model.Evaluate.Name;
                evaluate.Slug = model.Evaluate.Slug;
                evaluate.Phone = model.Evaluate.Phone;
                evaluate.Email = model.Evaluate.Email;
                evaluate.Address = model.Evaluate.Address;
                evaluate.Position = model.Evaluate.Position;
                evaluate.Company = model.Evaluate.Company;
                evaluate.Content = model.Evaluate.Content;
                evaluate.IdYoutube = model.Evaluate.IdYoutube;
                evaluate.SourceYou = model.Evaluate.SourceYou;
                evaluate.LinkImage = model.Evaluate.LinkImage;
                evaluate.IsPublished = model.Evaluate.IsPublished;

                await SaveEvaluateMedias(model, evaluate);

                _evaluateService.Update(evaluate);
                customJsonResult.Result = evaluate.Id;
                customJsonResult.StatusCode = 200;
            }
            catch (Exception ex)
            {
                _trackingService.TrackingError("PUT evaluate", ex + string.Empty);
                customJsonResult.StatusCode = 502;
            }
            return Ok(customJsonResult);
        }

        [HttpPost("delete-evaluate/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var evaluate = _evaluateRepository.Query().FirstOrDefault(x => x.Id == id);
            if (evaluate == null)
            {
                return NotFound();
            }

            var currentUser = await _workContext.GetCurrentUser();
            if (!User.IsInRole("admin"))
            {
                return BadRequest(new { error = _localizer["You don't have permission to manage this fields Item"].Value });
            }

            await _evaluateService.Delete(evaluate);

            return NoContent();
        }
        private async Task SaveEvaluateMedias(EvaluateForm model, Evaluate evaluate)
        {
            if (model.ThumbnailImage != null)
            {
                var fileName = await SaveFile(model.ThumbnailImage);
                if (evaluate.ThumbnailImage != null)
                {
                    evaluate.ThumbnailImage.FileName = fileName;
                }
                else
                {
                    evaluate.ThumbnailImage = new Media { FileName = fileName };
                }
            }

        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Value.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _mediaService.SaveMediaAsync(file.OpenReadStream(), fileName, file.ContentType);
            return fileName;
        }
    }
}
