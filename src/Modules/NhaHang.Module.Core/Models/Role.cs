using Microsoft.AspNetCore.Identity;
using NhaHang.Infrastructure.Models;
using System.Collections.Generic;

namespace NhaHang.Module.Core.Models
{
    public class Role : IdentityRole<long>, IEntityWithTypedId<long>
    {
        public IList<UserRole> Users { get; set; } = new List<UserRole>();
    }
}
