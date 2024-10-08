﻿using System.Threading.Tasks;
using NhaHang.Module.Core.Models;

namespace NhaHang.Module.Core.Services
{
    public interface IEntityService
    {
        string ToSafeSlug(string slug, long entityId, string entityTypeId);

        Entity Get(long entityId, string entityTypeId);
        Entity GetSlug(string Slug);
        
        void Add(string name, string slug, long entityId, string entityTypeId);

        void Update(string newName, string newSlug, long entityId, string entityTypeId);

        Task Remove(long entityId, string entityTypeId);
    }
}
