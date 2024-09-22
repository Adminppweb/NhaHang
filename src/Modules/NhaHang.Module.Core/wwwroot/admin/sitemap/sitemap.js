/*global angular, jQuery*/
(function ($) {
    angular
        .module('simplAdmin.core')
        .controller('InfomationFormCtrl', ['InformationService', 'summerNoteService', 'translateService', InfomationFormCtrl]);

    function InfomationFormCtrl(InformationService, summerNoteService, translateService) {
        var vm = this;
        vm.translate = translateService;
        vm.infomation = {};


        vm.descUpload = function (files) {
            summerNoteService.upload(files[0])
                .then(function (response) {
                    $(vm.descEditor).summernote('insertImage', response.data);
                });
        };
        vm.save = function save() {
            vm.validationErrors = [];

            vm.infomation.open = $('#open').val();
            vm.infomation.close = $('#close').val();
            InformationService.updateAbout(vm.infomation)
                .then(function (result) {
                    toastr.success(translateService.get('Application settings have been saved'));
                })
                .catch(function (response) {
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
        {
           //$(event)
           
        }


        function init() {
            InformationService.geAbouts().then(function (result) {
                vm.infomation = result.data;
                $('#open').val(vm.infomation.open);
                $('#close').val(vm.infomation.close);
                //$('#open').val(vm.Information.open);
                //$('#close').val(vm.Information.close);
            });
        }

        init();
    }
})(jQuery);