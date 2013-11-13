'use strict';

angular.module('linkout.services', ['ngResource']) 
    .value('version', '0.1')

    .factory('SelApiList', ['$q', function ($q) {
        var successProp = 'success';

        function resultObj(result, listArr, type) {
            var obj = {};
            for (var name in result) {
                if (name === listArr) {
                    obj.list = result[listArr];
                } else if ((name != successProp) && result.hasOwnProperty(name) && (typeof result[name] !== "function")) {
                    obj[name] = result[name];
                }
            };
            obj.type = type;
            return obj;
        };

        return function (resource, parmObj, listArr, type, listTransform, itemTransFcn) {
            var delay = $q.defer();
            resource
                .get(parmObj,
                    function (result) {
                        if ((result) && (result[successProp])) {
                            var obj = resultObj(result, listArr, type);

                            if (angular.isArray(obj.list)) {
                                for (var i = 0; i < obj.list.length; i++) {
                                    if (typeof itemTransFcn === "function") {
                                        itemTransFcn(obj.list[i]);
                                    }
                                    if (angular.isUndefined(obj.list[i].display)) {
                                        obj.list[i].display = obj.list[i].name;
                                    }
                                };
                            };

                            if (typeof listTransform === "function") {
                                obj.list = listTransform(obj);
                            }

                            delay.resolve(obj);
                        } else {
                            var obj = resultObj(result, listArr)
                            delay.reject(obj);
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
            return SelApiList(Makes, {}, "makes", 'Make', listTransform);
        }
    } ])

    .factory('Years', ['$resource', function ($resource) {
        return $resource('/selector/make/:makeid/years', { makeid: '@makeid' });
    } ])
    .factory('YearList', ['SelApiList', 'Years', function (SelApiList, Years) {
        return function (parm, listTransform) {
            return SelApiList(Years, parm, "years", 'Year', listTransform);
        }
    } ])

    .factory('Models', ['$resource', function ($resource) {
        return $resource('/selector/make/:makeid/year/:year/models', { makeid: '@makeid', year: '@year' });
    } ])
    .factory('ModelList', ['SelApiList', 'Models', function (SelApiList, Models) {
        return function (parm, listTransform) {
            return SelApiList(Models, parm, 'models', 'Model', listTransform);
        }
    } ]) 

    .factory('Bodies', ['$resource', function ($resource) {
        return $resource('/selector/make/:makeid/year/:year/model/:modelid/bodies', { makeid: '@makeid', year: '@year', modelid: '@modelid' });
    } ])
    .factory('BodyList', ['SelApiList', 'Bodies', function (SelApiList, Bodies) {
        return function (parm, listTransform) {
            return SelApiList(Bodies, parm, 'bodies', 'Body', listTransform);
        }
    } ])

    .factory('Trims', ['$resource', function ($resource) {
        return $resource('/selector/make/:makeid/year/:year/model/:modelid/body/:bodyid/trims', { makeid: '@makeid', year: '@year', modelid: '@modelid', bodyid: '@bodyid' });
    } ])
    .factory('TrimList', ['SelApiList', 'Trims', function (SelApiList, Trims) {
        return function (parm, listTransform) {
            return SelApiList(Trims, parm, 'trims', 'Trim', listTransform);
        }
    } ])

    .factory('Cars', ['$resource', function ($resource) {
        return $resource('/selector/make/:makeid/year/:year/model/:modelid/body/:bodyid/trim/:trimid/cars', { makeid: '@makeid', year: '@year', modelid: '@modelid', bodyid: '@bodyid', trimid: '@trimid' });
    } ])
    .factory('CarList', ['SelApiList', 'Cars', function (SelApiList, Cars) {
        return function (parm, listTransform) {
            return SelApiList(Cars, parm, 'cars', 'Car', listTransform);
        }
    } ])

    .factory('Ptrns', ['$resource', function ($resource) {
        return $resource('/selector/car/:carid/ptrns', { carid: '@carid' });
    } ])
    .factory('PtrnList', ['SelApiList', 'Ptrns', function (SelApiList, Ptrns) {
        return function (parm, listTransform) {
            return SelApiList(Ptrns, parm, 'patterns', 'Pattern', listTransform,
                function (item) { item.display = item.seldescr + ' (' + item.name + ')'; });
        }
    } ])

    .factory('IntCols', ['$resource', function ($resource) {
        return $resource('/selector/car/:carid/ptrn/:ptrnid/intcols', { carid: '@carid', ptrnid: '@ptrnid' });
    } ])
    .factory('IntColList', ['SelApiList', 'IntCols', function (SelApiList, IntCols) {
        return function (parm, listTransform) {
            return SelApiList(IntCols, parm, 'intColors', 'Interior Color', listTransform,
                function (item) { item.display = item.name + ' (' + item.id + ')'; });
        }
    } ])

    .factory('RecCols', ['$resource', function ($resource) {
        return $resource('/selector/car/:carid/ptrn/:ptrnid/intcol/:intcolid/reccols', { carid: '@carid', ptrnid: '@ptrnid', intcolid: '@intcolid' });
    } ])
    .factory('RecColList', ['SelApiList', 'RecCols', function (SelApiList, RecCols) {
        return function (parm, listTransform) {
            return SelApiList(RecCols, parm, 'kits', 'Reccomended Color', listTransform,
                function (item) {
                    item.display = item.leacolorname + ' (' + item.rectype + ')';
                    item.invitemid = item.id;
                });
        }
        } ])

    .factory('AllCols', ['$resource', function ($resource) {
        return $resource('/selector/ptrn/:ptrnid/allcols', { ptrnid: '@ptrnid' });
    } ])
    .factory('AllColList', ['SelApiList', 'AllCols', function (SelApiList, AllCols) {
        return function (parm, listTransform) {
            return SelApiList(AllCols, parm, 'kits', 'Color', listTransform,
                function (item) {
                    item.display = item.leacolorname;
                    item.invitemid = item.id;
                });
        }
    } ])

    .factory('Installers', ['$resource', function ($resource) {
        return $resource('/schedule/installers/:zipcode', { zipcode: '@zipcode' });
    } ])

//    .factory('ProductPrice', ['$http', function ($http) {
//        return {
//            leather: function (rows) {
//                return $http.get('/price/leather/' + rows, { cache: true });
//            },
//            heater: function () {
//                return $http.get('/price/heater');
//            }
//        };
//    } ])

    .factory('Product', ['$resource', function ($resource) {
        return $resource('/product/:id', { id: '@id' });
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

            var origProp = function (name) {
                return prod.hasOwnProperty(name)
                && !angular.isFunction(prod[name])
                && !angular.isArray(prod[name]);
            }

            var prod = {
                type: typ,
                id: '',
                code: '',
                description: '',
                price: '',
                ccItemId: '',
                ccUrl: ''
            };
            prod.discount = function () { return 0; },

            prod.updateChanged = function () {
                prod.origVals = {
                    type: prod.type,
                    id: prod.id,
                    code: prod.code,
                    description: prod.description,
                    price: prod.price,
                    ccItemId: prod.ccItemId,
                    ccUrl: prod.ccUrl
                };
            };

            prod.isChanged = function () {
                var changed = false;
                if (prod.origVals) {
                    for (var name in prod.origVals) {
                        if (origProp(name)) {
                            if (prod.origVals[name] != prod[name]) {
                                changed = true;
                                break;
                            }
                        }
                    };
                };
                return changed;
            };

            prod.updateChanged();
            return prod;
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

                    prod.id = srcObj.ID;
                    prod.code = srcObj.Code;

                    prod.updateChanged();
                };

                var prod = self.findProd(rows, heaters);
                if (prod) {
                    assgn(prod);
                }
            }

            self.findProd = function (rows, heaters) {
                var prod = null;
                if ((!rows) && (heaters == 1)) {
                    prod = self.h1;
                } else if ((!rows) && (heaters == 2)) {
                    prod = self.h2;

                } else if ((rows == 1) && (!heaters)) {
                    prod = self.l1;
                } else if ((rows == 1) && (heaters == 1)) {
                    prod = self.l1h1;
                } else if ((rows == 1) && (heaters == 2)) {
                    prod = self.l1h2;

                } else if ((rows == 2) && (!heaters)) {
                    prod = self.l2;
                } else if ((rows == 2) && (heaters == 1)) {
                    prod = self.l2h1;
                } else if ((rows == 2) && (heaters == 2)) {
                    prod = self.l2h2;

                } else if ((rows == 3) && (!heaters)) {
                    prod = self.l3;
                } else if ((rows == 3) && (heaters == 1)) {
                    prod = self.l3h1;
                } else if ((rows == 3) && (heaters == 2)) {
                    prod = self.l3h2;
                };
                return prod;
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

            self.isListChanged = function () {
                var changed = false;
                var prod;
                for (var i = 0; i < self.prodList.length; i++) {
                    prod = self.prodList[i];
                    if (prod.isChanged()) {
                        changed = true;
                        break;
                    }
                };
                return changed;
            };

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

    .factory('UniqueProps', [function () {

        var objProp = function (obj, name) {
            return obj.hasOwnProperty(name)
                && !angular.isFunction(obj[name])
                && !angular.isArray(obj[name]);
        }
        var getObjCompVal = function (obj, name) {
            if (objProp(obj, name)) {
                return obj[name];
            } else {
                return null;
            }
        }
        //var makeValCompObj = function (name, value) {            return { name: name, value: value, unique: false };        }

        var uniqueProps = function (arr, propList, uniqueListName) {

            if ((!angular.isArray(arr)) || (arr.length < 2)) {
                return 0;
            }

            var listName = uniqueListName || 'uniques';

            var obj,
                propObj,
                uniqueProps = [];

            // loop property list
            for (var p = 0; p < propList.length; p++) {
                propObj = propList[p];

                var compVal;
                // loop array to identify non-unique property values
                for (var o = 0; o < arr.length; o++) {
                    obj = arr[o];

                    var curVal = getObjCompVal(obj, propObj.name);
                    // if first element, store values, there is nothing to compare yet
                    if (o === 0) {
                        compVal = curVal;
                    } else {
                        if (compVal !== curVal) {
                            uniqueProps.push(propObj);
                            break;
                        }
                    }

                }
            }

            // loop list of property names found to have unique values
            for (var p = 0; p < uniqueProps.length; p++) {
                propObj = uniqueProps[p];
                // loop array to add non-unique property values do each item's details list
                for (var o = 0; o < arr.length; o++) {
                    obj = arr[o];
                    var list = obj[listName] || [];

                    var newPropObj = $.extend({ value: getObjCompVal(obj, propObj.name) }, propObj);
                    list.push(newPropObj);

                    obj[listName] = list;
                }
            }

            return uniqueProps.length;
        }

        return function (arr, propList) {
            return uniqueProps(arr, propList);
        }

    } ])

;
