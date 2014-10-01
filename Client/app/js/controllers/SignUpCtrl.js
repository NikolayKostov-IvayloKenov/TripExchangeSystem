'use strict';

tripExchange.controller('SignUpCtrl', ['$scope', '$location', 'auth', 'notifier',
    function SignUpCtrl($scope, $location, auth, notifier) {
        $scope.emailPattern = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

        $scope.signup = function(user) {
            auth.signup(user).then(function() {
                notifier.success('Registration successful!');
                $location.path('/');
            })
        }
    }]);