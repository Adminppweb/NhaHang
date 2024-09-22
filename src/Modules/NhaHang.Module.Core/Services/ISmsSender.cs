using System.Threading.Tasks;

namespace NhaHang.Module.Core.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}