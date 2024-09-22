using NhaHang.Module.Core.Models;

namespace NhaHang.Module.Pricing.Models
{
    public class CartRuleCustomerGroup
    {
        public long CartRuleId { get; set; }

        public CartRule CartRule { get; set; }

        public long CustomerGroupId { get; set; }

        public CustomerGroup CustomerGroup { get; set; }
    }
}
