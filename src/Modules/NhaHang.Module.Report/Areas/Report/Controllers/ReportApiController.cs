using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NhaHang.Infrastructure.Web.SmartTable;
using NhaHang.Module.Core.Extensions;
using NhaHang.Module.Core.Services;

namespace NhaHang.Module.Report.Areas.Report.Controllers
{
    [Area("Contacts")]
    [Authorize(Roles = "admin")]
    [Route("api/contacts")]
    public class ReportApiController : Controller
    {
        private readonly IMediaService _mediaService;
        private readonly IWorkContext _workContext;

        public ReportApiController(IMediaService mediaService, IWorkContext workContext)
        {

            _mediaService = mediaService;
            _workContext = workContext;
        }

        [HttpPost("grid")]
        public IActionResult Get([FromBody] SmartTableParam param)
        {

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            return Ok();
        }

    }
}
