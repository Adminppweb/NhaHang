/*global angular, confirm*/
(function () {
    angular
        .module('simplAdmin.customers')
        .controller('CustomerListCtrl', ['customerService', 'customerAreaService', 'translateService', CustomerListCtrl]);

    function CustomerListCtrl(customerService, customerAreaService, translateService) {
        var vm = this;
        vm.tableStateRef = {};
        vm.translate = translateService;
        vm.customers = [];

        customerAreaService.getCustomerAreas().then(function (result) {
            vm.customerAreas = result.data;
        });

        vm.getCustomers = function getCustomers(tableState) {
            vm.tableStateRef = tableState;
            vm.isLoading = true;
            customerService.getCustomers(tableState).then(function (result) {
                vm.customers = result.data.items;
                tableState.pagination.numberOfPages = result.data.numberOfPages;
                tableState.pagination.totalItemCount = result.data.totalRecord;
                vm.isLoading = false;
            });
        };        

        vm.deleteCustomer = function deleteCustomer(customer) {
            bootbox.confirm(translateService.get('Are you sure you want to delete this customer: ') + simplUtil.escapeHtml(customer.fullName), function (result) {
                if (result) {
                    customerService.deleteCustomer(customer)
                       .then(function (result) {
                           vm.getCustomers(vm.tableStateRef);
                           toastr.success(customer.name + translateService.get(' has been deleted'));
                       })
                       .catch(function (response) {
                           toastr.error(response.data.error);
                       });
                }
            });
        };
    }
})();
