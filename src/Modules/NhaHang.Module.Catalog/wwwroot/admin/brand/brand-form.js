/*global angular*/
(function () {
    angular
        .module('simplAdmin.catalog')
        .controller('BrandFormCtrl', ['$state', '$stateParams', 'brandService', 'translateService', BrandFormCtrl]);

    function BrandFormCtrl($state, $stateParams, brandService, translateService) {
        var vm = this;
        vm.translate = translateService;
        vm.brand = { description: '', isPublished: true };
        vm.brandId = $stateParams.id;
        vm.isEditMode = vm.brandId > 0;
        vm.showProgress = false;

        vm.updateSlug = function () {
            vm.brand.slug = slugify(vm.brand.name);
        };

        vm.tinymceDescriptionOptions = {
            menubar: true,
            toolbar: [
                "undo redo cut copy paste blocks styleselect fontselect fontsizeselect bold italic underline strikethrough | align lineheight checklist bullist numlist outdent indent",
                "blockquote subscript superscript | advlist lists autolink table emoticons charmap | link image media | print preview code removeformat fullscreen"],
            plugins: 'anchor autolink charmap codesample emoticons image link lists media searchreplace table visualblocks wordcount code linkchecker preview fullscreen',
            max_height: 500,
            min_height: 800,
            max_width: 800,
            min_width: 400
        };
        vm.tinymceMetaDescriptionOptions = {
            menubar: false,
            plugins: 'anchor autolink charmap codesample emoticons image link lists media searchreplace table visualblocks wordcount code linkchecker preview fullscreen',
            toolbar: 'undo redo blocks styleselect fontselect fontsizeselect | bold italic underline strikethrough | align lineheight checklist lists numlist bullist indent outdent | emoticons charmap removeformat fullscreen',
            max_height: 300,
            min_height: 150,
            max_width: 500,
            min_width: 400
        };

        vm.copySlug = function () {
            if (vm.brand.slug) {
                var inputElement = document.createElement('input');
                inputElement.setAttribute('value', vm.brand.slug);
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
                vm.brand.metaTitle = vm.brand.metaTitle === null ? '' : vm.brand.metaTitle;
                vm.brand.metaKeywords = vm.brand.metaKeywords === null ? '' : vm.brand.metaKeywords;
                vm.brand.metaDescription = vm.brand.metaDescription === null ? '' : vm.brand.metaDescription;

                if (vm.isEditMode) {
                    promise = brandService.editBrand(vm.brand);
                } else {
                    promise = brandService.createBrand(vm.brand);
                }

                promise
                    .then(function (response) {
                        vm.showProgress = false; 
                        result = response.data;
                        if (result.statusCode == 200) {
                            toastr.success(translateService.get('Save change success'));
                            $state.go('brand', { id: result.result });
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
                            vm.validationErrors.push(translateService.get('Could not add brand.'));
                        }
                    });
        };

        function init() {
            if (vm.isEditMode) {
                brandService.getBrand(vm.brandId).then(function (result) {
                    vm.brand = result.data;
                });
            }
        }

        init();
    }
})();
