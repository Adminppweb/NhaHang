
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NhaHang.Module.ElFinder.Areas.ElFinder.Controllers
{
    [Area("ElFinder")]
    [Authorize(Roles = "admin, vendor")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class FileManagerController : Controller
    {
        [Route("admin/file-manager")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
