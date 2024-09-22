using Microsoft.EntityFrameworkCore;
using NhaHang.Infrastructure.Data;
using NhaHang.Infrastructure.Models;

namespace NhaHang.Module.Core.Data
{
    public class Repository<T> : RepositoryWithTypedId<T, long>, IRepository<T>
       where T : class, IEntityWithTypedId<long>
    {
        public Repository(SimplDbContext context) : base(context)
        {
        }
    }

    public class BrazorRepository<T> : RepositoryWithTypedId<T, long>, IRepository<T>
   where T : class, IEntityWithTypedId<long>
    {
        public BrazorRepository(SimplBrazorDbContext context) : base(context)
        {
        }
    }
}