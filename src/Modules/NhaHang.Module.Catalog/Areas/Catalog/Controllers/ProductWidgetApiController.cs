using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NhaHang.Infrastructure.Data;
using NhaHang.Infrastructure.Helpers;
using NhaHang.Module.Catalog.Areas.Catalog.ViewModels;
using NhaHang.Module.Core.Models;

namespace NhaHang.Module.Catalog.Areas.Catalog.Controllers
{
    [Area("Catalog")]
    [Authorize(Roles = "admin")]
    [Route("api/product-widgets")]
    public class ProductWidgetApiController : Controller
    {
        private readonly IRepository<WidgetInstance> _widgetInstanceRepository;

        public ProductWidgetApiController(IRepository<WidgetInstance> widgetInstanceRepository)
        {
            _widgetInstanceRepository = widgetInstanceRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var widgetInstance = _widgetInstanceRepository.Query().FirstOrDefault(x => x.Id == id);
            var model = new ProductWidgetForm
            {
                Id = widgetInstance.Id,
                Name = widgetInstance.Name,
                SubName = widgetInstance.SubName,
                Description = widgetInstance.Description,
                WidgetZoneId = widgetInstance.WidgetZoneId,
                PublishStart = widgetInstance.PublishStart,
                PublishEnd = widgetInstance.PublishEnd,
                DisplayOrder = widgetInstance.DisplayOrder,
                Setting = JsonConvert.DeserializeObject<ProductWidgetSetting>(widgetInstance.Data)
            };

            var enumMetaData = MetadataProvider.GetMetadataForType(typeof(ProductWidgetOrderBy));
            return Json(model);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductWidgetForm model)
        {
            if (ModelState.IsValid)
            {
                var widgetInstance = new WidgetInstance
                {
                    Name = model.Name,
                    SubName = model.SubName,
                    Description = model.Description,
                    WidgetId = "ProductWidget",
                    WidgetZoneId = model.WidgetZoneId,
                    PublishStart = model.PublishStart,
                    PublishEnd = model.PublishEnd,
                    DisplayOrder = model.DisplayOrder,
                    Data = JsonConvert.SerializeObject(model.Setting)
                };

                _widgetInstanceRepository.Add(widgetInstance);
                _widgetInstanceRepository.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = widgetInstance.Id }, null);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("update-product-widget/{id}")]
        public IActionResult Put(long id, [FromBody] ProductWidgetForm model)
        {
            if (ModelState.IsValid)
            {
                var widgetInstance = _widgetInstanceRepository.Query().FirstOrDefault(x => x.Id == id);
                widgetInstance.Name = model!.Name;
                widgetInstance.SubName = model!.SubName;
                widgetInstance.Description = model!.Description;
                widgetInstance.WidgetZoneId = model!.WidgetZoneId;
                widgetInstance.PublishStart = model!.PublishStart;
                widgetInstance.PublishEnd = model!.PublishEnd;
                widgetInstance.DisplayOrder = model!.DisplayOrder;
                widgetInstance.Data = JsonConvert.SerializeObject(model.Setting);

                _widgetInstanceRepository.SaveChanges();
                return Accepted();
            }

            return BadRequest(ModelState);
        }

        [HttpGet("available-orderby")]
        public IActionResult GetAvailableOrderBy()
        {
            var model = EnumHelper.ToDictionary(typeof(ProductWidgetOrderBy)).Select(x => new { Id = x.Key.ToString(), Name = x.Value });
            return Json(model);
        }
    }
}
