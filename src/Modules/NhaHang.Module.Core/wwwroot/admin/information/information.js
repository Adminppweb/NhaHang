/*global angular, jQuery*/
(function ($) {
    angular
        .module('simplAdmin.core')
        .controller('InfomationFormCtrl', ['InformationService', 'translateService', InfomationFormCtrl]);

    function InfomationFormCtrl(InformationService, translateService) {
        var vm = this;
        vm.translate = translateService;
        vm.infomation = {};
        vm.showProgress = false;

        // Tùy chọn của TinyMCE
        vm.tinymceDescriptionOptions = {
            menubar: true,
            plugins: 'anchor autolink charmap codesample emoticons image link lists media searchreplace table visualblocks wordcount code linkchecker preview fullscreen',
            toolbar: 'undo redo blocks styleselect fontselect fontsizeselect | bold italic underline strikethrough | align lineheight checklist lists numlist bullist indent outdent | emoticons charmap removeformat fullscreen',
            max_height: 300,
            min_height: 150,
            max_width: 500,
            min_width: 400
        };

        vm.save = function save() {
            vm.showProgress = true;
            vm.validationErrors = [];
            vm.infomation.open = $('#open').val();
            vm.infomation.close = $('#close').val();
            InformationService.updateAbout(vm.infomation)
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

        vm.setTime = function setTime(event, time)
        {}


        function init() {
            InformationService.geAbouts().then(function (result) {
                vm.infomation = result.data;
                $('#open').val(vm.infomation.open);
                $('#close').val(vm.infomation.close);
            });
        }

        init();
    }
})(jQuery);