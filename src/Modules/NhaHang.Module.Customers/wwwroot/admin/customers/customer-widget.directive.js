(function () {
    angular
        .module('simplAdmin.customers')
        .directive('customerWidget', customerWidget);

    function customerWidget() {
        var directive = {
            restrict: 'E',
            templateUrl: '_content/NhaHang.Module.Customers/admin/customers/customer-widget.directive.html',
            scope: {
                status: '=',
                numRecords: '='
            },
            controller: ['customerService', 'translateService', CustomerWidgetCtrl],
            controllerAs: 'vm',
            bindToController: true
        };

        return directive;
    }

    function CustomerWidgetCtrl(customerService, translateService) {
        var vm = this;
        vm.translate = translateService;
        vm.customers = [];
        vm.totalCustomers = 0;

        vm.$onInit = function () {
            customerService.getCustomers(vm.status, vm.numRecords).then(function (result) {
                vm.customers = result.data;
                vm.totalCustomers = vm.customers.length;
                console.log('Tổng số khách hàng liên hệ: ' + vm.totalCustomers);
            });
        };
    }
})();
