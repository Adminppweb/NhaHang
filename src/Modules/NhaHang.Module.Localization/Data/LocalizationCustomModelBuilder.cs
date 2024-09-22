using Microsoft.EntityFrameworkCore;
using NhaHang.Infrastructure;
using NhaHang.Infrastructure.Data;
using NhaHang.Infrastructure.Localization;
using NhaHang.Module.Core.Models;

namespace NhaHang.Module.Localization.Data
{
    public class LocalizationCustomModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Culture>().ToTable("Localization_Culture");
            modelBuilder.Entity<Resource>().ToTable("Localization_Resource");
            modelBuilder.Entity<LocalizedContentProperty>().ToTable("Localization_LocalizedContentProperty");

            modelBuilder.Entity<Culture>().HasData(
               new Culture(GlobalConfiguration.DefaultCulture) { Name = "English (US)" }
            );

            modelBuilder.Entity<AppSetting>().HasData(
                new AppSetting("Localization.LocalizedConentEnable") { Module = "Localization", IsVisibleInCommonSettingPage = true, Value = "true" });
        }
    }
}
