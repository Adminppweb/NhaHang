using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaHang.Module.Catalog.Services
{
    public interface IProductOptionService
    {

        public  string OptionValue(string value);

        public  string OptionDisplayType(string value);
      
    }
}
