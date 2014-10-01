'use strict';

tripExchange.controller('DriversCtrl', ['$scope', 'identity', 'DriversResource',
    function DriversCtrl($scope, identity, DriversResource) {
        $scope.identity = identity;
        if (identity.isAuthenticated()) {
            $scope.request = {
                page: 1
            };

            $scope.drivers = DriversResource.all($scope.request);

            $scope.filter = function(request) {
                DriversResource.all(request)
                    .$promise
                    .then(function(drivers) {
                        $scope.drivers = drivers;
                    });
            }
        }
        else {
            $scope.drivers = DriversResource.public();
        }
    }]);