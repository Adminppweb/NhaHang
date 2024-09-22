using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NhaHang.Infrastructure;
using NhaHang.Infrastructure.Modules;
using NhaHang.Module.ElFinder.Services;

namespace NhaHang.Module.ElFinder
{
	public class ModuleInitializer : IModuleInitializer
	{
		public void ConfigureServices(IServiceCollection serviceCollection)
		{
			serviceCollection.AddTransient<IElFinderServices, ElFinderServices>();
			///GlobalConfiguration.RegisterAngularModule("simplAdmin.elFinder");
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
		}
	}
}