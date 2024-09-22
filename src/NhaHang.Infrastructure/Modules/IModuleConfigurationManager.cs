using System.Collections.Generic;

namespace NhaHang.Infrastructure.Modules
{
    public interface IModuleConfigurationManager
    {
        IEnumerable<ModuleInfo> GetModules();

        IEnumerable<string> getFiles(string file);
    }
}