'use strict';

tripExchange.directive('drivers', [function() {
    return {
        restrict: 'A',
        templateUrl: 'views/directives/drivers.html',
        scope: true,
        replace: true
    }
}]);