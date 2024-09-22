using System;

namespace NhaHang.Module.Customers.Areas.Customers.ViewModels
{
    public class CustomerForm
    {
        public string Name { get; set; }

        public string Phone { get; set; }
        
        public string Email { get; set; }
        
        public string Content { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
    }
}
