
namespace NhaHang.Infrastructure.Commons
{
    public static class CommonConstants
    {
        #region JWT Api Secret
        public static string JWT_SECRET = "vinagiay_api_token_key";
        public static string JWT_ROLE = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
        public static string JWT_EMAIL = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        public static string JWT_CPUID = "CPUID";
        #endregion

        #region User Authentication
        public static string SUPPERADMIN = "SUPPERADMIN";
        public static string ADMIN = "ADMIN";
        public static string USER = "USER";
        public static string SUPPLIER = "SUPPLIER";
        public static string EMPLOYEE = "EMPLOYEE";
        public static string ROLE_CUAHANG = "CUAHANG";
        public static string ROLE_KHOVUNGMIEN = "KHOVUNGMIEN";
        public static string ROLE_QUANLYKHOLOI = "QUANLYKHOLOI";
        public static string ROLE_DUYET = "DUYET";
        public static string ROLE_VM_SALE = "VM_SALE";
        public static string ROLE_KT_SALE = "KT_SALE";
        public static string ROLE_SALE = "KHOSALE";
        public static string ROLE_KHOTONG = "KHOTONG";
        public static string Culture_VN = "vi-VN";
        public static string Culture_EN = "en-US";
        public static int PAGESIZE = 10;
        public static string SESSION_TOKEN = "SESSION_TOKEN";
        #endregion

        #region Prefix Auto identity Id
        public static string Cuahang = " ";
        public static string KHOTONG_NHAPKHO = "KTNK";
        public static string KHOXULY_NHAPKHO = "KXLNK";
        public static string KHOSALE_NHAPKHO = "KSNK";
        public static string KHOHUY_NHAPKHO = "KHNK";
        public static string KHOTONG_XUATKHO = "KTXK";
        public static string KHOVM_NHAPKHO = "VMNK";
        public static string KHOVM_XUATKHO = "VMXK";
        public static string KHOSI_NHAPKHO = "KSNK";
        public static string TRALOI = "_X_TLOI_"; // xuất trả lỗi: mã này phát sinh khi CH-VM-KT tồn kho xuất trả lỗi
        public static string XUATLOI = "_XLOI_";  // xuất lỗi
        public static string NHAP_TRALOI = "_N_TLOI_";
        public static string NHAPLAI_TONKHO = "_NLAI_";
        public static string KT_X_VungMien = "KT_X_";
        public static string KS_X_VungMien = "KS_X_";
        public static string KT_XT_VungMien = "KT_XT_";
        public static string Vungmien_X_CuaHang = "_X_";
        public static string Vungmien_XT_KhoTong = "_KHOTONG_";
        public static string mavandon_KhoLoi = "KL_VC_";
        public static string mavandon_KhoXuLy = "KXL_VC_";
        public static string CODETHUNG = "CODETHUNG";
        public static string DOT_HUY = "DotHuy_";
        public static string DOT_SALE = "DotSale_";
        public static string KHOSALE_XUATKHO = "KSXK";
        public static string KHOSALEVM_NHAPKHO = "SVMNK";
        public static string KHOSALEVM_XUATKHO = "SVMXK";
        #endregion

        #region KhuVuc
        public static string KhoTong = "KhoTong";
        public static string KhoMienBac = "KhoMienBac";
        public static string KhoMienNam = "KhoMienNam";
        public static string KhoMienTrung = "KhoMienTrung";
        public static string KhoMienTay = "KhoMienTay";
        public static string KhoSi = "KhoSi";
        public static string KhoLoi = "KhoLoi";
        public static string KhoXuLyLoi = "KhoXuLyLoi";
        public static string KhoSale = "KhoSale";
        public static string KhoHuy = "KhoHuy";
        public static string MienBac_Sale = "MienBac_Sale";
        public static string MienNam_Sale = "MienNam_Sale";
        public static string MienTrung_Sale = "MienTrung_Sale";
        public static string MienTay_Sale = "MienTay_Sale";
        public static string mnsalech1 = "MienNam_Cuahang1";
        public static string mnsalech2 = "MienNam_Cuahang2";
        public static string mnsalech3 = "MienNam_Cuahang3";

        #endregion

        #region Datetime
        public static string yyyyMMdd = "yyyy-MM-dd";
        public static string ddMMyyyy = "dd/MM/yyyy";
        public static string MMddyyyy = "MM/dd/yyyy";
        #endregion

        #region Default Value
        public static string DEFAULT_VALUE = "-1";
        public static string DEFAULT_TEXT = "Tất Cả";
        #endregion

        #region Trạng Thái Nhận??
        public enum TrangThaiNhan
        {

            Pending = 0,
            IsGot = 1,
            Cancel = 2,

        }
        #endregion

        #region trang thái kho ?? 
        public enum TrangThaiKho
        {
            Pending = 0, // chờ xử lý
            IsGot = 1, // tồn kho
            Delivered = 2, // đã chuyển --> sản phẩm đã chuyển về
            DaNhap = 0,
            DaXuat = 1,
            XinNhapLai = 2,
            None = -1,

        }
        #endregion

        #region TrangThaiVanChuyen
        public enum TrangThaiVanChuyen
        {
            ChuaChuyen = 0, // chưa chuyển : hàng chưa đưa đi
            DaChuyen = 1, // đã chuyển : hàng đã chuyển đi -- 

        }
        #endregion

        //Cập nhật qui trình: Gửi -chuyển - nhận
        #region TrangThaiHoanThanh
        public enum TrangThaiHoanThanh
        {
            ChuaHoanThanh = 0,
            HoanThanh = 1,

        }
        #endregion

        #region trạng thái duyệt
        public enum TrangThaiDuyet
        {
            Pending = 0,   // Chờ duyêt
            Approved = 1,  // Đã Duyệt
            UnApproved = 2,  //Không Duyệt - Trả LẠI KHO TỔNG
            None = -1,

            ChuaDuyet = 0,
            DaDuyet = 1,
            HuyDuyet = 2, // CHỈ DANH RIENG CHO DUYET NHAP LAI TON KHO
        }

        #endregion

        #region Hành động thực hiện vào kho lỗi
        public enum Action_KhoLoi
        {
            TonKhoXuatLoi = 0,  // Tồn kho xuất vào kho lỗi
            XuatTraLoi = 1,   // ch trả lỗi vùng miền. vùng miền trả lỗi cho kho tổng, kho tổng đưa trả lỗi vào kho xử lý
            NhapTraLoi = 2,  // thực hiện khi : VM tiếp nhận lại lỗi từ CH trả về và Kho tổng tiếp nhận lại lỗi từ kho vùng miền
            Vm_XinTraTonkho = 3, // thực hiện khi : Vùng miền xin được nhập lại sản phẩm vào tồn kho
        }

        #endregion

        #region pagination
        public enum Paginations
        {
            PageIndex = 0,
            PageSize = 20,
            PageSizeMax = 100000
        }
        #endregion

        #region trang thái kho Hủy
        public enum TrangThaiKhoHuy
        {
            Dahuy = 1,
            ChuaHuy = 0

        }
        #endregion

        #region Type WidgetZone
        public static string WidgetZone_HomeTopSlide = "HomeTopSlide";
        public static string WidgetZone_AboutTopSlide = "AboutTopSlide";
        #endregion

        #region Position
        public static string HEADER = "HEADER";
        public static string BODY = "BODY";
        public static string FOOTER = "FOOTER";
        #endregion

        #region Position
        public static string LEFT = "LEFT";
        public static string RIGHT = "RIGHT";
        public static string CENTER = "CENTER";
        public static string TOP = "TOP";
        public static string BOTTOM = "BOTTOM";
        public static string TOP_RIGHT = "TOP_RIGHT";
        public static string TOP_LEFT = "TOPLEFT";
        public static string BOTTOM_RIGHT = "BOTTOM_RIGHT";
        public static string BOTTOM_LEFT = "BOTTOM_LEFT";
        #endregion

        #region Type Layout
        public static string LAYOUT_DEFAULT = "INDEX";
        public static string LAYOUT_RIGHT = "RIGHT";
        public static string LAYOUT_CENTER = "CENTER";
        public static string LAYOUT_TOP = "TOP";
        public static string LAYOUT_BOTTOM = "BOTTOM";
        public static string LAYOUT_TOP_RIGHT = "TOP_RIGHT";
        public static string LAYOUT_TOP_LEFT = "TOP_LEFT";
        public static string LAYOUT_BOTTOM_RIGHT = "BOTTOM_RIGHT";
        public static string LAYOUT_BOTTOM_LEFT = "BOTTOM_LEFT";

        public static string LAYOUT_TOP_SLIDE = "TOP_SLIDE";
        public static string LAYOUT_BODY_SLIDE = "BODY_SLIDE";
        public static string LAYOUT_BOTTOM_SLIDE = "BOTTOM_SLIDE";
        public static string LAYOUT_RIGHT_SLIDE = "RIGHT_SLIDE";
        public static string LAYOUT_LEFT_SLIDE = "LEFT_SLIDE";

        public static string LAYOUT_BODY_CONTENT_TOP = "BODY_CONTENT_TOP";
        public static string LAYOUT_BODY_CONTENT_CENTER = "BODY_CONTENT_CENTER";
        public static string LAYOUT_BODY_BOTTOM_CENTER = "BODY_BOTTOM_CENTER";
        #endregion


        #region Payment Method
        public static string CashOnDelivery = "CashOnDelivery";
        public static string PayWithCash = "PayWithCash";
        public static string PayWithMomo = "PayWithMomo";
        public static string PayWithZaloPay = "PayWithZaloPay";
        public static string PaypalExpress = "PaypalExpress";
        public static string NganLuong = "NganLuong";
        public static string CoD = "CoD";
        public static string Cashfree = "Cashfree";
        public static string Braintree = "Braintree";
        #endregion

        #region IsView
        public static string View = "View";
        public static string ViewComponent = "ViewComponent";
        public static string RenderHTML = "RenderHTML";
        #endregion


        public class AppRole
        {
            public const string AdminRole = "Admin";
        }
        public class UserClaims
        {
            public const string Id = "sub";
            public const string Roles = "role";
            public const string Children = "Children";
        }
        public class TypeTemplate
        {
            public const string User = "User";
            public const string ImageThumnail = "ImageThumnail";
            public const string Number = "Number";
            public const string DateTimeVN = "DateTimeVN";
            public const string Text = "Text";
        }
    }

}
