/*global angular confirm*/
(function () {
    angular
        .module('simplAdmin.customers')
        .controller('EvaluateListCtrl', ['evaluateService', 'translateService', '$window', EvaluateListCtrl]);

    function EvaluateListCtrl(evaluateService, translateService, $window) {
        var vm = this;
        vm.tableStateRef = {};
        vm.translate = translateService;
        vm.evaluates = [];
        vm.enableCultures = $window.Global_EnableCultures;


        //get data list smart table
        vm.getEvaluates = function getEvaluates(tableState) {

            vm.tableStateRef = tableState;
            vm.isLoading = true;
            evaluateService.getEvaluates(tableState).then(function (result) {
                vm.evaluates = result.data.items;
                tableState.pagination.numberOfPages = result.data.numberOfPages;
                tableState.pagination.totalItemCount = result.data.totalRecord;
                vm.isLoading = false;
            });
        };

        vm.changeStatus = function changeStatus(evaluate) {
            evaluateService.changeStatus(evaluate).then(function () {
                evaluate.isPublished = !evaluate.isPublished;
            });
        };

        vm.deleteEvaluate = function deleteEvaluate(evaluate) {
            bootbox.confirm(translateService.get('Are you sure you want to delete this evaluate: ') + simplUtil.escapeHtml(evaluate.name), function (result) {
                if (result) {
                    evaluateService.deleteEvaluate(evaluate)
                        .then(function (result) {
                            vm.getEvaluates(vm.tableStateRef);
                            toastr.success(evaluate.name + translateService.get(' has been deleted'));
                        })
                        .catch(function (response) {
                            toastr.error(response.data.error);
                        });
                }
            });
        };
    }
})();
