/*global angular*/
(function () {
    angular
        .module('simplAdmin.core')
        .factory('InformationService', ['$http', InformationService]);

    function InformationService($http) {
        var service = {
            geAbouts: geAbouts,
            updateAbout: updateAbout
        };
        return service;

        function geAbouts() {
            return $http.get('api/sitemaps');
        }

        function updateAbout(abouts) {
            return $http.post('api/sitemaps/', abouts);
        }
    }
})();