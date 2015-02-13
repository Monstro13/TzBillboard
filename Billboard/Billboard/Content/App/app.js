var app = angular.module('Billboard', ['ngRoute', 'ui.bootstrap']);

app.config(['$routeProvider', '$locationProvider',
    function ($routeProvider, $locationProvider) {
        $locationProvider.html5Mode(true);

        $routeProvider
            .when('/welcome', {
                controller: 'WelcomeCtrl',
                templateUrl: '/Partials/Welcome'
            })
            .when('/login', {
                controller: 'LoginCtrl',
                templateUrl: '/Partials/Welcome'
            })
            .when('/registration', {
                controller: 'RegisterCtrl',
                templateUrl: '/Partials/Welcome'
            })
            .when('/advt', {
                controller: 'AdCtrl',
                templateUrl: '/Partials/Ad'
            })
            .when('/add', {
                controller: 'AddCtrl',
                templateUrl: '/Partials/Empty'
            })
            .when('/friends', {
                controller: 'FriendsCtrl',
                templateUrl: '/Partials/Friends'
            })
            .when('/friends/:friend', {
                controller: 'FriendCtrl',
                templateUrl: '/Partials/Ad'
            })
            .otherwise({ redirectTo: '/welcome' });
    }]);