using System.Collections.Generic;
using System.Threading.Tasks;
using NhaHang.Infrastructure;
using NhaHang.Module.Orders.Services;
using Table = NhaHang.Module.Orders.Models.Table;

namespace NhaHang.Module.Catalog.Services
{
    public class TableService : ITableService
    {
        public Task DeleteById(long Id, long UserId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<List<Table>>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<List<Table>>> GetAllActive()
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<Table>> GetById(long Id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<Table>> Insert(Table entity, long UserId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<long>> Update(Table entity, long UserId)
        {
            throw new System.NotImplementedException();
        }
    }
}