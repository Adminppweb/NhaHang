using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NhaHang.Infrastructure.Models;

namespace NhaHang.Module.ElFinder.Data
{
    public class FilePermission : EntityBase
    {
        public string UserId { get; set; }
        public string Permission { get; set; }
        public string Groups { get; set; }

        public string[] GetPermisson()
        {
            var arrPermisson = Permission.Split(',', StringSplitOptions.None);
            return arrPermisson;
        }
    }
}