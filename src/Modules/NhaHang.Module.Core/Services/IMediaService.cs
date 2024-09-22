
using System.IO;
using System.Threading.Tasks;
using NhaHang.Infrastructure.Commons;
using NhaHang.Module.Core.Models;

namespace NhaHang.Module.Core.Services
{
    public interface IMediaService
    {
        Task<CustomJsonResult> CreateDirectory(string fileName, string caption = "");

        Task<CustomJsonResult> CreateFolderNew(string fileName, string caption = "", string pathParent = "");

        Media GetMediaId(long Id);

        string GetMediaUrl(Media media);

        string GetMediaUrl(string fileName);

        string GetThumbnailUrl(Media media);

        Task SaveMediaAsync(Stream mediaBinaryStream, string fileName, string mimeType = null);

        Task DeleteMediaAsync(Media media);

        Task DeleteMediaAsync(string fileName);
    }
}
