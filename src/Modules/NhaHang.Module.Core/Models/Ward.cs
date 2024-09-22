using NhaHang.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace NhaHang.Module.Core.Models
{
    public class Ward : EntityBase
    {
        public Ward() { }

        public Ward(long id)
        {
            Id = id;
        }

        public long DistrictId { get; set; }

        public District District { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(450)]
        public string Name { get; set; }

        [StringLength(450)]
        public string Type { get; set; }

        public string Location { get; set; }
    }
}
