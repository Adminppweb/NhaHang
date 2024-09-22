using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NhaHang.Infrastructure;
using NhaHang.Infrastructure.Modules;
using NhaHang.Module.Customers.Services;

namespace NhaHang.Module.Customers
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IEvaluateService, EvaluateService>();
            GlobalConfiguration.RegisterAngularModule("simplAdmin.customers");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}
