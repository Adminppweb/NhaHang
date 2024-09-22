using System;
using NhaHang.Infrastructure.Models;

namespace NhaHang.Module.Core.Models
{
    public class TrackingError : EntityBaseWithTypedId<long>
    {
        //public long Id { get; set; }
        public string Page { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
