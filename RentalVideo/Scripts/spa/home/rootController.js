(function (app) {
    'use strict';
    app.controller('rootController', rootController);
    rootController.$inject = ['$scope'];
    function rootController($scope) {
        $scope.userData = {};
        $scope.userData.displayUserInfo = function () {

        }

        $scope.logout = function () {

        }
    }
})(angular.module('rentalVideo'));