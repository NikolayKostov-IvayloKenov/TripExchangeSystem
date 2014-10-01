'use strict';

tripExchange.controller('HomeCtrl', ['$scope', 'StatsResource', 'TripsResource', 'DriversResource',
    function HomeCtrl($scope, StatsResource, TripsResource, DriversResource) {
        $scope.stats = StatsResource.get();
        $scope.trips = TripsResource.public();
        $scope.drivers = DriversResource.public();
        $scope.hideIsMine = true;
    }]);