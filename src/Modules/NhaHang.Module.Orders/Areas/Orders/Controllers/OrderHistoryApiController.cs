﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NhaHang.Infrastructure.Data;
using NhaHang.Module.Orders.Models;

namespace NhaHang.Module.Orders.Areas.Orders.Controllers
{
    [Area("Orders")]
    [Authorize(Roles = "admin, vendor")]
    [ApiController]
    public class OrderHistoryApiController : Controller
    {
        private readonly IRepository<OrderHistory> _orderHistoryRepository;

        public OrderHistoryApiController(IRepository<OrderHistory> orderHistoryRepository)
        {
            _orderHistoryRepository = orderHistoryRepository;
        }

        [HttpGet("api/orders/{orderId}/history")]
        public async Task<IActionResult> Get(long orderId)
        {
            var histories = await _orderHistoryRepository.Query()
                .Where(x => x.OrderId == orderId)
                .Select(x => new
                {
                    OldStatus = x.OldStatus.ToString(),
                    NewStatus = x.NewStatus.ToString(),
                    CreatedById = x.CreatedById,
                    CreatedByFullName = x.CreatedBy.FullName,
                    x.Note,
                    x.CreatedOn
                }).ToListAsync();

            return Ok(histories);
        }
    }
}