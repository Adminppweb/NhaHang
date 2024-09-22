using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using MediatR;
using NhaHang.Infrastructure.Modules;
using NhaHang.Module.Core.Events;
using NhaHang.Module.Localization.Events;
using NhaHang.Module.Localization.Services;
using NhaHang.Module.Core.Services;
using NhaHang.Infrastructure;

namespace NhaHang.Module.Localization
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<INotificationHandler<UserSignedIn>, UserSignedInHandler>();
            services.AddTransient<IContentLocalizationService, ContentLocalizationService>();

            GlobalConfiguration.RegisterAngularModule("simplAdmin.localization");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
    }
}
