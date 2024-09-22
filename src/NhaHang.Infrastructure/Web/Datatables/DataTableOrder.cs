namespace Webhcm.Infrastructure.Web.Datatables
{
	public class DataTableOrder
	{
		public int Column { get; set; }         // Cột cần sắp xếp (dựa trên chỉ số cột)
		public string Dir { get; set; }         // Hướng sắp xếp: "asc" hoặc "desc"
	}
}
