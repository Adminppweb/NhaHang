using System.IO;
using System.Threading.Tasks;
using NhaHang.Infrastructure.Commons;

namespace NhaHang.Module.Core.Services
{
    public interface IStorageService
    {
        Task<CustomJsonResult> CreateDirectory(string path);

        Task<string> CreateFolderNew(string nameFolder);

        string GetMediaUrl(string fileName);

        Task SaveMediaAsync(Stream mediaBinaryStream, string fileName, string mimeType = null);

        Task DeleteMediaAsync(string fileName);
    }
}
