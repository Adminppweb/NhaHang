using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaHang.Module.ElFinder.Services
{
    public interface IElFinderServices
    {
        //// save file/folder to database
        Task SaveMediaAsync(FullPath fullPath, string name);
    }
}
