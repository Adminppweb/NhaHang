
(function () {
    'use strict';

    angular
        .module('simplAdmin.elfinder', [])
        .config(['$stateProvider', function ($stateProvider) {
            $stateProvider.state('file-manager', {
                url: '/file-manager',
                templateUrl: "/admin/file-manager"
            });
        }]);
})();