using System.Collections.Generic;
using System.Threading.Tasks;
using NhaHang.Module.Catalog.Areas.Catalog.ViewModels;
using NhaHang.Module.Catalog.Models;

namespace NhaHang.Module.Catalog.Services
{
    public interface IProductService
    {
        void Create(Product product);

        void Update(Product product);

        Task Delete(Product product);

        Task<List<ProductThumbnail>> GetPromotionProducts();
    }
}
