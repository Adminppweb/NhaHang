using Microsoft.EntityFrameworkCore;
using NhaHang.Infrastructure.Data;

namespace NhaHang.Module.Contacts.Data
{
    public class ReportCustomModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<AppSetting>().HasData(
            //    new AppSetting("GoogleAppKey") { Module = "Contact", IsVisibleInCommonSettingPage = false, Value = "" }
            //);
        }
    }
}
