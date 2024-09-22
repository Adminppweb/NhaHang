using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using NhaHang.Infrastructure.Data;
using NhaHang.Infrastructure.Web;
using NhaHang.Infrastructure.Web.SmartTable;
using NhaHang.Module.Core.Extensions;
using NhaHang.Module.Core.Models;

namespace NhaHang.Module.Core.Areas.Core.Components
{
    public class PaginationViewComponent : ViewComponent
    {
        private readonly IRepository<UserAddress> _userAddressRepository;
        private readonly IWorkContext _workContext;
        private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;
        public PaginationViewComponent(IRepository<UserAddress> userAddressRepository, IWorkContext workContext, Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor)
        {
            _userAddressRepository = userAddressRepository;
            _workContext = workContext;
            this._httpContextAccessor = _httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync(Pagination pagination = null)
        {
            if (pagination == null)
            {
                pagination = new Pagination();
            }

            return View(this.GetViewPath(), pagination);
        }
    }
}