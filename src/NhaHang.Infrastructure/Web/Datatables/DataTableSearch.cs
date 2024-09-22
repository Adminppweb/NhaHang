namespace Webhcm.Infrastructure.Web.Datatables
{
	public class DataTableSearch
	{
		public string Value { get; set; }       // Giá trị tìm kiếm toàn bộ bảng
		public string Regex { get; set; }       // Có sử dụng biểu thức chính quy cho tìm kiếm hay không (ít khi dùng)
	}
}
