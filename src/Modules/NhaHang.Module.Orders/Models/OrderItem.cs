﻿using Newtonsoft.Json;
using NhaHang.Infrastructure.Models;
using NhaHang.Module.Catalog.Models;

namespace NhaHang.Module.Orders.Models
{
    public class OrderItem : EntityBase
    {
        [JsonIgnore]
        public Order Order { get; set; }

        public long ProductId { get; set; }

        [JsonIgnore]
        public Product Product { get; set; }

        public decimal ProductPrice { get; set; }

        public int Quantity { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal TaxPercent { get; set; }
    }
}
