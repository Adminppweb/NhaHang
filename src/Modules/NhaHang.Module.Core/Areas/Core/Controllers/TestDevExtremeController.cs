using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace NhaHang.Module.Core.Areas.Core.Controllers
{
    [Area("Core")]
    public class TestDevExtremeController : Controller
    {
        public TestDevExtremeController()
        {
        }

        /// <summary>
        /// TestDevExtreme/Index
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}