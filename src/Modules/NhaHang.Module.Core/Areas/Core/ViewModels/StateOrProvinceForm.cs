﻿using System.ComponentModel.DataAnnotations;

namespace NhaHang.Module.Core.Areas.Core.ViewModels
{
    public class StateOrProvinceForm
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public string CountryId { get; set; }

        public string CountryCode { get; set; }

        public string Code { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public string Name { get; set; }

        public string Type { get; set; }
    }
}
