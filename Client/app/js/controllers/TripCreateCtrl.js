'use strict';

tripExchange.controller('TripCreateCtrl', ['$scope', '$location', 'auth', 'notifier', 'TripsResource', 'CitiesResource',
    function TripCreateCtrl($scope, $location, auth, notifier, TripsResource, CitiesResource) {
        auth.userInfo()
            .then(function(userInfo) {
                $scope.userInfo = userInfo;
                if (userInfo.isDriver) {
                    $scope.cities = CitiesResource.all();

                    $scope.createTrip = function(trip) {
                        TripsResource.create(trip)
                            .then(function() {
                                notifier.success('Trip created successfully!!');
                                $location.path('/trips');
                            });
                    }
                }
            })
    }]);