using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NhaHang.Module.Catalog.Areas.Catalog.ViewModels;

namespace NhaHang.Module.Catalog.Services
{
    public class ProductOptionService: IProductOptionService
    {

        public  string OptionValue(string value)
        {
            string returnValue = "";
            //if (OptionDisplayValues[value] != null)
            //{
            //    returnValue = OptionDisplayValues[value].Value + string.Empty;
            //}
            //else
            //    returnValue = string.Empty;
            return returnValue;
        }

        public  string OptionDisplayType(string value)
        {
            string returnValue = "";
            //if (OptionDisplayValues[value] != null)
            //{
            //    returnValue = OptionDisplayValues[value].DisplayType + string.Empty;
            //}
            //else
            //    returnValue = string.Empty;
            return returnValue;
        }
    }
}
