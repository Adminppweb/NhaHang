using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using NhaHang.Infrastructure.Modules;
using NhaHang.Module.Pricing.Services;
using NhaHang.Infrastructure;

namespace NhaHang.Module.Pricing
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ICouponService, CouponService>();

            GlobalConfiguration.RegisterAngularModule("simplAdmin.pricing");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
    }
}
