'use strict';


// Declare app level module which depends on services and directives
var linkout = angular.module('linkout', ['ui.bootstrap', 'linkout.services', 'linkout.directives']) //'linkout.filters'
    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/type', {
            title: 'type',
            templateUrl: '/Linkout/Partials/Type',
            controller: "TypeCtrl"
        });
        $routeProvider.when('/leather', {
            title: 'leather',
            templateUrl: '/Linkout/Partials/Leather',
            controller: "LeatherCtrl"
        });
        $routeProvider.when('/heaters', {
            title: 'heaters',
            templateUrl: '/Linkout/Partials/Heaters',
            controller: "HeaterCtrl"
        });
        $routeProvider.when('/schedule', {
            title: 'schedule',
            templateUrl: '/Linkout/Partials/Schedule',
            controller: "ScheduleCtrl"
        });
        $routeProvider.when('/confirm', {
            title: 'confirm',
            templateUrl: '/Linkout/Partials/Confirm',
            controller: "ConfirmCtrl"
        });
        $routeProvider.when('/about', {
            title: 'faq',
            templateUrl: '/Linkout/Partials/About',
            controller: "EmptyCtrl"
        });
        $routeProvider.when('/faq', {
            title: 'faq',
            templateUrl: '/Linkout/Partials/Faq',
            controller: "EmptyCtrl"
        });
        $routeProvider.when('/videos', {
            title: 'videos',
            templateUrl: '/Linkout/Partials/Videos',
            controller: "EmptyCtrl"
        });

        $routeProvider.otherwise({ redirectTo: '/type' });
    } ])
;
    linkout.run(['$location', '$rootScope', function ($location, $rootScope) {

        //        $rootScope.$on("$routeChangeStart", function (event, next, current) {
        //            if ((next.templateUrl == '/Linkout/Partials/Schedule') || (next.templateUrl == '/Linkout/Partials/Confirm')) {
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


