'use strict';

var tripExchange = angular.module('tripExchange', ['ngRoute', 'ngResource', 'ngCookies']).
    config(['$routeProvider', '$httpProvider', function($routeProvider, $httpProvider) {
        $httpProvider.interceptors.push('errorHandlerHttpInterceptor');

        var routeUserChecks = {
            authenticated: {
                authenticate: function(auth) {
                    return auth.isAuthenticated();
                }
            }
        };

        $routeProvider
            .when('/', {
                templateUrl: 'views/partials/home.html',
                controller: 'HomeCtrl'
            })
            .when('/unauthorized', {
                templateUrl: 'views/partials/unauthorized.html',
                controller: 'HomeCtrl'
            })
            .when('/register', {
                templateUrl: 'views/partials/register.html',
                controller: 'SignUpCtrl'
            })
            .when('/drivers', {
                templateUrl: 'views/partials/drivers.html',
                controller: 'DriversCtrl'
            })
            .when('/drivers/:id', {
                templateUrl: 'views/partials/driver-details.html',
                controller: 'DriverDetailsCtrl',
                resolve: routeUserChecks.authenticated
            })
            .when('/trips', {
                templateUrl: 'views/partials/trips.html',
                controller: 'TripsCtrl'
            })
            .when('/trips/create', {
                templateUrl: 'views/partials/trip-create.html',
                controller: 'TripCreateCtrl',
                resolve: routeUserChecks.authenticated
            })
            .when('/trips/:id', {
                templateUrl: 'views/partials/trip-details.html',
                controller: 'TripDetailsCtrl',
                resolve: routeUserChecks.authenticated
            })
            .otherwise({ redirectTo: '/' });
    }])
    .run(function($rootScope, $location) {
        $rootScope.$on('$routeChangeError', function(ev, current, previous, rejection) {
            if (rejection === 'not authorized') {
                $location.path('/unauthorized');
            }
        })
    })
    .value('toastr', toastr)
    .constant('baseServiceUrl', 'http://spa2014.bgcoder.com');