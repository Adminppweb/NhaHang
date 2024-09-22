/*global angular*/
(function () {
    'use strict';

    angular.module('simplAdmin.customers', [])
        .config(['$stateProvider', function ($stateProvider) {
            $stateProvider
                .state('customer', {
                    url: '/customer',
                    templateUrl: '_content/NhaHang.Module.Customers/admin/customers/customer-list.html',
                    controller: 'CustomerListCtrl as vm'
                })
                .state('customer-preview', {
                    url: '/customer/preview/:id',
                    templateUrl: '_content/NhaHang.Module.Customers/admin/customers/customer.html',
                    controller: 'CustomerCtrl as vm'
                })
                .state('evaluate', {
                    url: '/evaluate',
                    templateUrl: '_content/NhaHang.Module.Customers/admin/evaluate/evaluate-list.html',
                    controller: 'EvaluateListCtrl as vm'
                })
                .state('evaluate-create', {
                    url: '/evaluate-create',
                    templateUrl: '_content/NhaHang.Module.Customers/admin/evaluate/evaluate-form.html',
                    controller: 'EvaluateFormCtrl as vm'
                })
                .state('evaluate-edit', {
                    url: '/evaluate/edit/:id',
                    templateUrl: '_content/NhaHang.Module.Customers/admin/evaluate/evaluate-form.html',
                    controller: 'EvaluateFormCtrl as vm'
                })
                .state('widget-evaluate-create', {
                    url: '/widget-evaluate/create',
                    templateUrl: '_content/NhaHang.Module.Customers/admin/evaluate-widget/evaluate-widget-form.html',
                    controller: 'EvaluateWidgetFormCtrl as vm'
                })
                .state('widget-evaluate-edit', {
                    url: '/widget-evaluate/edit/:id',
                    templateUrl: '_content/NhaHang.Module.Customers/admin/evaluate-widget/evaluate-widget-form.html',
                    controller: 'EvaluateWidgetFormCtrl as vm'
                })
                .state('widget-simple-evaluate-create', {
                    url: '/widget-simple-evaluate/create',
                    templateUrl: '_content/NhaHang.Module.Customers/admin/simple-evaluate-widget/simple-evaluate-widget-form.html',
                    controller: 'SimpleEvaluateWidgetFormCtrl as vm'
                })
                .state('widget-simple-evaluate-edit', {
                    url: '/widget-simple-evaluate/edit/:id',
                    templateUrl: '_content/NhaHang.Module.Customers/admin/simple-evaluate-widget/simple-evaluate-widget-form.html',
                    controller: 'SimpleEvaluateWidgetFormCtrl as vm'
                });
        }]);
})();
