'use strict';

angular.module('linkout.services', ['ngResource'])
    .value('version', '0.1')

    .factory('Makes', ['$resource', function ($resource) {
        return $resource('/selector/makes');
    } ])
    .factory('Years', ['$resource',  function($resource) {
        return $resource('/selector/make/:makeid/years', { makeid: '@makeid' });
    }])
    .factory('Models', ['$resource', function ($resource) {
        return $resource('/selector/make/:makeid/year/:yearid/models', { makeid: '@makeid', yearid: '@yearid' });
    } ])
    .factory('Bodies', ['$resource', function ($resource) {
        return $resource('/selector/make/:makeid/year/:yearid/model/:modelid/bodies', { makeid: '@makeid', yearid: '@yearid', modelid: '@modelid' });
    } ])
    .factory('Trims', ['$resource', function ($resource) {
        return $resource('/selector/make/:makeid/year/:yearid/model/:modelid/body/:bodyid/trims', { makeid: '@makeid', yearid: '@yearid', modelid: '@modelid', bodyid: '@bodyid' });
    } ])
    .factory('Cars', ['$resource', function ($resource) {
        return $resource('/selector/make/:makeid/year/:yearid/model/:modelid/body/:bodyid/trim/:trimid/cars', { makeid: '@makeid', yearid: '@yearid', modelid: '@modelid', bodyid: '@bodyid', trimid: '@trimid' });
    } ])
    .factory('Ptrns', ['$resource', function ($resource) {
        return $resource('/selector/car/:carid/ptrns', { carid: '@carid' });
    } ])
    .factory('IntCols', ['$resource', function ($resource) {
        return $resource('/selector/car/:carid/ptrn/:ptrnid/intcols', { carid: '@carid', ptrnid: '@ptrnid' });
    } ])
    .factory('RecCols', ['$resource', function ($resource) {
        return $resource('/selector/car/:carid/ptrn/:ptrnid/intcol/:intcolid/reccols', { carid: '@carid', ptrnid: '@ptrnid', intcolid: '@intcolid' });
    } ])
    .factory('AllCols', ['$resource', function ($resource) {
        return $resource('/selector/ptrn/:ptrnid/allcols', { ptrnid: '@ptrnid' });
    } ])

    .factory('Installers', ['$resource', function ($resource) {
        return $resource('/schedule/installers/:zipcode', { zipcode: '@zipcode' });
    } ])

    .factory('ProductPrice', ['$http', function($http) {
        return {
            leather: function(rows) {
                return $http.get('/price/leather/' + rows, { cache: true });
            },
            heater: function () {
                return $http.get('/price/heater');
            }
        };
    } ]);

;
