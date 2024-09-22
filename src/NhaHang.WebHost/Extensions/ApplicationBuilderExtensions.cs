using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using NhaHang.Infrastructure;
using NhaHang.Infrastructure.Data;
using NhaHang.Infrastructure.Localization;
using NhaHang.Module.Localization;

namespace NhaHang.WebHost.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomizedIdentity(this IApplicationBuilder app)
        {
            app.UseIdentityServer();
            app.UseWhen(
                context => context.Request.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase),
                a => a.Use(async (context, next) =>
                {
                    if (!context.User.Identity.IsAuthenticated)
                    {
                        var principal = new ClaimsPrincipal();

                        var bearerAuthResult = await context.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
                        if (bearerAuthResult?.Principal != null)
                        {
                            principal.AddIdentities(bearerAuthResult.Principal.Identities);
                        }

                        context.User = principal;
                    }

                    await next();
                }));

            app.UseAuthorization();
            return app;
        }

        public static IApplicationBuilder UseCustomizedStaticFiles(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    OnPrepareResponse = (context) =>
                    {
                        var headers = context.Context.Response.GetTypedHeaders();
                        headers.CacheControl = new CacheControlHeaderValue
                        {
                            NoCache = true,
                            NoStore = true,
                            MaxAge = TimeSpan.FromDays(-1)
                        };
                    }
                });
            }
            else
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    OnPrepareResponse = (context) =>
                    {
                        var headers = context.Context.Response.GetTypedHeaders();
                        headers.CacheControl = new CacheControlHeaderValue
                        {
                            Public = true,
                            MaxAge = TimeSpan.FromDays(60)
                        };
                    }
                });
            }

            return app;
        }

        public static IApplicationBuilder UseCustomizedRequestLocalization(this IApplicationBuilder app)
        {
            string defaultCultureUI = GlobalConfiguration.DefaultCulture;
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var cultureRepository = scope.ServiceProvider.GetRequiredService<IRepositoryWithTypedId<Culture, string>>();
                GlobalConfiguration.Cultures = cultureRepository.Query().ToList();

                var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                defaultCultureUI = config.GetValue<string>("Global.DefaultCultureUI");
            }

            var supportedCultures = GlobalConfiguration.Cultures.Select(c => c.Id).ToArray();
            app.UseRequestLocalization(options =>
            options
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures)
                .SetDefaultCulture(defaultCultureUI ?? GlobalConfiguration.DefaultCulture)
                .RequestCultureProviders.Insert(0, new EfRequestCultureProvider())
            );

            return app;
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                var provider = app.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwagger(options =>
                {
                    options.SerializeAsV2 = true;
                    options.PreSerializeFilters.Add((swagger, req) =>
                    {
                        swagger.Servers = new List<OpenApiServer>() { new OpenApiServer() { Url = $"https://{req.Host}/" } };
                    });
                });

                app.UseSwaggerUI(options =>
                {
                    //foreach (var desc in provider.ApiVersionDescriptions)
                    //{
                    //    options.SwaggerEndpoint($"../swagger/{desc.GroupName}/swagger.json", desc.ApiVersion.ToString());
                    //    //// options.DefaultModelsExpandDepth(-1);
                    //    //// options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                    //}

                    //foreach (var description in provider.ApiVersionDescriptions)
                    //{
                    //    options.SwaggerEndpoint($"/swagger/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    //}

                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1.0");
                    options.SwaggerEndpoint("/swagger/v2/swagger.json", "v2.0");
                    options.RoutePrefix = "";
                });

            }
            return app;
        }

    }
}
