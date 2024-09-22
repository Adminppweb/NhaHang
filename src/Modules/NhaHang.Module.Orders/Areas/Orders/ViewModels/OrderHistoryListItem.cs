﻿using System;
using System.Collections.Generic;
using NhaHang.Module.Core.Services;
using NhaHang.Module.Orders.Models;

namespace NhaHang.Module.Orders.Areas.Orders.ViewModels
{
    public class OrderHistoryListItem
    {
        private readonly ICurrencyService _currencyService;

        public OrderHistoryListItem(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        public long Id { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public decimal SubTotal { get; set; }

        public string SubTotalString { get { return _currencyService.FormatCurrency(SubTotal); } }

        public OrderStatus OrderStatus { get; set; }

        public IList<OrderHistoryProductVm> OrderItems { get; set; } = new List<OrderHistoryProductVm>();
    }
}
