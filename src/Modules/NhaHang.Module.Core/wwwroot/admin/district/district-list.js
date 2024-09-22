/*global angular*/
(function () {
    angular
        .module('simplAdmin.core')
        .controller('DistrictListCtrl', ['stateProvinceService', 'stateProvinceService', 'translateService', '$stateParams', DistrictListCtrl]);

    function DistrictListCtrl(stateProvinceService, stateProvinceService, translateService, $stateParams) {
        var vm = this;
        vm.tableStateRef = {};

        vm.stateProvinces = [];
        vm.districts = [];
        vm.stateProvinceId = $stateParams.stateProvinceId;
        vm.translate = translateService;

        vm.onStateOrProvinceSelected = function () {
            vm.getDistricts(vm.tableStateRef);
        };

        vm.getDistricts = function (tableState) {
            vm.tableStateRef = tableState;
            vm.isLoading = true;
            stateProvinceService.getDistricts(vm.stateProvinceId, tableState).then(function (result) {
                vm.districts = result.data.items;
                tableState.pagination.numberOfPages = result.data.numberOfPages;
                tableState.pagination.totalItemCount = result.data.totalRecord;
                vm.isLoading = false;
            });
        };

        vm.deleteDistrict = function deleteDistrict(district) {
            bootbox.confirm(translateService.get('Are you sure you want to delete this state or province: ') + simplUtil.escapeHtml(district.name), function (result) {
                if (result) {
                    stateProvinceService.deleteDistrict(district)
                        .then(function (result) {
                            vm.getDistricts(vm.tableStateRef);
                            toastr.success(district.name + translateService.get(' has been deleted'));
                        })
                        .catch(function (response) {
                            toastr.error(response.data.error);
                        });
                }
            });
        };

        function init() {
            stateProvinceService.getAllStateOrProvinces().then(function (result) {
                vm.stateProvinces = result.data;
                vm.stateProvinceId = vm.stateProvinceId || vm.stateProvinces[0].id.toString();
            });
        }

        init();
    }
})();
