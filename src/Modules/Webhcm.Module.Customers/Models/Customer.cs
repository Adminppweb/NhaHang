using System;
using System.ComponentModel.DataAnnotations;
using NhaHang.Infrastructure.Models;
using NhaHang.Module.Customers.Models;

namespace NhaHang.Module.Customers.Models
{
    public class Customer : EntityBase
    {
        public Customer()
        {
            CreatedOn = DateTimeOffset.Now;
        }

        [StringLength(450)]
        public string Name { get; set; }

        [StringLength(450)]
        public string Phone { get; set; }

        [StringLength(450)]
        public string Email { get; set; }

        public string Content { get; set; }

        public bool IsDeleted { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public long CustomerAreaId { get; set; }

        public virtual CustomerArea CustomerArea { get; set; }

        public long CustomerTimeId { get; set; }

        public virtual CustomerTime CustomerTime { get; set; }

        public long CustomerTypeId { get; set; }

        public virtual CustomerType CustomerType { get; set; }
    }
}
