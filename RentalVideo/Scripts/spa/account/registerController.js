(function (app) {
    'use strict';
    app.controller('registerController', registerController);

    registerController.$inject = ['$scope', 'membershipService', 'notificationService', '$rootScope', '$location']
    function registerController($scope, membershipService, notificationService, $rootScope, $location) {
        $scope.pageClass = 'page-login';
        $scope.user = {};

        function registerCompleted(result) {
            if (result.data.success) {
                membershipService.saveCredentials($scope.user);
                notificationService.displaySuccess('Hello ' + $scope.user.username);
                $scope.userData.displayUserInfo();
                $location.path('/');
            } else {
                notificationService.displayError('Registration failed. Try again');
            }
        }

        $scope.register = function () {
            membershipService.register($scope.user, registerCompleted);
        }

    }
})(angular.module('common.core'));