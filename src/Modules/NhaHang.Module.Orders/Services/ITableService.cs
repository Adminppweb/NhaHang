using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NhaHang.Infrastructure;
using NhaHang.Module.Orders.Models;

namespace NhaHang.Module.Orders.Services
{
    public interface ITableService
    {
        public Task DeleteById(long Id, long UserId);

        public Task<Result<List<Table>>> GetAll();

        public Task<Result<List<Table>>> GetAllActive();

        public Task<Result<Table>> GetById(long Id);

        public Task<Result<Table>> Insert(Table entity, long UserId);

        public Task<Result<long>> Update(Table entity, long UserId);
    }
}