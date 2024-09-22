using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaHang.Module.ElFinder.Authorization
{
    public interface IFilePermission
    {
        public void AddPermission(string UserId, string Permission)
        {
        }

        public string[] GetPermission(string UserId, string Permission)
        {
            return new string[] { "", "" };
        }
    }
}