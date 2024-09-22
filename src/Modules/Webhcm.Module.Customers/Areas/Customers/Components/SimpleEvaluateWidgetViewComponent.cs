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
    public class SimpleEvaluateWidgetViewComponent : ViewComponent
    {
        private readonly IRepository<Evaluate> _evaluateRepository;
        private readonly IMediaService _mediaService;
        private readonly IContentLocalizationService _contentLocalizationService;

        public SimpleEvaluateWidgetViewComponent(IRepository<Evaluate> evaluateRepository,
            IMediaService mediaService,
            IContentLocalizationService contentLocalizationService)
        {
            _evaluateRepository = evaluateRepository;
            _mediaService = mediaService;
            _contentLocalizationService = contentLocalizationService;
        }

        public IViewComponentResult Invoke(WidgetInstanceViewModel widgetInstance)
        {
            var model = new SimpleEvaluateWidgetComponentVm
            {
                Id = widgetInstance.Id,
                WidgetName = _contentLocalizationService.GetLocalizedProperty(nameof(WidgetInstance), widgetInstance.Id, nameof(widgetInstance.Name), widgetInstance.Name),
                WidgetSubName = widgetInstance.SubName,
                WidgetDescription = widgetInstance.Description,
                Setting = JsonConvert.DeserializeObject<SimpleEvaluateWidgetSetting>(widgetInstance.Data)
            };

            foreach (var item in model.Setting.Evaluates)
            {
                var evaluate = _evaluateRepository.Query().Where(x => x.Id == item.Id).Include(x => x.ThumbnailImage).FirstOrDefault();

                if (evaluate != null)
                {
                    var evaluateThumbnail = EvaluateThumbnail.FromEvaluate(evaluate);
                    evaluateThumbnail.Name = _contentLocalizationService.GetLocalizedProperty(nameof(Evaluate), evaluateThumbnail.Id, nameof(evaluate.Name), evaluateThumbnail.Name);
                    evaluateThumbnail.ThumbnailUrl = _mediaService.GetThumbnailUrl(evaluate.ThumbnailImage);
                    model.Evaluates.Add(evaluateThumbnail);
                }
            }

            return View(this.GetViewPath(), model);
        }
    }
}
