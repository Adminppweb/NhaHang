using Microsoft.EntityFrameworkCore;
using System;
using NhaHang.Infrastructure.Data;
using NhaHang.Module.Catalog.Models;
using NhaHang.Module.Core.Models;

namespace NhaHang.Module.Catalog.Data
{
    public class CatalogCustomModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<ProductLink>()
                .HasOne(x => x.Product)
                .WithMany(p => p.ProductLinks)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductLink>()
                .HasOne(x => x.LinkedProduct)
                .WithMany(p => p.LinkedProductLinks)
                .HasForeignKey(x => x.LinkedProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EntityType>().HasData(
                new EntityType("Category") { AreaName = "Catalog", RoutingController = "Category", RoutingAction = "CategoryDetail", IsMenuable = true },
                new EntityType("Brand") { AreaName = "Catalog", RoutingController = "Brand", RoutingAction = "BrandDetail", IsMenuable = true },
                new EntityType("Certification") { AreaName = "Catalog", RoutingController = "Certification", RoutingAction = "CertificationDetail", IsMenuable = true },
                new EntityType("Contaminant") { AreaName = "Catalog", RoutingController = "Contaminant", RoutingAction = "ContaminantDetail", IsMenuable = true },
                new EntityType("Product") { AreaName = "Catalog", RoutingController = "Product", RoutingAction = "ProductDetail", IsMenuable = false }
            );

            modelBuilder.Entity<Widget>().HasData(
                new Widget("CategoryWidget") { Name = "Category Widget", ViewComponentName = "CategoryWidget", CreateUrl = "widget-category-create", EditUrl = "widget-category-edit", CreatedOn = new DateTimeOffset(new DateTime(2018, 5, 29, 4, 33, 39, 160, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)) },
                new Widget("ProductWidget") { Name = "Product Widget", ViewComponentName = "ProductWidget", CreateUrl = "widget-product-create", EditUrl = "widget-product-edit", CreatedOn = new DateTimeOffset(new DateTime(2018, 5, 29, 4, 33, 39, 163, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)) },
                new Widget("SimpleProductWidget") { Name = "Simple Product Widget", ViewComponentName = "SimpleProductWidget", CreateUrl = "widget-simple-product-create", EditUrl = "widget-simple-product-edit", CreatedOn = new DateTimeOffset(new DateTime(2018, 5, 29, 4, 33, 39, 163, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)) }
            );
        }
    }
}
