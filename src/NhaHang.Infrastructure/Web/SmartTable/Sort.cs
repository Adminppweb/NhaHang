using System.Diagnostics.CodeAnalysis;

namespace NhaHang.Infrastructure.Web.SmartTable
{
    public class Sort
    {
        [AllowNull]
        public string Predicate { get; set; }
		[AllowNull]
		public bool Reverse { get; set; }
    }
}
