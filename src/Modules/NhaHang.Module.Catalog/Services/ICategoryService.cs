
using System.Threading.Tasks;
using System.Collections.Generic;
using NhaHang.Module.Catalog.Areas.Catalog.ViewModels;
using NhaHang.Module.Catalog.Models;
using NhaHang.Infrastructure.Web.SmartTable;

namespace NhaHang.Module.Catalog.Services
{
    public interface ICategoryService
    {
        Task<List<ProductThumbnail>> GetProductFilter(List<Filter> shopCartFilterVm = null);
        Task<List<ProductThumbnail>> GetlayoutProductFilter(List<Filter> shopCartFilterVm = null);
        Task<IList<CategoryListItem>> GetAll();

        Task Create(Category category);

        Task Update(Category category);

        Task Delete(Category category);
    }
}
