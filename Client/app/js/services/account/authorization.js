'use strict';

tripExchange.factory('authorization', ['identity', function(identity) {
    var headers = {};

    return {
        getAuthorizationHeader: function() {
            var currentUser = identity.getCurrentUser();
            if (currentUser) {
                this.setAuthorizationHeader(currentUser['access_token']);
            }

            return headers;
        },
        setAuthorizationHeader: function(auth) {
            headers['Authorization'] = 'Bearer ' + auth;
        },
        removeAuthorizationHeader: function() {
            delete headers['Authorization'];
        }
    }
}]);