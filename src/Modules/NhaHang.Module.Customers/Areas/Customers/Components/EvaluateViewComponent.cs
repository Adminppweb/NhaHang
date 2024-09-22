using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NhaHang.Infrastructure.Data;
using NhaHang.Infrastructure.Web;
using NhaHang.Module.Customers.Areas.Customers.ViewModels;
using NhaHang.Module.Customers.Models;
using NhaHang.Module.Customers.Services;
using NhaHang.Module.Core.Services;

namespace NhaHang.Module.Customers.Areas.Customers.Components
{
    public class EvaluateViewComponent : ViewComponent
    {
        private readonly IRepository<Evaluate> _evaluateRepository;
        private readonly IMediaService _mediaService;
        private readonly IContentLocalizationService _contentLocalizationService;

        public EvaluateViewComponent(IRepository<Evaluate> evaluateRepository,
            IMediaService mediaService,
            IContentLocalizationService contentLocalizationService)
        {
            _evaluateRepository = evaluateRepository;
            _mediaService = mediaService;
            _contentLocalizationService = contentLocalizationService;
        }

        public async Task<IViewComponentResult> InvokeAsync(long? evaluateId, int itemCount = 10)
        {
            IQueryable<Evaluate> query = _evaluateRepository.Query()
              .Where(x => x.IsPublished && !x.IsDeleted)
                .Include(x => x.ThumbnailImage);
            if (evaluateId.HasValue)
            {
                query = query.Where(x => x.Id != evaluateId.Value);
            }

            var model = query.Take(itemCount)
                .Select(x => EvaluateThumbnail.FromEvaluate(x)).ToList();

            foreach (var evaluate in model)
            {
                evaluate.Name = _contentLocalizationService.GetLocalizedProperty(nameof(Evaluate), evaluate.Id, nameof(evaluate.Name), evaluate.Name);
                evaluate.ThumbnailUrl = _mediaService.GetThumbnailUrl(evaluate.ThumbnailImage);
            }

            return View(this.GetViewPath(), model);
        }
    }
}
