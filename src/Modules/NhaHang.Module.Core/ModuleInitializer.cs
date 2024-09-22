using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NhaHang.Infrastructure;
using NhaHang.Infrastructure.Modules;
using NhaHang.Module.Core.Extensions;
using NhaHang.Module.Core.Services;

namespace NhaHang.Module.Core
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IEntityService, EntityService>();
            serviceCollection.AddTransient<IMediaService, MediaService>();
            serviceCollection.AddTransient<IThemeService, ThemeService>();
            serviceCollection.AddTransient<IWidgetInstanceService, WidgetInstanceService>();
            serviceCollection.AddScoped<IWorkContext, WorkContext>();
            serviceCollection.AddScoped<ISmsSender, SmsSender>();
            serviceCollection.AddSingleton<SettingDefinitionProvider>();
            serviceCollection.AddScoped<ISettingService, SettingService>();
            serviceCollection.AddScoped<ICurrencyService, CurrencyService>();
            serviceCollection.AddScoped<ITrackingService, TrackingService>();
            GlobalConfiguration.RegisterAngularModule("simplAdmin.core");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }

    public class ModuleBrazorInitializer : IModuleBlazorInitializer
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IWidgetInstanceService, WidgetInstanceService>();
            //serviceCollection.AddScoped<IWorkContext, WorkContext>();
            //serviceCollection.AddTransient<IEntityService, EntityService>();
            //serviceCollection.AddTransient<IMediaService, MediaService>();
            //serviceCollection.AddTransient<IThemeService, ThemeService>();
            //serviceCollection.AddTransient<IWidgetInstanceService, WidgetInstanceService>();
            //serviceCollection.AddScoped<IWorkContext, WorkContext>();
            //serviceCollection.AddScoped<ISmsSender, SmsSender>();
            //serviceCollection.AddSingleton<SettingDefinitionProvider>();
            //serviceCollection.AddScoped<ISettingService, SettingService>();
            //serviceCollection.AddScoped<ICurrencyService, CurrencyService>();
            //serviceCollection.AddScoped<ITrackingService, TrackingService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}