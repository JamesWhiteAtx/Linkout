'use strict';
// Declare app level module which depends on services and directives and filters

var costco = angular.module('costco', ['ui.bootstrap', 'costco.services']) 
    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/menu', {
            title: 'menu',
            templateUrl: '/CostcoAdmin/Partials/Menu',
            controller: "CcMenuCtrl"
        });

        $routeProvider.when('/downloadorders', {
            title: 'ftporders',
            templateUrl: '/CostcoAdmin/Partials/FtpOrders',
            controller: "FtpOrdersCtrl"
        });
        $routeProvider.when('/downloadorder/:name', {
            title: 'ftporder',
            templateUrl: '/CostcoAdmin/Partials/DownloadedOrder',
            controller: "FtpOrderCtrl"
        });
        $routeProvider.when('/vieworders', {
            title: 'vieworders',
            templateUrl: '/CostcoAdmin/Partials/Orders',
            controller: "ViewOrdersCtrl"
        });
        $routeProvider.when('/vieworder/:name', {
            title: 'vieworder',
            templateUrl: '/CostcoAdmin/Partials/OrderBatch',
            controller: "ViewOrderCtrl"
        });
        $routeProvider.when('/localconfirms', {
            title: 'readconfirms',
            templateUrl: '/CostcoAdmin/Partials/Confirms',
            controller: "LocalConfirmsCtrl"
        });
        $routeProvider.when('/newconfirm', {
            title: 'newconfrim',
            templateUrl: '/CostcoAdmin/Partials/ConfirmBatch',
            controller: "EditConfirmCtrl"
        });
        $routeProvider.when('/editconfirm/:name', {
            title: 'editconfrim',
            templateUrl: '/CostcoAdmin/Partials/ConfirmBatch',
            controller: "EditConfirmCtrl"
        });
        $routeProvider.when('/localacks', {
            title: 'readacks',
            templateUrl: '/CostcoAdmin/Partials/Acks',
            controller: "LocalAcksCtrl"
        });
        $routeProvider.when('/editack/:name', {
            title: 'editack',
            templateUrl: '/CostcoAdmin/Partials/AckBatch',
            controller: "EditAckCtrl"
        });
        $routeProvider.when('/newack', {
            title: 'newack',
            templateUrl: '/CostcoAdmin/Partials/AckBatch',
            controller: "EditAckCtrl"
        });
        $routeProvider.when('/orderack/:cached', {
            title: 'orderack',
            templateUrl: '/CostcoAdmin/Partials/AckBatch',
            controller: "EditAckCtrl"
        });

        $routeProvider.otherwise({ redirectTo: '/menu' });
    } ])
;
costco.run(['$location', '$rootScope', function ($location, $rootScope) {
    $rootScope.$on('$routeChangeSuccess', function (event, current, previous) {
        if (current.$$route) {
            $rootScope.title = current.$$route.title;
        }
    });

} ])
;


