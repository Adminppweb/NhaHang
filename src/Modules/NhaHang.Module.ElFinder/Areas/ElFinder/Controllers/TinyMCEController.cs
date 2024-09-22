using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NhaHang.Module.ElFinder.Areas.ElFinder.Controllers
{
    [Area("ElFinder")]
	[Route("tiny-mce")]
    public class TinyMCEController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("browse")]
        public IActionResult Browse()
        {
            return View();
        }
    }
}