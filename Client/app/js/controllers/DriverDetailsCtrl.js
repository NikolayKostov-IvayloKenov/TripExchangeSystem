'use strict';

tripExchange.controller('DriverDetailsCtrl', ['$scope', '$routeParams', 'DriversResource',
    function DriverDetailsCtrl($scope, $routeParams, DriversResource) {
        DriversResource.byId($routeParams.id)
            .$promise
            .then(function(driver) {
                $scope.driver = driver;
                $scope.trips = driver.trips;

                $scope.onOnlyDriverCheckboxChange = function() {
                    if ($scope.onlyDriver) {
                        $scope.currentDriver = driver.id;
                    }
                    else {
                        $scope.currentDriver = undefined;
                    }
                }
            })
    }]);