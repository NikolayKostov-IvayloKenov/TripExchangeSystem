'use strict';

tripExchange.directive('statistics', [function() {
    return {
        restrict: 'A',
        templateUrl: 'views/directives/statistics.html',
        scope: {
            stats: '='
        },
        replace: true
    }
}]);