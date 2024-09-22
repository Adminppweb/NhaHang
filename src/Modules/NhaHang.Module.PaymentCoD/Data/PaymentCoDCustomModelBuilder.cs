using Microsoft.EntityFrameworkCore;
using NhaHang.Infrastructure.Data;
using NhaHang.Module.Payments.Models;

namespace NhaHang.Module.PaymentCoD.Data
{
    public class PaymentCoDCustomModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentProvider>().HasData(
                new PaymentProvider("CoD") { Name = "Cash On Delivery", LandingViewComponentName = "CoDLanding", ConfigureUrl = "payments-cod-config", IsEnabled = true, AdditionalSettings = null }
            );
        }
    }
}
