﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using NhaHang.Infrastructure.Models;
using NhaHang.Module.Core.Models;

namespace NhaHang.Module.Catalog.Models
{
    public class ProductPriceHistory : EntityBase
    {
        public ProductPriceHistory()
        {
            CreatedOn = DateTimeOffset.Now;
        }

        public Product Product { get; set; }

        public long CreatedById { get; set; }

        public User CreatedBy { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public decimal? Price { get; set; }

        public decimal? OldPrice { get; set; }

        public decimal? SpecialPrice { get; set; }

        public DateTimeOffset? SpecialPriceStart { get; set; }

        public DateTimeOffset? SpecialPriceEnd { get; set; }

        [NotMapped]
        public bool IsPriceChange { get; set; }
        public bool? IsActive { get; set; }
    }
}
