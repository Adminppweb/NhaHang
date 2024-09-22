(function () {
    angular
        .module('simplAdmin.common')
        .factory('fileUploadService', fileUploadService);

    /* @ngInject */
    function fileUploadService($http) {
        var service = {
            uploadFile: uploadFile
        };
        return service;

        function uploadFile(file, uploadUrl) {
            var data = new FormData();
            data.append("file", file);
            return $http.post(uploadUrl, data, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            });
        }
    }
})();
