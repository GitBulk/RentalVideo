(function (app) {
    'use strict';
    app.factory('apiService', apiService);
    apiService.$inject = ['$http', '$location', 'notificationService', '$rootScope']

    function apiService($http, $location, notificationService, $rootScope) {

        //  success callback to invoke and an optional failure one in case of a failed request
        function get(url, config, success, failure) {
            return $http.get(url, config)
                        .then(function (result) {
                            success(result);
                        }, function (error) {
                            if (error.status == '401') {
                                notificationService.displayError('Authentication required.');
                                $rootScope.previousState = $location.path();
                                $location.path('/login');
                            } else if (failure != null) {
                                failure(error);
                            }
                        });
        }

        //  success callback to invoke and an optional failure one in case of a failed request
        function post(url, data, success, failure) {
            return $http.post(url, data)
                        .then(function (result) {
                            success(result);
                        }, function (error) {
                            if (error.status == '401') {
                                notificationService.displayError('Authentication required.');
                                $rootScope.previousState = $location.path();
                                $location.path('/login');
                            } else if (failure != null) {
                                failure(error);
                            }
                        });
        }

        var service = {
            get: get,
            post: post
        };

        return service;
    }
})(angular.module('common.core'));