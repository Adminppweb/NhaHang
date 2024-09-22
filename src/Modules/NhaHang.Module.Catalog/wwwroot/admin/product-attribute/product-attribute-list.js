/*global angular, confirm*/
(function () {
    angular
        .module('simplAdmin.catalog')
        .controller('ProductAttributeListCtrl', ['productAttributeService', 'translateService', ProductAttributeListCtrl]);

    function ProductAttributeListCtrl(productAttributeService, translateService) {
        var vm = this;
        vm.translate = translateService;
        vm.productAttributes = [];

        vm.getProductAttributes = function getProductAttributes() {
            productAttributeService.getProductAttributes().then(function (result) {
                vm.productAttributes = result.data;
            });
        };

        vm.deleteProductAttribute = function deleteProductAttribute(productAttribute) {
            bootbox.confirm(translateService.get('Are you sure you want to delete this template: ') + simplUtil.escapeHtml(productAttribute.name), function (result) {
                if (result) {
                    productAttributeService.deleteProductAttribute(productAttribute)
                        .then(function (result) {
                            vm.getProductAttributes();
                            toastr.success(productAttribute.name + translateService.get(' has been deleted'));
                        })
                        .catch(function (response) {
                            toastr.error(response.data.error);
                        });
                }
            });
        };

        vm.getProductAttributes();
    }
})();
