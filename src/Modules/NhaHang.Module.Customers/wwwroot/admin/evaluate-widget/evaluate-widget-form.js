/*global angular, jQuery*/
(function ($) {
    angular
        .module('simplAdmin.customers')
        .controller('EvaluateWidgetFormCtrl', ['$state', '$stateParams', 'evaluateWidgetService', 'tinymceConfigService', 'translateService', EvaluateWidgetFormCtrl]);

    function EvaluateWidgetFormCtrl($state, $stateParams, evaluateWidgetService, tinymceConfigService, translateService) {
        var vm = this;
        vm.translate = translateService;
        vm.widgetZones = [];
        vm.sorts = [];
        vm.widgetInstance = { description: '', widgetZoneId: 1, displayOrder: 0, setting: { numberOfEvaluates: 4 }, publishStart: new Date() };
        vm.widgetInstanceId = $stateParams.id;
        vm.isEditMode = vm.widgetInstanceId > 0;
        vm.datePickerPublishStart = {};
        vm.datePickerPublishEnd = {};
        vm.showProgress = false;

        // Tùy chọn của TinyMCE
        vm.tinymceDescriptionOptions = tinymceConfigService.getDefaultConfig();

        vm.openCalendar = function (e, picker) {
            vm[picker].open = true;
        };


        vm.save = function save() {
            vm.showProgress = true;
            var promise;
            vm.widgetInstance.subName = vm.widgetInstance.subName === null ? '' : vm.widgetInstance.subName;

            if (vm.isEditMode) {
                promise = evaluateWidgetService.editEvaluateWidget(vm.widgetInstance);
            } else {
                promise = evaluateWidgetService.createEvaluateWidget(vm.widgetInstance);
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
                        vm.validationErrors.push(translateService.get('Could not evaluate display widget.'));
                    }
                });
        };

        function init() {         
            evaluateWidgetService.getWidgetZones().then(function (result) {
                vm.widgetZones = result.data;
            });

            evaluateWidgetService.getEvaluateWidgetAvailableOrderBy().then(function (result) {
                vm.sorts = result.data;

                if (!vm.isEditMode) {
                    vm.widgetInstance.setting.orderBy = vm.sorts[0].id;
                }
            });

            if (vm.isEditMode) {
                evaluateWidgetService.getEvaluateWidget(vm.widgetInstanceId).then(function (result) {
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
