namespace Webhcm.Infrastructure.Web.Datatables
{
	public class DataTablesParam
	{
		public int Draw { get; set; }           // Số thứ tự request, để client có thể theo dõi các yêu cầu
		public int Start { get; set; }          // Vị trí bắt đầu của dữ liệu (cho phân trang)
		public int Length { get; set; }         // Số lượng bản ghi trên mỗi trang (số dòng cần lấy)
		public DataTableSearch Search { get; set; }  // Thông tin tìm kiếm
		public List<DataTableOrder> Order { get; set; }  // Danh sách các cột để sắp xếp
		public List<DataTableColumn> Columns { get; set; }  // Danh sách các cột từ bảng dữ liệu
	}
}
