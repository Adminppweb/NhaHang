using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NhaHang.Module.Customers.Areas.Customers.ViewModels
{
    public class CustomerVm
    {
        [Required(ErrorMessage = "The {0} field is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public string Content { get; set; }
    }
}
