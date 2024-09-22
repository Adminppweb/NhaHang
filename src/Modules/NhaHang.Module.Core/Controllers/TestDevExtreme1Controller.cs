using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Webhcm.Module.Core.Controllers
{
    public class TestDevExtreme1Controller : Controller
    {
        public TestDevExtreme1Controller()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}