using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using NhaHang.Infrastructure.Data;
using NhaHang.Module.Core.Extensions;
using NhaHang.Module.Core.Services;
using NhaHang.Module.Report.Models;

namespace NhaHang.Module.Report.Areas.Report.Controllers
{
    [Area("Contacts")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ReportController : Controller
    {
        private readonly IRepository<Models.Report> _contactRepository;
        private readonly IRepository<ReportItem> _contactAreaRepository;
        private readonly IWorkContext _workContext;
        private readonly IContentLocalizationService _contentLocalizationService;

        public ReportController(IRepository<Models.Report> contactRepository, IRepository<ReportItem> contactAreaRepository, IWorkContext workContext, IContentLocalizationService contentLocalizationService)
        {
            _contactRepository = contactRepository;
            _contactAreaRepository = contactAreaRepository;
            _workContext = workContext;
            _contentLocalizationService = contentLocalizationService;
        }


        public async Task<IActionResult> Index()
        {

            return View();
        }

    }
}
