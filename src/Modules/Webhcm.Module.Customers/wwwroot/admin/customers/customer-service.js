/*global angular*/
(function () {
    angular
        .module('simplAdmin.customers')
        .factory('customerService', ['$http', customerService]);

    function customerService($http) {
        var service = {
            getCustomer: getCustomer,
            deleteCustomer: deleteCustomer,
            getCustomers: getCustomers
        };
        return service;

        function getCustomer(id) {
            return $http.get('api/customers/' + id);
        }

        function getCustomers(params) {
            return $http.post('api/customers/grid',params);
        }        

        function deleteCustomer(customer) {
            return $http.post('api/customers/delete-customer/' + customer.id, null);
        }
    }
})();
