/*global angular, confirm*/
(function () {
    angular
        .module('simplAdmin.catalog')
        .controller('ProductAttributeGroupListCtrl', ['productAttributeGroupService', 'translateService', ProductAttributeGroupListCtrl]);

    function ProductAttributeGroupListCtrl(productAttributeGroupService, translateService) {
        var vm = this;
        vm.translate = translateService;
        vm.productAttributeGroups = [];

        vm.getProductAttributeGroups = function getProductAttributeGroups() {
            productAttributeGroupService.getProductAttributeGroups().then(function (result) {
                vm.productAttributeGroups = result.data;
            });
        };

        //vm.deleteProductAttributeGroup = function deleteProductAttributeGroup(productAttributeGroup) {
        //    if (confirm("Are you sure?")) {
        //        productAttributeGroupService.deleteProductAttributeGroup(productAttributeGroup)
        //            .then(function (result) {
        //                vm.getProductAttributeGroups();
        //            });
        //    }
        //};
        vm.deleteProductAttributeGroup = function deleteProductAttributeGroup(productAttributeGroup) {
            bootbox.confirm(translateService.get('Are you sure you want to delete this template: ') + simplUtil.escapeHtml(productAttributeGroup.name), function (result) {
                if (result) {
                    productAttributeGroupService.deleteProductAttributeGroup(productAttributeGroup)
                        .then(function (result) {
                            vm.getProductAttributeGroups();
                            toastr.success(productAttributeGroup.name + translateService.get(' has been deleted'));
                        })
                        .catch(function (response) {
                            toastr.error(response.data.error);
                        });
                }
            });
        };

        vm.getProductAttributeGroups();
    }
})();


