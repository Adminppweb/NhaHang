﻿using System.Threading.Tasks;
using NhaHang.Module.Catalog.Models;

namespace NhaHang.Module.Catalog.Services
{
    public interface IBrandService
    {
        Task Create(Brand brand);

        Task Update(Brand brand);

        Task Delete(long id);

        Task Delete(Brand brand);
    }
}
