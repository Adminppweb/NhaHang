/*global angular*/
(function () {
    angular
        .module('simplAdmin.catalog')
        .controller('ProductOptionFormCtrl', ['$state', '$stateParams', 'productOptionService', 'translateService', ProductOptionFormCtrl]);

    function ProductOptionFormCtrl($state, $stateParams, productOptionService, translateService) {
        var vm = this;
        vm.translate = translateService;
        vm.productOptionId = $stateParams.id;
        vm.isEditMode = vm.productOptionId > 0;

        vm.productOption = {};
        vm.showProgress = false;

        vm.save = function save() {
            vm.showProgress = true;
            var promise;
            if (vm.isEditMode) {
                promise = productOptionService.editProductOption(vm.productOption);
            } else {
                promise = productOptionService.createProductOption(vm.productOption);
            }

            promise
                .then(function (result) {
                    vm.showProgress = false;
                    $state.go('product-option');
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

        function getProductOption() {
            productOptionService.getProductOption(vm.productOptionId).then(function (result) {
                vm.productOption = result.data;
            });
        }

        function init() {
            if (vm.isEditMode) {
                getProductOption();
            }
        }

        init();
    }
})();
