'use strict';

angular.module('linkout.services', ['ngResource'])
    .value('version', '0.1')

    .factory('Makes', ['$resource', function ($resource) {
        return $resource('/selector/makes');
    } ])
    .factory('Years', ['$resource', function ($resource) {
        return $resource('/selector/make/:makeid/years', { makeid: '@makeid' });
    } ])
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

    .factory('ProductPrice', ['$http', function ($http) {
        return {
            leather: function (rows) {
                return $http.get('/price/leather/' + rows, { cache: true });
            },
            heater: function () {
                return $http.get('/price/heater');
            }
        };
    } ])


    .factory('ProductList', ['$resource', '$cacheFactory', 'ProductDefn', function ($resource, $cacheFactory, ProductDefn) {
        var listingKey = 'listingKey';
        var cache = $cacheFactory('ProductList');
        var ListingSrcv = $resource('/product/listing');

        return {
            getListing: function () {
                var listing = cache.get(listingKey);
                if (!listing) {

                    var srvcList = ListingSrcv.query(
                        function (data, status, headers, config) {
                            var listing = ProductDefn.makeProdList()

                            if (angular.isArray(srvcList)) {
                                for (var i = 0; i < srvcList.length; i++) {
                                    listing.mapProd(srvcList[i]);
                                };
                            };
                            cache.put(listingKey, listing);
                        }
                    );


                    
                }
                return listing;
            },
            clearListing: function () {
                cache.remove(listingKey);
            }

        };

    } ])

    .factory('ProductDefn', [function () {
        var makeProduct = function (typ) {
            return {
                type: typ,
                discription: '',
                price: '',
                ccItemId: '',
                ccUrl: '',
                discount: function () { return 0; }
            };
        };
        var makeList = function () {
            var self = {};

            self.h1 = makeProduct('1 Heater');
            self.h2 = makeProduct('2 Heaters');

            self.l1 = makeProduct('1 Row Kit');
            self.l1h1 = makeProduct(self.l1.type + ' ' + self.h1.type);
            self.l1h2 = makeProduct(self.l1.type + ' ' + self.h2.type);

            self.l2 = makeProduct('2 Row Kit');
            self.l2h1 = makeProduct(self.l2.type + ' ' + self.h1.type);
            self.l2h2 = makeProduct(self.l2.type + ' ' + self.h2.type);

            self.l3 = makeProduct('3 Row Kit');
            self.l3h1 = makeProduct(self.l3.type + ' ' + self.h1.type);
            self.l3h2 = makeProduct(self.l3.type + ' ' + self.h2.type);

            self.mapProd = function (srcObj) {
                var rows = srcObj.LeatheRows;
                var heaters = srcObj.Heaters;

                function assgn(prod) {
                    prod.description = srcObj.Description;
                    prod.price = srcObj.Price;
                }

                if ((!rows) && (heaters == 1)) {
                    assgn(self.h1);
                } else if ((!rows) && (heaters == 2)) {
                    assgn(self.h2);
                
                } else if ((rows == 1) && (!heaters)) {
                    assgn(self.l1);
                } else if ((rows == 1) && (heaters == 1)) {
                    assgn(self.l1h1);
                } else if ((rows == 1) && (heaters == 2)) {
                    assgn(self.l1h2);

                } else if ((rows == 2) && (!heaters)) {
                    assgn(self.l2);
                } else if ((rows == 2) && (heaters == 1)) {
                    assgn(self.l2h1);
                } else if ((rows == 2) && (heaters == 2)) {
                    assgn(self.l2h2);

                } else if ((rows == 3) && (!heaters)) {
                    assgn(self.l3);
                } else if ((rows == 3) && (heaters == 1)) {
                    assgn(self.l3h1);
                } else if ((rows == 3) && (heaters == 2)) {
                    assgn(self.l3h2);
                }
            };

            function safeNum(v) {
                var f = parseFloat(v);
                if (angular.isNumber(f)) {
                    return f;
                } else {
                    return 0;
                }
            }

            function discount(lp, hp, lhp) {
                return (safeNum(lp) + safeNum(hp)) - safeNum(lhp);
            }

            self.h2.discount = function () {
                return discount(self.h1.price, self.h1.price, self.h2.price);
            };

            self.l1h1.discount = function () {
                return discount(self.l1.price, self.h1.price, self.l1h1.price);
            };
            self.l1h2.discount = function () {
                return discount(self.l1.price, self.h2.price, self.l1h2.price);
            };

            self.l2h1.discount = function () {
                return discount(self.l2.price, self.h1.price, self.l2h1.price);
            };
            self.l2h2.discount = function () {
                return discount(self.l2.price, self.h2.price, self.l2h2.price);
            };

            self.l3h1.discount = function () {
                return discount(self.l3.price, self.h1.price, self.l3h1.price);
            };
            self.l3h2.discount = function () {
                return discount(self.l3.price, self.h2.price, self.l3h2.price);
            };

            self.prodList = [];

            self.prodList.push(self.h1);
            self.prodList.push(self.h2);

            self.prodList.push(self.l1);
            self.prodList.push(self.l1h1);
            self.prodList.push(self.l1h2);

            self.prodList.push(self.l2);
            self.prodList.push(self.l2h1);
            self.prodList.push(self.l2h2);

            self.prodList.push(self.l3);
            self.prodList.push(self.l3h1);
            self.prodList.push(self.l3h2);

            return self;
        };

        return {
            makeProd: function () { return makeProduct(); },
            makeProdList: function () { return makeList(); }
        };


    } ])
;
