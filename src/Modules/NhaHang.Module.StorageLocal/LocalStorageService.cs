using System.IO;
using System.Threading.Tasks;
using NhaHang.Infrastructure;
using NhaHang.Infrastructure.Commons;
using NhaHang.Module.Core.Services;

namespace NhaHang.Module.StorageLocal
{
    public class LocalStorageService : IStorageService
    {
        private const string MediaRootFoler = "user-content";

        public string GetMediaUrl(string fileName)
        {
            return $"/{MediaRootFoler}/{fileName}";
        }

        public async Task<CustomJsonResult> CreateDirectory(string path)
        {
            CustomJsonResult jsonResult = new CustomJsonResult();
            // Create the directory info object for that dir (normalized to its absolute representation).
            DirectoryInfo oDir = new DirectoryInfo(Path.GetFullPath(path));
            try
            {
                // Try to create the directory by using standard .Net features. (#415)
                if (!oDir.Exists)
                    oDir.Create();
                jsonResult.Message = "Success";
                jsonResult.StatusCode = 200;
                jsonResult.Result = oDir;
                return jsonResult;
            }
            catch
            {
                jsonResult.Message = "Error";
                jsonResult.StatusCode = 502;
                return jsonResult;
            }
        }

        //retrun path folder
        public async Task<string> CreateFolderNew(string nameFolder)
        {
            try
            {
                var filePath = Path.Combine(GlobalConfiguration.WebRootPath, MediaRootFoler, nameFolder);
                await CreateDirectory(filePath);
                return Path.Combine(MediaRootFoler, nameFolder);
            }
            catch (System.Exception ex)
            {
                return "";
            }
        }

        public async Task SaveMediaAsync(Stream mediaBinaryStream, string fileName, string mimeType = null)
        {
            try
            {
                var filePath = Path.Combine(GlobalConfiguration.WebRootPath, MediaRootFoler, fileName);
                using (var output = new FileStream(filePath, FileMode.Create))
                {
                    await mediaBinaryStream.CopyToAsync(output);
                }
            }
            catch (System.Exception ex)
            {

            }

        }

        public async Task DeleteMediaAsync(string fileName)
        {
            var filePath = Path.Combine(GlobalConfiguration.WebRootPath, MediaRootFoler, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
    }
}