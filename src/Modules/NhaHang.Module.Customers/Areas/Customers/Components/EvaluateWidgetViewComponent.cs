using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NhaHang.Infrastructure.Data;
using NhaHang.Infrastructure.Web;
using NhaHang.Module.Customers.Areas.Customers.ViewModels;
using NhaHang.Module.Customers.Models;
using NhaHang.Module.Core.Areas.Core.ViewModels;
using NhaHang.Module.Core.Models;
using NhaHang.Module.Core.Services;

namespace NhaHang.Module.Customers.Areas.Customers.Components
{
    public class EvaluateWidgetViewComponent : ViewComponent
    {
        private readonly IRepository<Evaluate> _evaluateRepository;
        private readonly IMediaService _mediaService;
        private readonly IContentLocalizationService _contentLocalizationService;

        public EvaluateWidgetViewComponent(IRepository<Evaluate> evaluateRepository,
            IMediaService mediaService,
            IContentLocalizationService contentLocalizationService)
        {
            _evaluateRepository = evaluateRepository;
            _mediaService = mediaService;
            _contentLocalizationService = contentLocalizationService;
        }

        public IViewComponentResult Invoke(WidgetInstanceViewModel widgetInstance)
        {
            var model = new EvaluateWidgetComponentVm
            {
                Id = widgetInstance.Id,
                WidgetName = _contentLocalizationService.GetLocalizedProperty(nameof(WidgetInstance), widgetInstance.Id, nameof(widgetInstance.Name), widgetInstance.Name),
                WidgetSubName = widgetInstance.SubName,
                WidgetDescription = widgetInstance.Description,
                Setting = JsonConvert.DeserializeObject<EvaluateWidgetSetting>(widgetInstance.Data)
            };

            var query = _evaluateRepository.Query()
              .Where(x => x.IsPublished && !x.IsDeleted);

            model.Evaluates = query
              .Include(x => x.ThumbnailImage)
              .Take(model.Setting.NumberOfEvaluates)
              .Select(x => EvaluateThumbnail.FromEvaluate(x)).ToList(); 


			foreach (var evaluate in model.Evaluates)
            {
                evaluate.Name = _contentLocalizationService.GetLocalizedProperty(nameof(Evaluate), evaluate.Id, nameof(evaluate.Name), evaluate.Name);
                evaluate.Name = evaluate.Name;
                evaluate.Position = evaluate.Position;
                evaluate.Company = evaluate.Company;
                evaluate.Content = evaluate.Content;
                evaluate.ThumbnailUrl = _mediaService.GetThumbnailUrl(evaluate.ThumbnailImage);
            }
            return View(this.GetViewPath(), model);
        }

    }
}
