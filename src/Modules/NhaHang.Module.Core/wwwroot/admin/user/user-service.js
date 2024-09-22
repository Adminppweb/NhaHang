/*global angular*/
(function () {
    angular
        .module('simplAdmin.core')
        .factory('userService', ['$http', userService]);

    function userService($http) {
        var service = {
            getUsers: getUsers,
            getUser: getUser,
            quickSearchUsers: quickSearchUsers,
            createUser: createUser,
            editUser: editUser,
            deleteUser: deleteUser,
            getRoles: getRoles,
            getVendors: getVendors,
            getCustomerGroups: getCustomerGroups
        };
        return service;

        function getUsers(params) {
            return $http.post('api/users/grid', params);
        }

        function getUser(id) {
            return $http.get('api/users/' + id);
        }

        function quickSearchUsers(name) {
            return $http.get('api/users/quick-search/?name=' + name);
        }

        function createUser(user) {
            return $http.post('api/users', user);
        }

        function editUser(user) {
            return $http.post('api/users/update-user/' + user.id, user);
        }

        function deleteUser(user) {
            return $http.post('api/users/delete-user/' + user.id, null);
        }

        function getRoles() {
            return $http.get('api/roles');
        }

        function getVendors() {
            return $http.get('api/vendors');
        }

        function getCustomerGroups() {
            return $http.get('api/customergroups');
        }
    }
})();
