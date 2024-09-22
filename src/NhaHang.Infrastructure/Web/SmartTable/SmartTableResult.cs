using System.Collections.Generic;

namespace NhaHang.Infrastructure.Web.SmartTable
{
    public class SmartTableResult<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int TotalRecord { get; set; }

        public int NumberOfPages { get; set; }
    }

    public class SmartTableResultV2
    {
        public object Items { get; set; }

        public int TotalRecord { get; set; }

        public int NumberOfPages { get; set; }

        public int? Offset { get; set; } = 100;

        public SmartTableParam SmartTableParam { get; set; }
    }

	public class SmartPaginationTableResult<T>
	{
		public IEnumerable<T> Items { get; set; }
		public Pagination Pagination { get; set; }
	}
}
