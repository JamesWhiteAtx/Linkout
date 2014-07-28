/*
RoadwireCostco Costco App
(c) 2014 Roadwire, Inc.
*/

var costco = angular.module('costco', ['ngRoute', 'ngResource', 'routeStyles', 'ngAnimate', 'ui.bootstrap',
    'roadwire.directives', 'roadwire.services', 'costco.services', 'costco.directives'])

    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/leather', {
            title: 'leather',
            templateUrl: '/Partial/Costco/Leather',
            controller: "LeaCtrl",
            resolve: { Data: ['DataDeferred', function (DataDeferred) { return DataDeferred(); }] }
        });
        $routeProvider.when('/heaters', {
            title: 'heaters',
            templateUrl: '/Partial/Costco/Heaters',
            controller: "HtrCtrl",
            resolve: { Data: ['DataDeferred', function (DataDeferred) { return DataDeferred(); }] }
        });
        $routeProvider.when('/install', {
            title: 'install',
            templateUrl: '/Partial/Costco/Install',
            controller: "InstCtrl",
            resolve: { Data: ['DataDeferred', function (DataDeferred) { return DataDeferred(); }] }
        });
        $routeProvider.when('/confirm', {
            title: 'confirm',
            templateUrl: '/Partial/Costco/Confirm',
            controller: "ConfirmCtrl",
            resolve: { Data: ['DataDeferred', function (DataDeferred) { return DataDeferred(); }] }
        });
        $routeProvider.when('/map', {
            title: 'map',
            templateUrl: '/Partial/Costco/Map',
            controller: 'MapCtrl',
            css: '/css/cc/roadwire/map.css',
            resolve: {
                gglMaps: ['$q', 'LoadGglMaps', function ($q, LoadGglMaps) {
                    return LoadGglMaps();
                }]
            }
        });
        $routeProvider.otherwise({ redirectTo: '/leather' });
    }])
    .run(['$rootScope', '$location', 'WidgetData', function ($rootScope, $location, WidgetData) {

        $rootScope.$on("$routeChangeStart", function (event, next, current) {
            if ((next.$$route) && (next.$$route.originalPath == '/heaters')) {
                var data = WidgetData();
                if (!data.order.hasLea()) {
                    $location.path("/leather");
                };
            };
        });

        $rootScope.$on('$routeChangeSuccess', function (event, current, previous) {
            if (current.$$route) {
                document.title = current.$$route.title;
            }
        });

    }])

;

//costco;


