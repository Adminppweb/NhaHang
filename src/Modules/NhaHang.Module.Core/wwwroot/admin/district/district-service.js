/*global angular*/
(function () {
    angular
        .module('simplAdmin.core')
        .factory('districtService', ['$http', districtService]);

    function districtService($http) {
        var service = {
            editDistrict: editDistrict,
            getDistrict: getDistrict,
            createDistrict: createDistrict,
            deleteDistrict: deleteDistrict,
            getDistricts: getDistricts
        };
        return service;

        function getDistricts(stateProvinceId, params) {
            return $http.post('api/districts/grid?stateProvinceId=' + stateProvinceId, params);
        }

        function getDistrict(id) {
            return $http.get('api/districts/' + id, null);
        }

        function editDistrict(district) {
            return $http.put('api/districts/' + district.id, district);
        }

        function createDistrict(district) {
            return $http.post('api/districts/', district);
        }

        function deleteDistrict(district) {
            return $http.delete('api/districts/' + district.id, null);
        }
    }
})();
