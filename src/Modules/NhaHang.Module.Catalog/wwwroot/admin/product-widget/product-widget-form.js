/*global angular, jQuery*/
(function ($) {
    angular
        .module('simplAdmin.catalog')
        .controller('ProductWidgetFormCtrl', ['$state', '$stateParams', 'productWidgetService', 'categoryService', 'translateService', ProductWidgetFormCtrl]);

    function ProductWidgetFormCtrl($state, $stateParams, productWidgetService,  categoryService, translateService) {
        var vm = this;
        vm.translate = translateService;
        vm.widgetZones = [];
        vm.sorts = [];
        vm.widgetInstance = { description: '', widgetZoneId: 1, displayOrder: 0, setting: { numberOfProducts: 4 }, publishStart: new Date() };
        vm.widgetInstanceId = $stateParams.id;
        vm.isEditMode = vm.widgetInstanceId > 0;
        vm.categories = [];

        vm.datePickerPublishStart = {};
        vm.datePickerPublishEnd = {};
        vm.showProgress = false;

        // Tùy chọn của TinyMCE
        vm.tinymceDescriptionOptions = {
            menubar: false,
            plugins: 'anchor autolink charmap codesample emoticons image link lists media searchreplace table visualblocks wordcount code linkchecker preview fullscreen',
            toolbar: 'undo redo blocks styleselect fontselect fontsizeselect | bold italic underline strikethrough | align lineheight checklist lists numlist bullist indent outdent | emoticons charmap removeformat fullscreen',
            max_height: 300,
            min_height: 150,
            max_width: 500,
            min_width: 400
        };

        vm.openCalendar = function (e, picker) {
            vm[picker].open = true;
        };


        vm.save = function save() {
            vm.showProgress = true;
            var promise;
            vm.widgetInstance.subName = vm.widgetInstance.subName === null ? '' : vm.widgetInstance.subName;

            if (vm.isEditMode) {
                promise = productWidgetService.editProductWidget(vm.widgetInstance);
            } else {
                promise = productWidgetService.createProductWidget(vm.widgetInstance);
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
                        vm.validationErrors.push(translateService.get('Could not product display widget.'));
                    }
                });
        };

        function init() {         
            productWidgetService.getWidgetZones().then(function (result) {
                vm.widgetZones = result.data;
            });

            categoryService.getCategories().then(function (result) {
                vm.categories = result.data;
            });

            productWidgetService.getProductWidgetAvailableOrderBy().then(function (result) {
                vm.sorts = result.data;

                if (!vm.isEditMode) {
                    vm.widgetInstance.setting.orderBy = vm.sorts[0].id;
                }
            });

            if (vm.isEditMode) {
                productWidgetService.getProductWidget(vm.widgetInstanceId).then(function (result) {
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
