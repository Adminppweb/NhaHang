using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using NhaHang.Infrastructure.Models;
using NhaHang.Module.Core.Models;
using NhaHang.Module.Customers.Models;

namespace NhaHang.Module.Customers.Models
{
    public class Evaluate : EntityBase
	{

        [StringLength(450)]
        public string Name { get; set; }

        [StringLength(450)]
        public string Slug { get; set; }

        [StringLength(450)]
        public string Phone { get; set; }

        [StringLength(450)]
        public string Email { get; set; }

        [StringLength(450)]
        public string Address { get; set; }

        [StringLength(450)]
        public string Position { get; set; }

        [StringLength(450)]
        public string Company { get; set; }

        public string Content { get; set; }

        public string IdYoutube { get; set; }
        
        public string SourceYou { get; set; }

        public string LinkImage { get; set; }

		public bool IsPublished { get; set; }

		public bool IsDeleted { get; set; }

		public Media ThumbnailImage { get; set; }
	}
}