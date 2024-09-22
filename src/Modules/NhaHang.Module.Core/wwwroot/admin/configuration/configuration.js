/*global angular, jQuery*/
(function ($) {
    angular
        .module('simplAdmin.core')
        .controller('ConfigurationCtrl', ['configurationService', 'translateService', ConfigurationCtrl]);

    function ConfigurationCtrl(configurationService, translateService) {
        var vm = this;
        vm.translate = translateService;
        vm.settings = {};
        vm.showProgress = false;

        vm.save = function save() {
            vm.showProgress = true;
            vm.validationErrors = [];
            configurationService.updateSetting(vm.settings)
                .then(function (result) {
                    vm.showProgress = false;
                    toastr.success(translateService.get('Application settings have been saved'));
                })
                .catch(function (response) {
                    vm.showProgress = false;
                    var error = response.data;
                    vm.validationErrors = [];
                    if (error && angular.isObject(error)) {
                        for (var key in error) {
                            vm.validationErrors.push(error[key][0]);
                        }
                    } else {
                        vm.validationErrors.push(translateService.get('Could not save settings.'));
                    }
                });
        };

        function init() {
            configurationService.getSettings().then(function (result) {
                vm.settings = result.data;
            });
        }

        init();
    }
})(jQuery);
