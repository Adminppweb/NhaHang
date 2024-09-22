/*global angular*/
(function () {
    angular
        .module('simplAdmin.customers')
        .factory('simpleEvaluateWidgetService', ['$http', simpleEvaluateWidgetService]);

    /* @ngInject */
    function simpleEvaluateWidgetService($http) {
        var service = {
            getWidgetZones: getWidgetZones,
            getSimpleEvaluateWidget: getSimpleEvaluateWidget,
            createSimpleEvaluateWidget: createSimpleEvaluateWidget,
            editSimpleEvaluateWidget: editSimpleEvaluateWidget
        };

        return service;

        function getWidgetZones() {
            return $http.get('api/widget-zones');
        }

        function getSimpleEvaluateWidget(id) {
            return $http.get('api/simple-evaluate-widgets/' + id);
        }
        
        function createSimpleEvaluateWidget(widgetInstance) {
            return $http.post('api/simple-evaluate-widgets', widgetInstance);
        }

        function editSimpleEvaluateWidget(widgetInstance) {
            return $http.post('api/simple-evaluate-widgets/update-simple-evaluate-widget/' + widgetInstance.id, widgetInstance);
        }
    }
})();
