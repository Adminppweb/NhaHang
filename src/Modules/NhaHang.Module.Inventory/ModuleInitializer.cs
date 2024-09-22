using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using NhaHang.Infrastructure.Modules;
using NhaHang.Module.Inventory.Services;
using NhaHang.Infrastructure;

namespace NhaHang.Module.Inventory
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IStockService, StockService>();

            GlobalConfiguration.RegisterAngularModule("simplAdmin.inventory");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}
