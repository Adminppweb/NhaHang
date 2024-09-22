using System.IO;

namespace NhaHang.Module.ElFinder.Helpers
{
    public static class PathHelper
    {
        public static string GetFullPathNormalized(string path)
        {
            return Path.TrimEndingDirectorySeparator(Path.GetFullPath(path));
        }

        public static string MapPath(string path, string basePath = null, string webRootPath = "wwwroot")
        {
            if (string.IsNullOrEmpty(basePath))
            {
                basePath = webRootPath;
            }

            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
            return GetFullPathNormalized(Path.Combine(basePath, path));
        }
    }
}