'use strict';
// Declare app level module which depends on services and directives and filters

var util = angular.module('util', []) 
    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/menu', {
            title: 'menu',
            templateUrl: '/Util/Partials/Menu',
            controller: "MenuCtrl"
        });

        $routeProvider.when('/itemCars', {
            title: 'ItemCars',
            templateUrl: '/Util/Partials/ItemCars',
            controller: "ItemCarsCtrl"
        });

        $routeProvider.otherwise({ redirectTo: '/menu' });
    } ])
;
util.run(['$location', '$rootScope', function ($location, $rootScope) {
    $rootScope.$on('$routeChangeSuccess', function (event, current, previous) {
        if (current.$$route) {
            $rootScope.title = current.$$route.title;
        }
    });

} ])
;


