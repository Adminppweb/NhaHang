$(document).ready(function () {
    // Xử lý sự kiện khi nút "Mở Form Modal" được nhấn
    $('#openFormModal').on('click', function () {
        // Mở modal
        $('#yourFormModalId').modal('show');
    });

    // Xử lý sự kiện khi nút "Gửi ngay" được nhấn trong form modal
    $('#btnSubmit').on('click', function () {
        // Gửi dữ liệu form bằng AJAX
        $.ajax({
            url: '/customer',
            type: 'POST',
            data: $('#submitForm').serialize(),
            success: function (response) {
                if (response.success) {
                    // Hiển thị modal thành công
                    $('#successModal').modal('show');
                } else {
                    // Hiển thị modal lỗi và thông báo lỗi
                    $('#errorModal').modal('show');
                    // Hiển thị lỗi từ response.errors (nếu có)
                }
            }
        });
    });

    // Đóng modal khi nút "x" được nhấn
    $('.closed').on('click', function () {
        $('#successModal, #errorModal, #yourFormModalId').modal('hide');
    });
});
