'use strict';

angular.module('linkout.services', ['ngResource'])
    .value('version', '0.1')

    .factory('SelApiList', ['$q', function ($q) {
        var successProp = 'success';
        return function (resource, parmObj, listArr, listTransform) {
            var delay = $q.defer();
            resource
                .get(parmObj,
                    function (result) {
                        if ((result) && (result[successProp])) {
                            var obj = {};
                            for (var name in result) {
                                if (name === listArr) {
                                    obj.list = result[listArr];
                                } else if ((name != successProp) && result.hasOwnProperty(name) && (typeof result[name] !== "function")) {
                                    obj[name] = result[name];
                                }
                            };

                            if (typeof listTransform === "function") {
                                obj.list = listTransform(obj);
                            }

                            delay.resolve(obj);
                        } else {
                            delay.reject(result);
                        }
                    },
                    function (result) {
                        delay.reject(result);
                    }
                );
            return delay.promise;
        };
    } ])

    .factory('Makes', ['$resource', function ($resource) {
        return $resource('/selector/makes');
    } ])
    .factory('MakeList', ['SelApiList', 'Makes', function (SelApiList, Makes) {
        return function (parm, listTransform) {
            return SelApiList(Makes, {}, "makes", listTransform);
        }
    } ])

    .factory('Years', ['$resource', function ($resource) {
        return $resource('/selector/make/:makeid/years', { makeid: '@makeid' });
    } ])
    .factory('YearList', ['SelApiList', 'Years', function (SelApiList, Years) {
        return function (parm, listTransform) {
            return SelApiList(Years, parm, "years", listTransform);
        }
    } ])

    .factory('Models', ['$resource', function ($resource) {
        return $resource('/selector/make/:makeid/year/:yearid/models', { makeid: '@makeid', yearid: '@yearid' });
    } ])
    .factory('ModelList', ['SelApiList', 'Models', function (SelApiList, Models) {
        return function (parm, listTransform) {
            return SelApiList(Models, parm, 'models', listTransform);
        }
    } ])

    .factory('Bodies', ['$resource', function ($resource) {
        return $resource('/selector/make/:makeid/year/:yearid/model/:modelid/bodies', { makeid: '@makeid', yearid: '@yearid', modelid: '@modelid' });
    } ])
    .factory('BodyList', ['SelApiList', 'Bodies', function (SelApiList, Bodies) {
        return function (parm, listTransform) {
            return SelApiList(Bodies, parm, 'bodies', listTransform);
        }
    } ])

    .factory('Trims', ['$resource', function ($resource) {
        return $resource('/selector/make/:makeid/year/:yearid/model/:modelid/body/:bodyid/trims', { makeid: '@makeid', yearid: '@yearid', modelid: '@modelid', bodyid: '@bodyid' });
    } ])
    .factory('TrimList', ['SelApiList', 'Trims', function (SelApiList, Trims) {
        return function (parm, listTransform) {
            return SelApiList(Trims, parm, 'trims', listTransform);
        }
    } ])

    .factory('Cars', ['$resource', function ($resource) {
        return $resource('/selector/make/:makeid/year/:yearid/model/:modelid/body/:bodyid/trim/:trimid/cars', { makeid: '@makeid', yearid: '@yearid', modelid: '@modelid', bodyid: '@bodyid', trimid: '@trimid' });
    } ])
    .factory('CarList', ['SelApiList', 'Cars', function (SelApiList, Cars) {
        return function (parm, listTransform) {
            return SelApiList(Cars, parm, 'cars', listTransform);
        }
    } ])

    .factory('Ptrns', ['$resource', function ($resource) {
        return $resource('/selector/car/:carid/ptrns', { carid: '@carid' });
    } ])
    .factory('PtrnList', ['SelApiList', 'Ptrns', function (SelApiList, Ptrns) {
        return function (parm, listTransform) {
            return SelApiList(Ptrns, parm, 'patterns', listTransform);
        }
    } ])

    .factory('IntCols', ['$resource', function ($resource) {
        return $resource('/selector/car/:carid/ptrn/:ptrnid/intcols', { carid: '@carid', ptrnid: '@ptrnid' });
    } ])
    .factory('IntColList', ['SelApiList', 'IntCols', function (SelApiList, IntCols) {
        return function (parm, listTransform) {
            return SelApiList(IntCols, parm, 'colors', listTransform);
        }
    } ])

    .factory('RecCols', ['$resource', function ($resource) {
        return $resource('/selector/car/:carid/ptrn/:ptrnid/intcol/:intcolid/reccols', { carid: '@carid', ptrnid: '@ptrnid', intcolid: '@intcolid' });
    } ])
    .factory('RecColList', ['SelApiList', 'RecCols', function (SelApiList, RecCols) {
        return function (parm, listTransform) {
            return SelApiList(RecCols, parm, 'colors', listTransform);
        }
    } ])

    .factory('AllCols', ['$resource', function ($resource) {
        return $resource('/selector/ptrn/:ptrnid/allcols', { ptrnid: '@ptrnid' });
    } ])
    .factory('AllColList', ['SelApiList', 'AllCols', function (SelApiList, AllCols) {
        return function (parm, listTransform) {
            return SelApiList(AllCols, parm, 'colors', listTransform);
        }
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

    .factory('ProductList', ['$http', '$q', '$cacheFactory', 'ProductDefn', function ($http, $q, $cacheFactory, ProductDefn) {
        var listingKey = 'prod_list_key';
        var cache = $cacheFactory('ProductList');

        var loadListing = function () {
            var deferred = $q.defer();
            var listing = cache.get(listingKey);
            if (listing) {
                deferred.resolve(listing);
            } else {
                $http.get('/product/listing')
                    .success(function (data) {
                        var listing = ProductDefn.makeProdList()

                        if (angular.isArray(data)) {
                            for (var i = 0; i < data.length; i++) {
                                listing.mapProd(data[i]);
                            };
                        };

                        deferred.resolve(listing);
                        cacheListing(listing);
                    })
                    .error(function () {
                        deferred.reject();
                    });
            }
            return deferred.promise;
        };
        var cacheListing = function (listing) {
            cache.put(listingKey, listing);
        };
        var clearListing = function () {
            cache.remove(listingKey);
        };

        return {
            loadListing: function () { return loadListing(); },
            clearListing: function () { return clearListing(); },
            makeProd: function () { return ProductDefn.makeProd(); },
            makeProdList: function () { return ProductDefn.makeProdList(); }
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
                var rows = srcObj.LeatherRows;
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
