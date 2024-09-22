(function () {
    angular
        .module('simplAdmin.orders')
        .directive('ordersumWidget', ordersumWidget);

    function ordersumWidget() {
        var directive = {
            restrict: 'E',
            templateUrl: '_content/NhaHang.Module.Orders/admin/order/order-sum-widget.directive.html',
            scope: {
                status: '=',
                numRecords: '='
            },
            controller: ['orderService', 'translateService', OrderSumWidgetCtrl],
            controllerAs: 'vm',
            bindToController: true
        };

        return directive;
    }

    function OrderSumWidgetCtrl(orderService, translateService) {
        var vm = this;
        vm.translate = translateService;
        vm.orders = [];
        vm.totalOrders = 0;

        vm.$onInit = function () {
            orderService.getOrders(vm.status, vm.numRecords).then(function (result) {
                vm.orders = result.data;
                vm.totalOrders = vm.orders.length;
                console.log('Tổng số đơn hàng: ' + vm.totalOrders);
            });
        };
    }
})();
