using System.ComponentModel.DataAnnotations;
using NhaHang.Infrastructure.Models;

namespace NhaHang.Module.Customers.Models
{
    public class CustomerType : EntityBase
    {
        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(450)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}
