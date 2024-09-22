using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NhaHang.Infrastructure.Models;
using NhaHang.Module.Core.Models;

namespace NhaHang.Module.Orders.Models
{
    [Table("Table")]
    public class Table : EntityBase
    {
        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Floor { get; set; }

        public int Seq { get; set; }
        public bool Active { get; set; }
        public string QRCode { get; set; }
        public int Seats { get; set; }
        public bool IsPrivate { get; set; }
        public int Status { get; set; }

        #region Track Information

        public long CreatedById { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset LatestUpdatedOn { get; set; }
        public bool IsDelete { get; set; }
        public User CreatedBy { get; set; }

        #endregion Track Information
    }
}