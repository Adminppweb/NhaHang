using System;
using System.ComponentModel.DataAnnotations;
using NhaHang.Infrastructure.Models;

namespace NhaHang.Module.Core.Models
{
    public class WidgetInstance : EntityBase
    {
        public WidgetInstance()
        {
            CreatedOn = DateTimeOffset.Now;
            LatestUpdatedOn = DateTimeOffset.Now;
        }

        [StringLength(450)]
        public string Name { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset LatestUpdatedOn { get; set; }

        public DateTimeOffset? PublishStart { get; set; }

        public DateTimeOffset? PublishEnd { get; set; }

        [StringLength(450)]
        public string WidgetId { get; set; }

        public Widget Widget { get; set; }

        public long WidgetZoneId { get; set; }

        public WidgetZone WidgetZone { get; set; }

        public int DisplayOrder { get; set; }

        public string Data { get; set; }

        public string HtmlData { get; set; }

        [StringLength(450)]
        public string SubName { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// This property cannot be used to filter again DB because it don't exist in database
        /// </summary>
        public bool IsPublished
        {
            get
            {
                return PublishStart.HasValue && PublishStart.Value < DateTimeOffset.Now && (!PublishEnd.HasValue || PublishEnd.Value > DateTimeOffset.Now);
            }
        }
    }
}
