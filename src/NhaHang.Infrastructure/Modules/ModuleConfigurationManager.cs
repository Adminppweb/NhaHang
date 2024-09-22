using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace NhaHang.Infrastructure.Modules
{
    public class ModuleConfigurationManager : IModuleConfigurationManager
    {
        private readonly string ModulesFilename = "modules.json";

        public ModuleConfigurationManager()
        {
        }

        public ModuleConfigurationManager(string _modulesFilename)
        {
            this.ModulesFilename = _modulesFilename;
        }

        public IEnumerable<string> getFiles(string file)
        {
            var modulesPath = Path.Combine(GlobalConfiguration.ContentRootPath, file);
            Console.WriteLine(GlobalConfiguration.ContentRootPath);
            throw new NotImplementedException();
        }

        public IEnumerable<ModuleInfo> GetModules()
        {
            var modulesPath = Path.Combine(GlobalConfiguration.ContentRootPath, ModulesFilename);
            using var reader = new StreamReader(modulesPath);
            string content = reader.ReadToEnd();
            dynamic modulesData = JsonConvert.DeserializeObject(content);
            foreach (dynamic module in modulesData)
            {
                yield return new ModuleInfo
                {
                    Id = module.id,
                    Version = Version.Parse(module.version.ToString()),
                    IsBundledWithHost = module.isBundledWithHost
                };
            }
        }
    }
}