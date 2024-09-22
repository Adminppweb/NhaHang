using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NhaHang.Infrastructure.Models;

namespace NhaHang.Module.Report.Models
{
    public class Report : EntityBase
    {
        public Report()
        {
            CreatedOn = DateTimeOffset.Now;
        }

        [StringLength(450)]
        public string FullName { get; set; }

        [StringLength(450)]
        public string PhoneNumber { get; set; }

        [StringLength(450)]
        public string EmailAddress { get; set; }

        [StringLength(450)]
        public string Address { get; set; }

        public string Content { get; set; }

        public long ContactAreaId { get; set; }

        public virtual IList<ReportItem> ReportItem { get; set; }

        public bool IsDeleted { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
    }
}
