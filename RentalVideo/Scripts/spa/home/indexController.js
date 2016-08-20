(function (app) {
    'use strict';
    app.controller('indexController', indexController);
    indexController.$inject = ['$scope', 'apiService', 'notificationService'];
    function indexController($scope, apiService, notificationService) {
        $scope.pageClass = 'page-home';
        $scope.loadingMovies = true;
        $scope.loadingGenres = true;
        $scope.isReadOnly = true;
        $scope.latestMovies = [];
        $scope.loadData = loadData;

        function loadData() {
            var latestMoviesUrl = '/api/movies/latest';
            apiService.get(latestMoviesUrl, null, moviesLoadCompleted, moviesLoadFailed);
            var genresUrl = '/api/genres';
            apiService.get(genresUrl, null, genresLoadCompleted, genresLoadFailed);
        }

        function moviesLoadCompleted(result) {
            $scope.latestMovies = result.data;
            $scope.loadingMovies = false;
        }

        function moviesLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function genresLoadCompleted(result) {
            var genres = result.data;
            Morris.Bar({
                element: 'genres-bar',
                data: genres,
                xkey: 'Name',
                ykeys: ['NumberOfMovies'],
                labels: ['Number Of Movies'],
                barRatio: 0.4,
                xLabelAngle: 55,
                hideHover: 'auto',
                resize: 'true'
            });

            $scope.loadingGenres = false;
        }

        function genresLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        // run method
        loadData();
    }
})(angular.module('rentalVideo'));