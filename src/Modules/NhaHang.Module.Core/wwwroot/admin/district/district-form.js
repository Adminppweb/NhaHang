 /*global angular*/
(function () {
    angular
        .module('simplAdmin.core')
        .controller('DistrictFormCtrl', ['$state', '$stateParams', 'districtService', 'translateService', DistrictFormCtrl]);

    function DistrictFormCtrl($state, $stateParams, districtService, translateService) {
        var vm = this;
        vm.translate = translateService;
        vm.district = {};
        vm.districtId = $stateParams.id;
        vm.countryId = $stateParams.countryId;
        vm.isEditMode = vm.districtId > 0;

        vm.save = function save() {
            var promise;
            if (vm.isEditMode) {
                promise = districtService.editDistrict(vm.district);
            } else {
                vm.district.countryId = vm.countryId;
                promise = districtService.createDistrict(vm.district);
            }

            promise
                .then(function (result) {
                    $state.go('country-states-provinces', {countryId: vm.countryId});
                })
                .catch(function (response) {
                    var error = response.data;
                    vm.validationErrors = [];
                    if (error && angular.isObject(error)) {
                        for (var key in error) {
                            vm.validationErrors.push(error[key][0]);
                        }
                    } else { 
                        vm.validationErrors.push(translateService.get('Could not add State or Province.'));
                    }
                });
        };

        function init() {
            if (vm.isEditMode) {
                districtService.getDistrict(vm.districtId).then(function (result) {
                    vm.district = result.data;
                });
            }
        }

        init();
    }
})();
