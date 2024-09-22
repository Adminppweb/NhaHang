using NhaHang.Infrastructure.Commons;

namespace NhaHang.Infrastructure.Web.SmartTable
{
    public class Filter
    {
		public string Id { get; set; }
		public string Caption { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public object Data { get; set; }
        public string Number { get; set; }
        public TypeFilter TypeFilter { get; set; }
    }

	public class Filters
	{
		public string Name { get; set; }
		public string Value { get; set; }
		////public OperatorFilter OperatorFilter { get; set; }
	}
}
