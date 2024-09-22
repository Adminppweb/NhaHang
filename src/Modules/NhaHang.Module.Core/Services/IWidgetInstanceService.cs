using System.Linq;
using NhaHang.Module.Core.Models;

namespace NhaHang.Module.Core.Services
{
    public interface IWidgetInstanceService
    {
        IQueryable<WidgetInstance> GetPublished();
    }
}
