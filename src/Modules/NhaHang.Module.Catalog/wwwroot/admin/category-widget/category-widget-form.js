/*global angular, jQuery*/
(function ($) {
    angular
        .module('simplAdmin.catalog')
        .controller('CategoryWidgetFormCtrl', ['$state', '$stateParams', 'categoryWidgetService', 'translateService', CategoryWidgetFormCtrl]);

    function CategoryWidgetFormCtrl($state, $stateParams, categoryWidgetService, translateService) {
        var vm = this;
        vm.translate = translateService;
        vm.widgetZones = [];
        vm.categories = [];
        vm.widgetInstance = { widgetZoneId: 1, settings: { categoryId: 1 }, publishStart: new Date() };
        vm.widgetInstanceId = $stateParams.id;
        vm.isEditMode = vm.widgetInstanceId > 0;
        vm.numberOfWidgets = [];
        vm.datePickerPublishStart = {};
        vm.datePickerPublishEnd = {};
        vm.openCalendar = function (e, picker) {
            vm[picker].open = true;
        };
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

        vm.save = function save() {
            vm.showProgress = true;
            var promise;
            if (vm.isEditMode) {
                promise = categoryWidgetService.editCategoryWidget(vm.widgetInstance);
            } else {
                promise = categoryWidgetService.createCategoryWidget(vm.widgetInstance);
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
                        vm.validationErrors.push(translateService.get('Could not category display widget.'));
                    }
                });
        };


        function init() {
            categoryWidgetService.getWidgetZones().then(function (result) {
                vm.widgetZones = result.data;
            });
            categoryWidgetService.getCategories().then(function (result) {
                vm.categories = result.data;
            });

            categoryWidgetService.getNumberOfWidgets().then(function (result) {
                var count = parseInt(result.data);
                if (!vm.isEditMode) {
                    count = count + 1;
                }

                for (var i = 1; i <= count; i++)
                    vm.numberOfWidgets.push(i);
            });

            if (vm.isEditMode) {
                categoryWidgetService.getCategoryWidget(vm.widgetInstanceId).then(function (result) {
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
