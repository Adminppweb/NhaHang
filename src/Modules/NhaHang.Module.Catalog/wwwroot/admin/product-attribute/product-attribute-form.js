/*global angular*/
(function () {
    angular
        .module('simplAdmin.catalog')
        .controller('ProductAttributeFormCtrl', ['$state', '$stateParams', 'productAttributeGroupService', 'productAttributeService', 'translateService', ProductAttributeFormCtrl]);

    function ProductAttributeFormCtrl($state, $stateParams, productAttributeGroupService, productAttributeService, translateService) {
        var vm = this;
        vm.translate = translateService;
        vm.productAttributeId = $stateParams.id;
        vm.isEditMode = vm.productAttributeId > 0;
        vm.productAttribute = {};
        vm.productAttributeGroups = [];
        vm.showProgress = false;

        vm.save = function save() {
            vm.showProgress = true;
            var promise;
            if (vm.isEditMode) {
                promise = productAttributeService.editProductAttribute(vm.productAttribute);
            } else {
                promise = productAttributeService.createProductAttribute(vm.productAttribute);
            }

            promise
                .then(function (result) {
                    vm.showProgress = false;
                    $state.go('product-attribute');
                })
                .catch(function (response) {
                    vm.showProgress = false;
                    var error = response.data;
                    vm.validationErrors = [];
                    if (error && angular.isObject(error)) {
                        for (var key in error) {
                            vm.validationErrors.push(error[key][0]);
                        }
                    } else {
                        vm.validationErrors.push(translateService.get('Could not add create product option.'));
                    }
                });
        };

        function getProductAttribute() {
            productAttributeService.getProductAttribute(vm.productAttributeId).then(function (result) {
                vm.productAttribute = result.data;
            });
        }

        function getProductAttributeGroups() {
            productAttributeGroupService.getProductAttributeGroups().then(function (result) {
                vm.productAttributeGroups = result.data;
            });
        }

        function init() {
            getProductAttributeGroups();
            if (vm.isEditMode) {
                getProductAttribute();
            }
        }

        init();
    }
})();
