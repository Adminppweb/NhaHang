using Newtonsoft.Json.Linq;

namespace NhaHang.Infrastructure.Web.SmartTable
{
    public class Search
    {
		public string KeyString { get; set; }
		public JObject PredicateObject { get; set; } = new JObject();
    }
}
