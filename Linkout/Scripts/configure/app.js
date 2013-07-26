'use strict';
// Declare app level module which depends on services and directives and filters

var configure = angular.module('configure', ['ui.bootstrap', 'linkout.services', 'configure.services']) //'configure.directives', 'configure.services', 'configure.filters', 'ui.bootstrap'
    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/menu', {
            title: 'menu',
            templateUrl: '/Configure/Partials/Menu',
            controller: "MenuCtrl"
        });
        $routeProvider.when('/products', {
            title: 'products',
            templateUrl: '/configure/Partials/Products',
            controller: "ProductsCtrl"
        });
        $routeProvider.when('/cars', {
            title: 'cars',
            templateUrl: '/configure/Partials/Cars',
            controller: "CarsCtrl"
        });
        $routeProvider.when('/carPatterns', {
            title: 'carPatterns',
            templateUrl: '/configure/Partials/CarPatterns',
            controller: "CarPatternsCtrl"
        });
        $routeProvider.otherwise({ redirectTo: '/menu' });
    } ])
;
configure.run(['$location', '$rootScope', function ($location, $rootScope) {

    //        $rootScope.$on("$routeChangeStart", function (event, next, current) {
    //            if ((next.templateUrl == '/configure/Partials/Schedule') || (next.templateUrl == '/configure/Partials/Confirm')) {
    //                if (current) {
    //                    if (current.scope) {
    //                        if (current.scope.$parent) {
    //                            if (current.scope.$parent.pickedProduct) {
    //                                var x = current.scope.$parent.pickedProduct();
    //                                if (!x) {
    //                                    $location.path("/type");
    //                                }
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        });

    $rootScope.$on('$routeChangeSuccess', function (event, current, previous) {
        if (current.$$route) {
            $rootScope.title = current.$$route.title;
        }
    });

} ])
;


