using Microsoft.EntityFrameworkCore;
using NhaHang.Infrastructure.Data;

namespace NhaHang.Module.ElFinder.Data
{
    public class ElFinderCustomModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FilePermission>().ToTable("FilePermission");
        }
    }
}