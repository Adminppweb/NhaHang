namespace NhaHang.Infrastructure.Commons
{
    public class CustomJsonParams
    {
        public string Action { get; set; } // Trùng với Action Controller
        public string View { get; set; }
        //public string LayoutView { get; set; }
        public object Data { get; set; }
        public object DataOther { get; set; } = null;
        public string Layout { get; set; }
    }


}
