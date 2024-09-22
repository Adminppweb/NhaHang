using NhaHang.Module.Core.Models;

namespace NhaHang.Module.Pricing.Models
{
    public class CatalogRuleCustomerGroup
    {
        public long CatalogRuleId { get; set; }

        public CatalogRule CatalogRule { get; set; }

        public long CustomerGroupId { get; set; }

        public CustomerGroup CustomerGroup { get; set; }
    }
}
