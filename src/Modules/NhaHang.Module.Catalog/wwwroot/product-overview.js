/*global $ */
$(function () {
    $('body').on('click', '.quick-view', function () {
        var productId = $(this).closest('.product-overview').find('[name="productId"]').val();
        $.ajax({
            type: 'GET',
            url: '/product/product-overview?id=' + productId,
            contentType: "application/json"
        }).done(function (data) {
            $('#productOverview .modal-body').html(data);
            $('#productOverview').modal('show');
            $('.sp-wrap').smoothproducts();

            $('.product-attrs li').on('click', function () {
                var $variationDiv,
                    selectedproductOptions = [],
                    variationName,
                    $form = $(this).closest("form"),
                    $attrOptions = $form.find('.product-attr-options');

                $(this).find('input').prop('checked', true);

                $attrOptions.each(function () {
                    selectedproductOptions.push($(this).find('input[type=radio]:checked').val());
                });
                variationName = selectedproductOptions.join('-');
                $variationDiv = $('div[data-variation-name="' + variationName + '"]');
                $imagesVariationDiv = $('div[data-images-variation-name="' + variationName + '"]');
                $('.product-variation').hide();
                $('.product-images').hide();

                if ($variationDiv.length > 0) {
                    $variationDiv.show();
                    $('.product-variation-notavailable').hide();
                } else {
                    $('.product-variation-notavailable').show();
                }
                if ($imagesVariationDiv.length > 0) {
                    $imagesVariationDiv.show();
                }
                else {
                    $('#main-product-images').show();
                }
            });
            $('.quantity-button').on('click', function () {
                var quantityInput = $(this).closest("form").find('.quantity-field');
                if ($(this).val() === '+') {
                    quantityInput.val(parseInt(quantityInput.val(), 10) + 1);
                }
                else if (quantityInput.val() > 1) {
                    quantityInput.val(quantityInput.val() - 1);
                }
            });

        });
       

    });
});
