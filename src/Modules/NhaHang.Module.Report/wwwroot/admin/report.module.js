/*global angular*/
(function () {
    'use strict';

    angular.module('simplAdmin.report', [])
        .config(['$stateProvider', function ($stateProvider) {
            $stateProvider
                //.state('report-list', {
                //    url: '/re/preview/:id',
                //    templateUrl: '_content/NhaHang.Module.Contacts/admin/contacts/contact.html',
                //    controller: 'ContactCtrl as vm'
                //})
                .state('report-list', {
                    url: '/report-list',
                    templateUrl: '_content/NhaHang.Module.Contacts/admin/report/contact.html',
                    controller: 'ContactCtrl as vm'
                });
        }]);
})();
