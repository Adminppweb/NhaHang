/*global angular*/
(function () {
    angular
        .module('simplAdmin.catalog')
        .factory('productTemplateService', ['$http', productTemplateService]);

    function productTemplateService($http) {
        var service = {
            getProductTemplate: getProductTemplate,
            createProductTemplate: createProductTemplate,
            editProductTemplate: editProductTemplate,
            deleteProductTemplate: deleteProductTemplate,
            getProductTemplates: getProductTemplates
        };
        return service;

        function getProductTemplate(id) {
            return $http.get('api/product-templates/' + id);
        }

        function getProductTemplates() {
            return $http.get('api/product-templates');
        }

        function createProductTemplate(productTemplate) {
            return $http.post('api/product-templates', productTemplate);
        }

        function editProductTemplate(productTemplate) {
            return $http.post('api/product-templates/update-product-template/' + productTemplate.id, productTemplate);
        }

        function deleteProductTemplate(productTemplate) {
            return $http.post('api/product-templates/delete-product-template/' + productTemplate.id, null);
        }
    }
})();
