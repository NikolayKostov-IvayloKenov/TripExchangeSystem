'use strict';

tripExchange.filter('onlyDriver', [function() {
    return function(trips, driverId) {
        if (!driverId) {
            return trips;
        }

        trips = trips || [];
        var result = [];
        for(var i = 0; i < trips.length; i++) {
            if (trips[i].driverId == driverId) {
                result.push(trips[i]);
            }
        }

        return result;
    }
}]);