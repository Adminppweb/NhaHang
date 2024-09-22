using System.ComponentModel.DataAnnotations;
using NhaHang.Infrastructure.Models;

namespace NhaHang.Module.Payments.Models
{
    public class PaymentProvider : EntityBaseWithTypedId<string>
    {
        public PaymentProvider(string id)
        {
            Id = id;
        }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(450)]
        public string Name { get; set; }

        public bool IsEnabled { get; set; }

        [StringLength(450)]
        public string ConfigureUrl { get; set; }

        [StringLength(450)]
        public string LandingViewComponentName { get; set; }

        /// <summary>
        /// Additional setting for specific provider. Stored as json string.
        /// </summary>
        public string AdditionalSettings { get; set; }
    }
}
