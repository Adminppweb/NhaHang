using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NhaHang.Infrastructure.Models;

namespace NhaHang.Module.Core.Models
{
    public class User_ProductLike : EntityBase
    {         
        public User_ProductLike() { }
        public long ProductId { get; set; }
        public long UserId { get; set; }
        public string Comment { get; set; }
        public string Description { get; set; }
        public bool? IsLike { get; set; }
    }
}
