'use strict';

tripExchange.controller('TripDetailsCtrl', ['$scope', '$routeParams', '$location', 'identity', 'notifier', 'TripsResource',
    function TripDetailsCtrl($scope, $routeParams, $location, identity, notifier, TripsResource) {
        TripsResource.byId($routeParams.id)
            .$promise
            .then(function(trip) {
                $scope.trip = trip;
                $scope.passengers = trip.passengers.join(', ');

                $scope.disableJoinButton = function(trip) {
                    if (trip.numberOfFreeSeats < 1) {
                        return true;
                    }

                    if (trip.departureDate < (new Date()).toISOString()) {
                        return true;
                    }

                    for (var i = 0; i <= trip.passengers.length; i++) {
                        if (trip.passengers[i] == identity.getCurrentUser().userName) {
                            return true;
                        }
                    }

                    return false;
                };
            });

        $scope.joinTrip = function(id) {
            TripsResource.join(id)
                .then(function() {
                    notifier.success('Successfully joined the trip');
                    $location.path('/trips');
                });
        };
    }]);