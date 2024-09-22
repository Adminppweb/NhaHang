using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using NhaHang.Infrastructure.Commons;
using NhaHang.Module.Core.Services;

namespace NhaHang.Module.Core.Areas.Core.Controllers
{
    [Area("Core")]
    [Authorize(Roles = "admin")]
    [Route("api/tracking")]
    //[ApiController]
    public class TrackingApiController : Controller
    {
        private readonly ITrackingService _trackingService;
        private readonly IConfigurationRoot _configurationRoot;
        private readonly IMapper _mapper;
        public TrackingApiController(ITrackingService trackingService, IMapper _mapper)
        {
            _trackingService = trackingService;
            this._mapper = _mapper;

        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _trackingService.GetAll());
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = new CustomJsonResult();
            try
            {
                result.Result = await _trackingService.Delete(id);
                result.StatusCode = 200;
            }
            catch (System.Exception ex)
            {
                result.StatusCode = (int)HttpStatusCode.BadRequest;
                result.Message = ex.Message;
            }
            return Ok(result);
        }

        [HttpPost("deleteMultil")]
        public async Task<IActionResult> DeleteMultil(string ids)
        {
            var result = new CustomJsonResult();
            try
            {
                if (!string.IsNullOrEmpty(ids))
                {
                    ids = ids.TrimEnd('#');
                    result.Result = await _trackingService.DeleteMultil(ids);
                }
                result.StatusCode = (int)HttpStatusCode.OK;
            }
            catch (System.Exception ex)
            {
                result.StatusCode = (int)HttpStatusCode.BadRequest;
                result.Message = ex.Message;
                //_trackLog.TrackingError("Page: Role, function: DeleteMultil", ex.Message);
            }
            return Ok(result);
        }
    }
}
