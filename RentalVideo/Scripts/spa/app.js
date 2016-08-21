(function () {
    'use strict';
    angular.module('rentalVideo', ['common.core', 'common.ui'])
        .config(config)
        .run(run);

    run.$inject = ['$rootScope', '$location', '$cookieStore', '$http'];
    function run($rootScope, $location, $cookieStore, $http) {
        $rootScope.repository = $cookieStore.get('repository') || {};
        if ($rootScope.repository.loggedUser) {
            $http.defaults.headers.common['Authorization'] = $rootScope.repository.loggedUser.authdata;
        }

        $(document).ready(function () {
            $('.fancybox').fancybox({
                openEffect: 'none',
                closeEffect: 'none'
            });

            $('.fancybox-media').fancybox({
                openEffect: 'none',
                closeEffect: 'none',
                helpers: {
                    media: {}
                }
            });

            $('[data-toggle=offcanvas]').click(function () {
                $('.row-offcanvas').toggleClass('active');
            });
        });
    }

    config.$inject = ['$routeProvider', '$locationProvider'];
    function config($routeProvider, $locationProvider) {
        $routeProvider
        .when('/', {
            templateUrl: 'Scripts/spa/home/index.html',
            controller: 'indexController'
        })
        .when('/login', {
            templateUrl: 'Scripts/spa/account/login.html',
            controller: 'loginController'
        })
        .when('/register', {
            templateUrl: 'Scripts/spa/account/register.html',
            controller: 'registerController'
        })
        .when('/customers', {
            templateUrl: 'Scripts/spa/customers/customers.html',
            controller: 'customersController'
        })
        .when('/customers/register', {
            templateUrl: 'Scripts/spa/customers/register.html',
            controller: 'customerRegisterController'
        })
        .when('/movies', {
            templateUrl: 'Scripts/spa/movies/movies.html',
            controller: 'moviesController'
        })
        .when('/movies/add', {
            templateUrl: 'Scripts/spa/movies/add.html',
            controller: 'movieAddController'
        })
        .when('/movies/:id', {
            templateUrl: 'Script/spa/movies/details.html',
            controller: 'movieDetailsController'
        })
        .when('/movie/edit/:id', {
            templateUrl: 'Scripts/spa/movies/edit.html',
            controller: 'movieEditController'
        })
        .when('/rental', {
            templateUrl: 'Scripts/spa/rental/rental.html',
            controller: 'rentalController'
        })
        .otherwise({
            redirectTo: '/'
        });
        //$locationProvider.html5Mode({
        //    enabled: true,
        //    requireBase: false
        //});
    }
})();