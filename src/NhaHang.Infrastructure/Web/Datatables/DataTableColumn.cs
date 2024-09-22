namespace Webhcm.Infrastructure.Web.Datatables
{
	public class DataTableColumn
	{
		public string Data { get; set; }        // Tên của cột dữ liệu
		public string Name { get; set; }        // Tên cột để sắp xếp/tìm kiếm
		public bool Searchable { get; set; }    // Cột có được tìm kiếm hay không
		public bool Orderable { get; set; }     // Cột có được sắp xếp hay không
		public DataTableSearch Search { get; set; }  // Điều kiện tìm kiếm trên cột cụ thể
	}
}
