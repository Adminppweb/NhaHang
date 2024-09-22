/*global angular*/
(function () {
    angular
        .module('simplAdmin.customers')
        .controller('CustomerCtrl', ['$stateParams', 'customerService', 'translateService', CustomerCtrl]);

    function CustomerCtrl($stateParams, customerService, translateService) {
        var vm = this;
        vm.translate = translateService;
        vm.customer = {};
        vm.customerId = $stateParams.id;

        function init() {
            customerService.getCustomer(vm.customerId).then(function (result) {
                vm.customer = result.data;
            });
        }

        init();
    }
})();
