﻿/*global angular, jQuery*/
(function ($) {
    angular
        .module('simplAdmin.paymentCoD')
        .controller('CoDConfigFormCtrl', ['paymentCoDService', 'translateService', '$state', CoDConfigFormCtrl]);

    function CoDConfigFormCtrl(paymentCoDService, translateService, $state) {
        var vm = this;
        vm.translate = translateService;
        vm.codConfig = {};

        vm.save = function save() {
            vm.validationErrors = [];
            paymentCoDService.updateSetting(vm.codConfig)
                .then(function (result) {
                    toastr.success('Settings have been saved');
                    $state.go('payment-providers');
                })
                .catch(function (response) {
                    var error = response.data;
                    vm.validationErrors = [];
                    if (error && angular.isObject(error)) {
                        for (var key in error) {
                            vm.validationErrors.push(error[key][0]);
                        }
                    } else {
                        vm.validationErrors.push('Could not save settings.');
                    }
                });
        };

        function init() {
            paymentCoDService.getSettings().then(function (result) {
                vm.codConfig = result.data;
            });
        }

        init();
    }
})(jQuery);
