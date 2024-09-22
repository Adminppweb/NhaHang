
/*global angular*/
(function () {
    angular
        .module('simplAdmin.customers')
        .factory('evaluateService', ['$http', 'Upload', evaluateService]);

    function evaluateService($http, Upload) {
        var service = {
            quickSearchEvaluates: quickSearchEvaluates,
            getEvaluates: getEvaluates,
            createEvaluate: createEvaluate,
            editEvaluate: editEvaluate,
            getEvaluate: getEvaluate,
            deleteEvaluate: deleteEvaluate
        };
        return service;

        function quickSearchEvaluates(name) {
            return $http.get('api/evaluates/quick-search?name=' + name);
        }

        function getEvaluate(id) {
            return $http.get('api/evaluates/' + id);
        }

        function getEvaluates(params) {
            return $http.post('api/evaluates/grid', params);
        }

        function createEvaluate(evaluate, thumbnailImage) {
            return Upload.upload({
                url: 'api/evaluates',
                data: {
                    evaluate: evaluate,
                    thumbnailImage: thumbnailImage
                }
            });
        }

        function editEvaluate(evaluate, thumbnailImage) {
            return Upload.upload({
                url: 'api/evaluates/update-evaluate/' + evaluate.id,
                method: 'POST',
                data: {
                    evaluate: evaluate,
                    thumbnailImage: thumbnailImage
                }
            });
        }
        function deleteEvaluate(evaluate) {
            return $http.post('api/evaluates/delete-evaluate/' + evaluate.id, null);
        }
    }
})();
