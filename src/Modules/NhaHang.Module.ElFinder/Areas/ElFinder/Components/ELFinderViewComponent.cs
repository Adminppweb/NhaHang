using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using NhaHang.Infrastructure.Data;
using NhaHang.Infrastructure.Web;
using NhaHang.Infrastructure.Web.SmartTable;

namespace NhaHang.Module.Core.Areas.Core.Components
{
    public class ELFinderViewComponent : ViewComponent
    {
        //private readonly IRepository<UserAddress> _userAddressRepository;
        //private readonly IWorkContext _workContext;
        //private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;

        public ELFinderViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(this.GetViewPath());
        }
    }
}