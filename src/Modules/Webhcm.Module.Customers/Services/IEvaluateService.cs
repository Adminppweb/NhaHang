using System.Threading.Tasks;
using NhaHang.Module.Customers.Models;

namespace NhaHang.Module.Customers.Services
{
    public interface IEvaluateService
    {
        void Create(Evaluate evaluate);

        void Update(Evaluate evaluate);

        Task Delete(Evaluate evaluate);
    }
}
