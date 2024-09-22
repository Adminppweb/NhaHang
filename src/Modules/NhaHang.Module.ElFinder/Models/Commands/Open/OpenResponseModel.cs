using System.Runtime.Serialization;

namespace NhaHang.Module.ElFinder.Models.Commands
{
    [DataContract]
    public class OpenResponse : BaseOpenResponseModel
    {
        public OpenResponse(DirectoryModel currentWorkingDirectory, FullPath fullPath) : base(currentWorkingDirectory)
        {
            Options = new Options(fullPath);
            Files.Add(currentWorkingDirectory);
        }
    }
}