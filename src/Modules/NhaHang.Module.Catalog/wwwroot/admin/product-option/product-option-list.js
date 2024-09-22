/*global angular, confirm*/
(function () {
    angular
        .module('simplAdmin.catalog')
        .controller('ProductOptionListCtrl', ['productOptionService', 'translateService', ProductOptionListCtrl]);

    function ProductOptionListCtrl(productOptionService, translateService) {
        var vm = this;
        vm.translate = translateService;
        vm.productOptions = [];

        vm.getProductOptions = function getProductOptions() {
            productOptionService.getProductOptions().then(function (result) {
                vm.productOptions = result.data;
            });
        };

        //vm.deleteProductOption = function deleteProductOption(productOption) {
        //    if (confirm("Are you sure?")) {
        //        productOptionService.deleteProductOption(productOption)
        //            .then(function (result) {
        //                vm.getProductOptions();
        //            });
        //    }
        //};

        vm.deleteProductOption = function deleteProductOption(productOption) {
            bootbox.confirm(translateService.get('Are you sure you want to delete this template: ') + simplUtil.escapeHtml(productOption.name), function (result) {
                if (result) {
                    productOptionService.deleteProductOption(productOption)
                        .then(function (result) {
                            vm.getProductOptions();
                            toastr.success(productOption.name + translateService.get(' has been deleted'));
                        })
                        .catch(function (response) {
                            toastr.error(response.data.error);
                        });
                }
            });
        };

        vm.getProductOptions();
    }
})();

