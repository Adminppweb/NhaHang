/*global angular*/
(function () {
    angular
        .module('simplAdmin.contacts')
        .factory('contactAreaService', ['$http', contactAreaService]);

    function contactAreaService($http) {
        var service = {
            getContactArea: getContactArea,
            createContactArea: createContactArea,
            editContactArea: editContactArea,
            deleteContactArea: deleteContactArea,
            getContactAreas: getContactAreas,
            getContactAreaTranslation: getContactAreaTranslation,
            editContactAreaTranslation: editContactAreaTranslation
        };
        return service;

        function getContactArea(id) {
            return $http.get('api/contact-area/' + id);
        }

        function getContactAreas() {
            return $http.get('api/contact-area');
        }

        function createContactArea(contactArea) {
            return $http.post('api/contact-area', contactArea);
        }

        function editContactArea(contactArea) {
            return $http.post('api/contact-area/update-contact-area/' + contactArea.id, contactArea);
        }

        function deleteContactArea(contactArea) {
            return $http.post('api/contact-area/delete-contact-area/' + contactArea.id, null);
        }

        function getContactAreaTranslation(id, culture) {
            return $http.get('api/contact-area-translations/' + id + '?culture=' + culture);
        }

        function editContactAreaTranslation(id, culture, model) {
            return $http.post('api/contact-area-translations/update-contact-area-translation/' + id + '?culture=' + culture, model);
        }
    }
})();
