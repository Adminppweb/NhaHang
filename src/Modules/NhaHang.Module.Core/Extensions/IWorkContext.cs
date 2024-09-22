using System.Threading.Tasks;
using NhaHang.Module.Core.Models;

namespace NhaHang.Module.Core.Extensions
{
    public interface IWorkContext
    {
        Task<User> GetCurrentUser();
        public string GetSlugCurrent();
    }
}
