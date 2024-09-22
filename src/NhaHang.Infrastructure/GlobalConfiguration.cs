using System.Collections.Generic;

///using NhaHang.Infrastructure.Class;
using NhaHang.Infrastructure.Localization;
using NhaHang.Infrastructure.Modules;

namespace NhaHang.Infrastructure
{
    public static class GlobalConfiguration
    {
        public static IList<ModuleInfo> ModuleBlazors { get; set; } = new List<ModuleInfo>();

        public static IList<ModuleInfo> Modules { get; set; } = new List<ModuleInfo>();

        public static IList<Culture> Cultures { get; set; } = new List<Culture>();

        public static string DefaultCulture => "en-US";

        public static string WebRootPath { get; set; }

        public static string ContentRootPath { get; set; }

        public static IList<string> AngularModules { get; } = new List<string>();

        public static void RegisterAngularModule(string angularModuleName)
        {
            AngularModules.Add(angularModuleName);
        }

        //public static IList<FileClassInfo> FileRegisters { get; } = new List<FileClassInfo>();

        //public static void RegisterFile(FileClassInfo file)
        //{
        //    FileRegisters.Add(file);
        //}
    }
}