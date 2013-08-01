'use strict';
// Declare app level module which depends on services and directives and filters

var configure = angular.module('demo', ['ui.bootstrap', 'linkout.services', 'configure.services']) //'configure.directives', 'configure.services', 'configure.filters', 'ui.bootstrap'
;
configure.run(['$location', '$rootScope', function ($location, $rootScope) {
} ])
;


