using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NhaHang.Infrastructure.Models;

namespace NhaHang.Module.Core.Models
{
    public class Vendor : EntityBase
    {
        public Vendor()
        {
            CreatedOn = DateTimeOffset.Now;
        }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(450)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(450)]
        public string Slug { get; set; }

		public string Email { get; set; }

		public string Phone { get; set; }
        
        public string Fax { get; set; }

        public string Address { get; set; }

        public string Website { get; set; }

        public string Career { get; set; }

        public string Representative { get; set; }

        public string Note { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset LatestUpdatedOn { get; set; }

        public bool IsActive { get; set; }

		public bool IsPublished { get; set; }

		public bool IsDeleted { get; set; }

		public Media ThumbnailImage { get; set; }

		public IList<User> Users { get; set; } = new List<User>();
    }
}
