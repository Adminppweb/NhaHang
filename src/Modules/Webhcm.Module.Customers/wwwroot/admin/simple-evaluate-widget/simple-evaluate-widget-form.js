/*global angular, jQuery*/
(function ($) {
    angular
        .module('simplAdmin.customers')
        .controller('SimpleEvaluateWidgetFormCtrl', ['$state', '$stateParams', 'simpleEvaluateWidgetService', 'tinymceConfigService', 'translateService', SimpleEvaluateWidgetFormCtrl]);

    function SimpleEvaluateWidgetFormCtrl($state, $stateParams, simpleEvaluateWidgetService, tinymceConfigService, translateService) {
        var vm = this;
        vm.translate = translateService;
        vm.widgetInstance = { widgetZoneId: 2, displayOrder : 0, setting: { evaluates: [] }, publishStart: new Date() };
        vm.widgetZones = [];
        vm.widgetInstanceId = $stateParams.id;
        vm.isEditMode = vm.widgetInstanceId > 0;
        vm.datePickerPublishStart = {};
        vm.datePickerPublishEnd = {};
        vm.openCalendar = function (e, picker) {
            vm[picker].open = true;
        };
        vm.showProgress = false;


        // Tùy chọn của TinyMCE
        vm.tinymceDescriptionOptions = tinymceConfigService.getDefaultConfig();

        vm.save = function save() {
            vm.showProgress = true; 
                var promise;
                vm.widgetInstance.subName = vm.widgetInstance.subName === null ? '' : vm.widgetInstance.subName;
                if (vm.isEditMode) {
                    promise = simpleEvaluateWidgetService.editSimpleEvaluateWidget(vm.widgetInstance);
                } else {
                    promise = simpleEvaluateWidgetService.createSimpleEvaluateWidget(vm.widgetInstance);
                }
                promise
                    .then(function (result) {
                        vm.showProgress = false;
                        $state.go('widget');
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
                            vm.validationErrors.push(translateService.get('Could not create or edit simple evaluate widget.'));
                        }
                    });
        };

        function init() {
            simpleEvaluateWidgetService.getWidgetZones().then(function (result) {
                vm.widgetZones = result.data;
            });

            if (vm.isEditMode) {
                simpleEvaluateWidgetService.getSimpleEvaluateWidget(vm.widgetInstanceId).then(function (result) {
                    vm.widgetInstance = result.data;
                    if (vm.widgetInstance.publishStart) {
                        vm.widgetInstance.publishStart = new Date(vm.widgetInstance.publishStart);
                    }
                    if (vm.widgetInstance.publishEnd) {
                        vm.widgetInstance.publishEnd = new Date(vm.widgetInstance.publishEnd);
                    }
                });
            }
        }

        init();
    }
})(jQuery);
