using Microsoft.EntityFrameworkCore;

namespace NhaHang.Infrastructure.Data
{
    public interface ICustomModelBuilder
    {
        void Build(ModelBuilder modelBuilder);
    }
}
