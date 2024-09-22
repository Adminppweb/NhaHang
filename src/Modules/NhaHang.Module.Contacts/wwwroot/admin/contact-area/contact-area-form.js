 /*global angular*/
(function () {
    angular
        .module('simplAdmin.contacts')
        .controller('ContactAreaFormCtrl', ['$state', '$stateParams', 'contactAreaService', 'translateService', ContactAreaFormCtrl]);

    function ContactAreaFormCtrl($state, $stateParams, contactAreaService, translateService) {
        var vm = this;
        vm.translate = translateService;
        vm.contactArea = {};
        vm.contactAreaId = $stateParams.id;
        vm.isEditMode = vm.contactAreaId > 0;
        vm.showProgress = false;

        vm.save = function save() {
            vm.showProgress = true; // Hiển thị tiến trình

            // Đặt thời gian chạy tiến trình
            setTimeout(function () {
            var promise;
            if (vm.isEditMode) {
                promise = contactAreaService.editContactArea(vm.contactArea);
            } else {
                promise = contactAreaService.createContactArea(vm.contactArea);
            }

            promise
                .then(function (response) {
                    vm.showProgress = false; // Ẩn tiến trình khi lưu hoàn tất
                    result = response.data;
                    if (result.statusCode == 200) {
                        toastr.success('Save change success');
                        $state.go('contact-area', { id: result.result });
                    }
                    else {
                        toastr.error('Excuse me. An error occurred during execution');
                    }
                })
                .catch(function (response) {
                    vm.showProgress = false; // Ẩn tiến trình nếu có lỗi
                    var error = response.data;
                    vm.validationErrors = [];
                    if (error && angular.isObject(error)) {
                        for (var key in error) {
                            vm.validationErrors.push(error[key][0]);
                        }
                    } else {
                        vm.validationErrors.push(translateService.get('Could not add contact category.'));
                    }
                });
            }, 150); // Thời gian chạy tiến trình (2 giây)
        };

        function init() {
            if (vm.isEditMode) {
                contactAreaService.getContactArea(vm.contactAreaId).then(function (result) {
                    vm.contactArea = result.data;
                });
            }
        }

        init();
    }
})();
