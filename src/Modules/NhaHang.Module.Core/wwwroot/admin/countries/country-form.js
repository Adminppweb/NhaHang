 /*global angular*/
(function () {
    angular
        .module('simplAdmin.core')
        .controller('CountryFormCtrl', ['$state', '$stateParams', 'countryService', 'translateService', CountryFormCtrl]);

    function CountryFormCtrl($state, $stateParams, countryService, translateService) {
        var vm = this;
        vm.translate = translateService;
        vm.country = {};
        vm.countryId = $stateParams.id;
        vm.isEditMode = vm.countryId;
        vm.showProgress = false;


        vm.save = function save() {
            vm.showProgress = true;
            var promise;
            if (vm.isEditMode) {
                promise = countryService.editCountry(vm.country);
            } else {
                promise = countryService.createCountry(vm.country);
            }

            promise
                .then(function (response) {
                    vm.showProgress = false;
                    result = response.data;
                    if (result.statusCode == 200) {
                        toastr.success(translateService.get('Save change success'));
                        $state.go('countries', { id: result.result });
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
                        for (var key in error) {
                            vm.validationErrors.push(error[key][0]);
                        }
                    } else { 
                        vm.validationErrors.push(translateService.get('Could not add country.'));
                    }
                });
        };

        function init() {
            if (vm.isEditMode) {
                countryService.getCountry(vm.countryId).then(function (result) {
                    vm.country = result.data;
                });
            }
        }

        init();
    }
})();
