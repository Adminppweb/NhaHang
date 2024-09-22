using NhaHang.Module.Inventory.Models;
using System.Threading.Tasks;

namespace NhaHang.Module.Inventory.Services
{
    public interface IStockService
    {
        Task AddAllProduct(Warehouse warehouse);

        Task UpdateStock(StockUpdateRequest stockUpdateRequest);
    }
}
