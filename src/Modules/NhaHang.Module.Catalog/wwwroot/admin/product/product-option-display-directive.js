﻿/*global angular confirm*/
(function () {
    angular
        .module('simplAdmin.catalog')
        .directive('productOptionDisplayDirective', productOptionDisplayDirective);

    function productOptionDisplayDirective() {
        var directive = {
            restrict: 'E',
            templateUrl: '_content/NhaHang.Module.Catalog/admin/product/product-option-display-directive.html',
            scope: {
                option: '=option',
                modelId: '@modelId',
                title: '@title'
            },
            controller: ProductOptionDisplayCtrl,
            controllerAs: 'vm',
            bindToController: true
        };

        return directive;
    }

    /* @ngInject */
    function ProductOptionDisplayCtrl() {
        var vm = this;
    }
})();
