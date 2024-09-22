/*global angular*/
(function () {
    angular
        .module('simplAdmin.customers')
        .controller('EvaluateFormCtrl', ['$state', '$stateParams', 'evaluateService', 'translateService', EvaluateFormCtrl]);

    function EvaluateFormCtrl($state, $stateParams, evaluateService, translateService) {
        var vm = this;
        vm.translate = translateService;
        vm.evaluate = { description: '', isPublished: true };
        vm.evaluateId = $stateParams.id;
        vm.isEditMode = vm.evaluateId > 0;
        vm.thumbnailImage = null;
        vm.showProgress = false;

        vm.updateSlug = function () {
            vm.evaluate.slug = slugify(vm.evaluate.name);
        };

        vm.tinymceContentOptions = tinymceConfigService.getDefaultConfig();
        vm.copySlug = function () {
            if (vm.evaluate.slug) {
                var inputElement = document.createElement('input');
                inputElement.setAttribute('value', vm.evaluate.slug);
                document.body.appendChild(inputElement);
                inputElement.select();
                document.execCommand('copy');
                document.body.removeChild(inputElement);

                toastr.success(translateService.get('Slug copied successfully'));
            }
        };

        vm.save = function save() {
            vm.showProgress = true;
            var promise;
            vm.evaluate.phone = vm.evaluate.phone === null ? '' : vm.evaluate.phone;
            vm.evaluate.email = vm.evaluate.email === null ? '' : vm.evaluate.email;
            vm.evaluate.address = vm.evaluate.address === null ? '' : vm.evaluate.address;
            vm.evaluate.position = vm.evaluate.position === null ? '' : vm.evaluate.position;
            vm.evaluate.company = vm.evaluate.company === null ? '' : vm.evaluate.company;
            vm.evaluate.content = vm.evaluate.content === null ? '' : vm.evaluate.content;
            vm.evaluate.idYoutube = vm.evaluate.idYoutube === null ? '' : vm.evaluate.idYoutube;
            vm.evaluate.sourceYou = vm.evaluate.sourceYou === null ? '' : vm.evaluate.sourceYou;
            vm.evaluate.linkImage = vm.evaluate.linkImage === null ? '' : vm.evaluate.linkImage;

            if (vm.isEditMode) {
                promise = evaluateService.editEvaluate(vm.evaluate, vm.thumbnailImage);
            } else {
                promise = evaluateService.createEvaluate(vm.evaluate, vm.thumbnailImage);
            }

            promise
                .then(function (response) {
                    vm.showProgress = false;
                    result = response.data;
                    if (result.statusCode == 200) {
                        toastr.success(translateService.get('Save change success'));
                        $state.go('evaluate', { id: result.result });
                    }
                    else {
                        toastr.error(translateService.get('Excuse me. An error occurred during execution'));
                    }
                })
                .catch(function (response) {
                    vm.showProgress = false;
                    var error = response.data;
                    vm.validationErrors = [];
                    if (error && angular.isObject(error)) {
                        var errors = error.errors ? error.errors : error;
                        for (var key in errors) {
                            vm.validationErrors.push(errors[key][0]);
                        }
                    } else {
                        vm.validationErrors.push(translateService.get('Could not add evaluate.'));
                    }
                });
        };

        function init() {
            if (vm.isEditMode) {
                evaluateService.getEvaluate(vm.evaluateId).then(function (result) {
                    vm.evaluate = result.data;
                });
            }
        }

        init();
    }
})();
