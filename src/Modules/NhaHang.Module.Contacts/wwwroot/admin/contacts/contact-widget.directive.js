(function () {
    angular
        .module('simplAdmin.contacts')
        .directive('contactWidget', contactWidget);

    function contactWidget() {
        var directive = {
            restrict: 'E',
            templateUrl: '_content/NhaHang.Module.Contacts/admin/contacts/contact-widget.directive.html',
            scope: {
                status: '=',
                numRecords: '='
            },
            controller: ['contactService', 'translateService', ContactWidgetCtrl],
            controllerAs: 'vm',
            bindToController: true
        };

        return directive;
    }

    function ContactWidgetCtrl(contactService, translateService) {
        var vm = this;
        vm.translate = translateService;
        vm.contacts = [];
        vm.totalContacts = 0;

        vm.$onInit = function () {
            contactService.getContacts(vm.status, vm.numRecords).then(function (result) {
                vm.contacts = result.data;
                vm.totalContacts = vm.contacts.length;
                console.log('Tổng số khách hàng liên hệ: ' + vm.totalContacts);
            });
        };
    }
})();
