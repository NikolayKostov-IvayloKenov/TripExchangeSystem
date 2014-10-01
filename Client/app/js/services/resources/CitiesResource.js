'use strict';

tripExchange.factory('CitiesResource', ['$resource', 'baseServiceUrl', function($resource, baseServiceUrl) {
    var CitiesResource = $resource(baseServiceUrl + '/api/cities');

    return {
        all: function() {
            return CitiesResource.query();
        }
    }
}]);