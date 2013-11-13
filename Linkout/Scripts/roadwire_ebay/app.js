'use strict';
// Declare app level module which depends on services and directives and filters

var configure = angular.module('roadwireEbay', ['ui.bootstrap', 'roadwireEbay.services']) 
    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/menu', {
            title: 'menu',
            templateUrl: '/Ebay/Partials/Menu',
            controller: "MenuCtrl"
        });
        $routeProvider.when('/paidUnshipped', {
            title: 'unshipped',
            templateUrl: '/Ebay/Partials/PaidUnshipped',
            controller: "PaidUnshippedCtrl"
        });

        $routeProvider.otherwise({ redirectTo: '/menu' });
    } ])
;
configure.run(['$location', '$rootScope', function ($location, $rootScope) {
    $rootScope.$on('$routeChangeSuccess', function (event, current, previous) {
        if (current.$$route) {
            $rootScope.title = current.$$route.title;
        }
    });

} ])
;


