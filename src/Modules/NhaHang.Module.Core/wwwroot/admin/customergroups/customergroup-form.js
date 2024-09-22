/*global angular*/
(function () {
    angular
        .module('simplAdmin.core')
        .controller('CustomerGroupFormCtrl', ['$state', '$stateParams', 'customergroupService', 'translateService', CustomerGroupFormCtrl]);

    function CustomerGroupFormCtrl($state, $stateParams, customergroupService, translateService) {
        var vm = this;
        vm.translate = translateService;
        vm.customergroup = {};
        vm.customergroupId = $stateParams.id;
        vm.isEditMode = vm.customergroupId > 0;
        vm.showProgress = false;

        vm.save = function save() {
            vm.showProgress = true;
            var promise;
            if (vm.isEditMode) {
                promise = customergroupService.editCustomerGroup(vm.customergroup);
            } else {
                promise = customergroupService.createCustomerGroup(vm.customergroup);
            }

            promise
                .then(function (response) {
                    vm.showProgress = false;
                    result = response.data;
                    if (result.statusCode == 200) {
                        toastr.success(translateService.get('Save change success'));
                        $state.go('customergroups', { id: result.result });
                    }
                    else {
                        toastr.error(translateService.get('Excuse me. An error occurred during execution'));
                    }
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
                        vm.validationErrors.push(translateService.get('Could not add customer group.'));
                    }
                });
        };

        function init() {
            if (vm.isEditMode) {
                customergroupService.getCustomerGroup(vm.customergroupId).then(function (result) {
                    vm.customergroup = result.data;
                });
            }
        }

        init();
    }
})();
