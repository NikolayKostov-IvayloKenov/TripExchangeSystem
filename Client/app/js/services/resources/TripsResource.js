'use strict';

tripExchange.factory('TripsResource', ['$resource', 'authorization', 'baseServiceUrl', function($resource, authorization, baseServiceUrl) {
    var headers = authorization.getAuthorizationHeader();
    var TripsResource = $resource(baseServiceUrl + '/api/trips/:id', null, {
            'create': { method: 'POST', params: { id: '@id' }, isArray: false, headers: headers },
            'public': {  method: 'GET', isArray: true },
            'request': {  method: 'GET', isArray: true, headers: headers },
            'byId': { method: 'GET', params: { id: '@id' }, isArray: false, headers: headers },
            'join': { method:'PUT', params: { id: '@id' }, isArray: false, headers: headers }
        });

    return {
        create: function(trip) {
            return TripsResource.create(trip).$promise;
        },
        public: function() {
            return TripsResource.public();
        },
        all: function(request) {
            return TripsResource.request(request);
        },
        byId: function(id) {
            return TripsResource.byId({id: id});
        },
        join: function(id) {
            return TripsResource.join({id: id}).$promise;
        }
    }
}]);