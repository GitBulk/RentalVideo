(function (app) {
    'use strict';
    app.controller('loginController', loginController);
    loginController.$inject = ['$scope', 'membershipService', 'notificationService', '$rootScope', '$location']

    function loginController($scope, membershipService, notificationService, $rootScope, $location) {
        $scope.pageClass = 'page-login';
        $scope.user = {
            username: '',
            password: ''
        };
        $scope.login = function () {
            membershipService.login($scope.user, loginCompleted);
        }

        function loginCompleted(result) {
            if (result.data.success) {
                membershipService.saveCredentials($scope.user);
                notificationService.displaySuccess('Hello ' + $scope.user.username);
                $scope.userData.displayUserInfo();
                if ($rootScope.previousState) {
                    $location.path($rootScope.previousState);
                } else {
                    $location.path('/');
                }
            } else {
                notificationService.displayError('Login failed');
            }
        }
    }
})(angular.module('common.core'));