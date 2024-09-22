/*global $ */
$(function () {
    /*jshint multistr: true */
    var generalError = ' \
        <div class="modal-header"> \
           <button type="button" class="close pull-right" data-dismiss="modal">\
                <span aria-hidden="true"> <i class="fas fa-times" aria-hidden="true"></i></span>\
            <span class="sr-only"></span>\
                    </button>\
        <h4 class="modal-title" id = "myModalLabel"></h4>\
        </div> \
        <div class="d-flex flex-center py-2 text-center">\
                <div class="d-flex flex-column">\
            <i class="flaticon2-delete icon-4x text-danger"></i>\
            <h3 class="text-muted font-weight-bolder font-size-lg">Đã xảy ra lỗi. Không thể thêm vào giỏ hàng</h3>\
        </div>\
    </div>';

    var cartLockedError = ' \
        <div class="modal-header"> \
 <button type="button" class="close pull-right" data-dismiss="modal">\
                <span aria-hidden="true"> <i class="fas fa-times" aria-hidden="true"></i></span>\
            <span class="sr-only"></span>\
                    </button>\
        <h4 class="modal-title" id = "myModalLabel"></h4> \
</div> \
 <div class="d-flex flex-center py-2 text-center">\
                <div class="d-flex flex-column">\
            <i class="flaticon2-delete icon-4x text-danger"></i>\
            <h3 class="text-muted font-weight-bolder font-size-lg">Giỏ hàng đang bị khóa để thanh toán. Vui lòng hoàn tất hoặc hủy thanh toán trước </h3>\
       <p><a href="/checkout/shipping" class="btn btn btn-primary">Thanh toán</a></p> \
</div >\
    </div>';
   

    $('body').on('click', '.btn-add-cart', function () {
        $('#productOverview').modal('hide');
        var quantity,
            $form = $(this).closest("form"),
            productId = $(this).closest("form").find('input[name=productId]').val(),
            $quantityInput = $form.find('.quantity-field');

        quantity = $quantityInput.length === 1 ? $quantityInput.val() : 1;

        $.ajax({
            type: 'POST',
            url: '/cart/add-item',
            data: JSON.stringify({ productId: Number(productId), quantity: Number(quantity) }),
            contentType: "application/json"
        }).done(function (data) {
            if (data.success === false) {
                if (data.errorCode === "cart-locked") {
                    $('#shopModal').find('.modal-content').html(cartLockedError);
                } else {
                    $('#shopModal').find('.modal-content').html(generalError).find('.modal-body').text(data.errorMessage);
                }
            } else {
                $('#shopModal').find('.modal-content').html(data);
                $('.cart-badge .badge').text($('#shopModal').find('.cart-item-count').text());
            }
            $('#shopModal').modal('show');
        }).fail(function () {
            $('#shopModal').find('.modal-content').html(generalError);
            $('#shopModal').modal('show');
        });
    });
});
