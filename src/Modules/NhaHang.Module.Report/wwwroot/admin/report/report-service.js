/*global angular*/
(function () {
    angular
        .module('simplAdmin.report')
        .factory('reportService', ['$http', reportService]);

    function reportService($http) {
        var service = {
            getReport: getReport,
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
