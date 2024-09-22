/*global angular*/
(function () {
    angular
        .module('simplAdmin.report')
        .factory('InvoiceService', ['$http', InvoiceService]);

    function InvoiceService($http) {
        var service = {
            getInvoice: getInvoice,
            deleteReport: deleteReport,
            getContacts: getReports
        };
        return service;

        function getReport(id) {
            return $http.get('api/report/' + id);
        }

        function getReports(params) {
            return $http.post('api/report/grid',params);
        }        

        function deleteReport(report) {
            return $http.post('api/report/' + report.id, null);
        }
    }
})();
