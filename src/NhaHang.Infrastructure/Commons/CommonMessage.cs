namespace NhaHang.Infrastructure.Commons
{
    public static class CommonMessage
    {
        public static string MESSAGE_SENDMAIL_ERROR = "Gửi mail không thành công";

        public static string MESSAGE_SENDMAIL_COMPLETED = "Gửi email thành công";
        public static string MESSAGE_SENDMAIL_EXISTS = "Email đã tồn tại";
        public static string MESSAGE_SENDMAIL_NO_EXISTS = "Email Không tồn tại trong hệ thống";
        public static string MESSAGE_NOT_SELECT_ROW = "Chỉ được phép chọn 1 dòng dữ liệu";
        public static string MESSAGE_NOT_ROW = "Vui lòng chọn dữ liệu";
        public static string MESSAGE_NOT_SELECT_MULTI_ROW = "Chỉ được phép chọn 1 dòng dữ liệu";
        public static string MESSAGE_CONFIRM_LOCK = "Bạn có đống ý khóa?";
        public static string MESSAGE_CONFIRM_UNLOCK = "Bạn có đống ý bỏ khóa?";
        public static string MESSAGE_CONFIRM_SYNC = "Bạn có đống ý đồng bộ";
        public static string MESSAGE_CONFIRM_DELETE = "Bạn có đống ý xóa?";
        public static string MESSAGE_CONFIRM_APPROVE = "Bạn có đống ý phê duyệt";
        public static string MESSAGE_CONFIRM_REJECT = "Bạn có đống ý loại trừ";
        public static string MESSAGE_CONFIRM_ACCEPT = "Bạn có đống ý chấp nhận";
        public static string MESSAGE_CONFIRM_ACTIVE = "Bạn có đống ý kích hoạt";
        public static string MESSAGE_CONFIRM_DEACTIVE = "Bạn có đống ý bỏ kích hoạt";

        public static string MESSAGE_TRANSACTION_SUCCESS = "Success";
        public static string MESSAGE_TRANSACTION_FAIL = "Transaction fail";
        public static string MESSAGE_TRANSACTION_CHANGEPASSWORD_SUCCESS = "Change password success";
        public static string MESSAGE_TRANSACTION_CHANGEPASSWORD_FAIL = "Invalid password, please check again.";
        public static string MESSAGE_TRANSACTION_CHANGEPASSWORD_ERROR = "Failed, please check again.";
        public static string MESSAGE_EXIST_CODE = "MESSAGE_EXIST_CODE";
        public static string MESSAGE_IS_EFFECT_NOT_EDIT = "MESSAGE_IS_EFFECT_NOT_EDIT";
        public static string MESSAGE_IS_EFFECT_NOT_DELETE = "MESSAGE_IS_EFFECT_NOT_DELETE";
        public static string EXCEPTION = "Error";
        public static string INPUT_EMAIl = "Please enter email";
        public static string INPUT_USERNAME = "Please enter username";
        public static string INPUT_EMAIL_NO_FORMAT = "email not valid";
        public static string INPUT_FULLNAME = "Please enter fullname";
        public static string INPUT_PASSWORD = "Please enter password";
        public static string INPUT_PASSWORD_CONFIRM = "Please enter confirm password";
        public static string INPUT_PASSWORD_NOT_MATCH = "Password not match";
        public static string INPUT_PHONE = "Please enter number phone";
        public static string INPUT_ADDRESS = "Please enter address";
        public static string INPUT_NOT_SELECT_FILE = "Please choose file";
        public static string INPUT_UPLOAD_FILE_ERROR = "Too upload file failed, please reload";
        public static string INPUT_FIELD = "Please enter the information with a *";
        public static string LOGIN_SUCCESS = "Login Success";
        public static string LOGIN_FAIL = "Login fail, Please check username & password";
        public static string LOGIN_FAIL_PASSWORD = "Wrong password, Please try again";
        public static string LOGIN_FAIL_PASSWORD_TRANSACTION = "Wrong password transaction, Please try again";
        public static string LOGIN_NOT_CONFIRM = "Account not activated";

        public static string MESSAGE_PERMISSION_ACCESS_403_VN = "Bạn không có quyền truy cập trang này";
        public static string MESSAGE_PERMISSION_ACCESS_403 = "You can not access this page";
        public static string MESSAGE_PERMISSION_ACCESS_502_VN = "Lỗi máy chủ";
        public static string MESSAGE_PERMISSION_ACCESS_502 = "Server error";
        public static string MESSAGE_PERMISSION_ACCESS_200_VN = "Truy cập thành công";
        public static string MESSAGE_PERMISSION_ACCESS_200 = "Access success";
        public static string MESSAGE_PERMISSION_ACCESS_400_VN = " Gửi yêu cầu thất bại";
        public static string MESSAGE_PERMISSION_ACCESS_400 = "Send request failed";
    }
}

