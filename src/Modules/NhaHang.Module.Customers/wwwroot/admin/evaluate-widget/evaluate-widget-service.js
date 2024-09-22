/*global angular*/
(function () {
    angular
        .module('simplAdmin.customers')
        .factory('evaluateWidgetService', ['$http', 'Upload', evaluateWidgetService]);

    /* @ngInject */
    function evaluateWidgetService($http, Upload) {
        var service = {
            TestFunction: TestFunction,
            getWidgetZones: getWidgetZones,
            getEvaluateWidget: getEvaluateWidget,
            createEvaluateWidget: createEvaluateWidget,
            editEvaluateWidget: editEvaluateWidget,
            getEvaluateWidgetAvailableOrderBy: getEvaluateWidgetAvailableOrderBy,
            getNumberOfWidgets: getNumberOfWidgets
        };
        return service;
        function TestFunction() {
            alert('test exist')
        }

        function getWidgetZones() {
            return $http.get('api/widget-zones');
        }

        function getEvaluateWidget(id) {
            return $http.get('api/evaluate-widgets/' + id);
        }

        function createEvaluateWidget(widgetInstance) {
            return $http.post('api/evaluate-widgets', widgetInstance);
        }

        function editEvaluateWidget(widgetInstance) {
            return $http.post('api/evaluate-widgets/update-evaluate-widget/' + widgetInstance.id, widgetInstance);
        }

        function getEvaluateWidgetAvailableOrderBy() {
            return $http.get('api/evaluate-widgets/available-orderby');
        }

        function getNumberOfWidgets() {
            return $http.get('api/widget-instances/number-of-widgets');
        }
    }
})();
