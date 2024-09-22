using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NhaHang.Module.Customers.Models;

namespace NhaHang.Module.Customers.Areas.Customers.ViewModels
{
    public class EvaluateVm
    {
        public EvaluateVm()
        {
            IsPublished = true;
        }

        public long Id { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public string Slug { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Position { get; set; }

        public string Company { get; set; }

        public string Content { get; set; }

        public string IdYoutube { get; set; }

        public string SourceYou { get; set; }

        public string LinkImage { get; set; }

        public bool IsPublished { get; set; }

        public string ThumbnailImageUrl { get; set; }
    }
}
