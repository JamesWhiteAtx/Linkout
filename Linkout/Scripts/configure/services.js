'use strict';
/* Configure Services */

angular.module('configure.services', ['ngResource'])

//AllCols
//RecCols
//IntCol
    .factory('ColorService', [function () {
        return {
            type: 'color'
        }
    } ])

    .factory('IntColService', ['KidService', 'RecColList', 'ColorService', function (KidService, RecColList, ColorService) {
        return {
            type: 'intcol',
            loadKids: function (parentNode) {
                return KidService.loadKids(parentNode, RecColList, { ctlg: parentNode.data.ctlg, carid: parentNode.data.carid, ptrnid: parentNode.data.ptrnid, intcolid: parentNode.data.intcolid }, ColorService);
            }
        }
    } ])
    .factory('AllColService', ['KidService', 'AllColList', 'ColorService', function (KidService, AllColList, ColorService) {
        return {
            type: 'allcol',
            loadKids: function (parentNode) {
                return KidService.loadKids(parentNode, AllColList, { ctlg: parentNode.data.ctlg, ptrnid: parentNode.data.ptrnid }, ColorService);
            }
        }
    } ])
    .factory('IntColorsService', ['KidService', 'IntColList', 'IntColService', function (KidService, IntColList, IntColService) {
        return {
            type: 'intColors',
            loadKids: function (parentNode) {
                return KidService.loadKids(parentNode, IntColList, { ctlg: parentNode.data.ctlg, carid: parentNode.data.carid, ptrnid: parentNode.data.ptrnid }, IntColService);
            }
        }
    } ])
    .factory('PtrnService', ['$q', 'NodeService', 'AllColList', 'IntColorsService', 'AllColService', function ($q, NodeService, AllColList, IntColorsService, AllColService) {
        return {
            type: 'ptrn',
            loadKids: function (parentNode) {
                var delay = $q.defer();
                var kids = [
                    NodeService.newNode('Interior Colors', parentNode.data, IntColorsService),
                    NodeService.newNode('All Colors', parentNode.data, AllColService)
                ];
                delay.resolve(kids);
                return delay.promise;
            }
        }
    } ]) 
    .factory('CarService', ['KidService', 'PtrnList', 'PtrnService', function (KidService, PtrnList, PtrnService) {
        return {
            type: 'car',
            loadKids: function (parentNode) {
                return KidService.loadKids(parentNode, PtrnList, { ctlg: parentNode.data.ctlg, carid: parentNode.data.carid }, PtrnService);
            }
        }
    } ])
    .factory('TrimService', ['KidService', 'CarList', 'CarService', function (KidService, CarList, CarService) {
        return {
            type: 'trim',
            loadKids: function (parentNode) {
                return KidService.loadKids(parentNode, CarList,
                    { ctlg: parentNode.data.ctlg, makeid: parentNode.data.makeid, year: parentNode.data.yearname, modelid: parentNode.data.modelid, bodyid: parentNode.data.bodyid, trimid: parentNode.data.trimid },
                    CarService);
            }
        }
    } ])
    .factory('BodyService', ['KidService', 'TrimList', 'TrimService', function (KidService, TrimList, TrimService) {
        return {
            type: 'body',
            loadKids: function (parentNode) {
                return KidService.loadKids(parentNode, TrimList,
                    { ctlg: parentNode.data.ctlg, makeid: parentNode.data.makeid, year: parentNode.data.yearname, modelid: parentNode.data.modelid, bodyid: parentNode.data.bodyid },
                    TrimService);
            }
        }
    } ])
    .factory('ModelService', ['KidService', 'BodyList', 'BodyService', function (KidService, BodyList, BodyService) {
        return {
            type: 'model',
            loadKids: function (parentNode) {
                return KidService.loadKids(parentNode, BodyList,
                    { ctlg: parentNode.data.ctlg, makeid: parentNode.data.makeid, year: parentNode.data.yearname, modelid: parentNode.data.modelid },
                    BodyService);
            }
        }
    } ])
    .factory('YearService', ['KidService', 'ModelList', 'ModelService', function (KidService, ModelList, ModelService) {
        return {
            type: 'year',
            loadKids: function (parentNode) {
                return KidService.loadKids(parentNode, ModelList, { ctlg: parentNode.data.ctlg, makeid: parentNode.data.makeid, year: parentNode.data.yearname }, ModelService);
            }
        }
    } ])
    .factory('MakeService', ['KidService', 'YearList', 'YearService', function (KidService, YearList, YearService) {
        return {
            type: 'make',
            loadKids: function (parentNode) {
                return KidService.loadKids(parentNode, YearList, { ctlg: parentNode.data.ctlg, makeid: parentNode.data.makeid }, YearService);
            }
        }
    } ])
    .factory('RootService', ['KidService', 'MakeList', 'MakeService', function (KidService, MakeList, MakeService) {
        return {
            type: 'root',
            loadKids: function (parentNode) {
                return KidService.loadKids(parentNode, MakeList, { ctlg: parentNode.data.ctlg }, MakeService);
            }
        }
    } ])

    .factory('KidService', ['$q', 'NodeService', function ($q, NodeService) {
        return {
            loadKids: function (parentNode, apiSrvc, parm, nodeSrvc) {
                var delay = $q.defer();
                apiSrvc(
                    parm,
                    function (result) {
                        var typeDisplay = result.type;
                        return $.map(result.list, function (item) {

                            if ((item.id) && (item.name)) {
                                var data = {};

                                var parentData = parentNode.data;
                                for (var name in parentData) {
                                    if (parentData.hasOwnProperty(name) && (!angular.isFunction(parentData[name]))) {
                                        data[name] = parentData[name];
                                    }
                                };

                                $.extend(data, item);

                                var type = nodeSrvc.type;
                                data[type + 'id'] = item.id;
                                data[type + 'name'] = item.name;

                                var newNode = NodeService.newNode(typeDisplay + ' ' + item.display, data, nodeSrvc);
                                return newNode;
                            }
                        });
                    }
                )
                .then(
                    function (result) {
                        delay.resolve(result.list);
                    },
                    function (err) {
                        delay.reject(err);
                    }
                );
                return delay.promise;
            }
        }
    } ])

    .factory('NodeService', [function (RootService) {
        var newNode = function (display, data, service) {
            var node = {}
            var nodeService = service;

            //Public property    
            node.type = service.type;
            node.display = display;
            node.data = data;
            node.kids = [];
            node.loaded = false;
            node.parentNode = null;
            node.isLoading = false;
            node.expanded = false;

            //Public methods
            node.canHaveKids = function () {
                if (!nodeService) {
                    return false;
                };
                var loadKidsFcn = nodeService.loadKids;
                if (!loadKidsFcn) {
                    return false;
                };
                return true;
            };
            node.loadKids = function () {
                node.isLoading = true;
                nodeService.loadKids(node)
                    .then(
                        function (kids) {
                            for (var i = 0; i < kids.length; i++) {
                                node.addKid(kids[i]);
                            };
                            node.isLoading = false;
                            node.loaded = true;
                        },
                        function (err) {
                            node.kids = [];
                            node.isLoading = false;
                            node.loaded = true;
                        }
                    );
            };
            node.addKid = function (newKid) {
                node.kids.push(newKid);
                newKid.parentNode = node;
                return newKid;
            };
            node.clearKids = function () {
                node.nodes = [];
            };

            node.hasKids = function () {
                return (angular.isArray(node.kids) && (node.kids.length > 0));
            };

            node.canExpand = function () {
                return (
                    ((!node.loaded && node.canHaveKids()) || (node.loaded && node.hasKids()))
                    &&
                    (!node.expanded)
                );
            }
            node.canCollapse = function () {
                return (node.hasKids() && node.expanded);
            }
            //node.kidsError = function () {
            //    return (node.canHaveKids() && node.loaded && !node.hasKids());
            //}

            node.exlapseState = function () {
                if (node.canCollapse()) {
                    return -1;
                } else if (node.canExpand()) {
                    return 1;
                } else {
                    return 0;
                }
            }

            node.expand = function () {
                if (node.isLoading) {
                    return;
                };
                if (node.hasKids()) {
                    node.expanded = true;
                } else if (!node.loaded && node.canHaveKids()) {
                    node.loadKids();
                    node.expanded = true;
                }
            }
            node.collapse = function () {
                node.expanded = false;
            }
            node.explapse = function () {
                if (node.isLoading) {
                    return;
                };
                if (node.expanded) {
                    node.collapse();
                } else {
                    node.expand();
                }
            }

            node.showing = function () {
                if (node.parentNode) {
                    return ((node.parentNode.expanded) && (node.parentNode.showing()));
                } else {
                    return true;
                }
            };

            //Return just the public parts
            return node;
        };

        return {
            newNode: newNode
        };

    } ])

    .factory('TreeService', ['NodeService', 'RootService', function (NodeService, RootService) {
        return {
            rootNode: function (display, data) { return NodeService.newNode(display, data || {}, RootService); }
        };
    } ])

    .factory('NSMakeLink', ['$resource', function ($resource) {
        return $resource('/netsuite/custrecmake');
    } ])

    .factory('NetsuiteLinks', ['$http', '$resource', '$q', function ($http, $resource, $q) {
        function NsStrUrlGet(httpPromise, id) {
            var delay = $q.defer();
            httpPromise.then(
                    function (result) {
                        var url = result.data + id;
                        delay.resolve(url);
                    },
                    function (err) {
                        delay.reject(err);
                    }
                );
            return delay.promise;
        };
        function NsPartsUrlGet(httpPromise, id) {
            var delay = $q.defer();
            httpPromise.then(
                    function (result) {
                        var url = result.data.prefix + id + result.data.suffix;
                        delay.resolve(url);
                    },
                    function (err) {
                        delay.reject(err);
                    }
                );
            //delay.resolve('http://shopping.sandbox.netsuite.com/s.nl/c.801095/it.A/id.' + id + '/.f');
            return delay.promise;
        };
        return {
            makeLink: function (id) {
                return NsStrUrlGet($http.get('/netsuite/custrecmake', { cache: true }), id);
            },
            modelLink: function (id) {
                return NsStrUrlGet($http.get('/netsuite/custrecmodel', { cache: true }), id);
            },
            bodyLink: function (id) {
                return NsStrUrlGet($http.get('/netsuite/custrecbody', { cache: true }), id);
            },
            trimLink: function (id) {
                return NsStrUrlGet($http.get('/netsuite/custrectrim', { cache: true }), id);
            },
            carLink: function (id) {
                return NsStrUrlGet($http.get('/netsuite/custreccar', { cache: true }), id);
            },
            patternLink: function (id) {
                return NsStrUrlGet($http.get('/netsuite/custrecpattern', { cache: true }), id);
            },
            invItemLink: function (id) {
                return NsStrUrlGet($http.get('/netsuite/item', { cache: true }), id);
            }, 
            storeItemLink: function (id) {
                return NsPartsUrlGet($http.get('/netsuite/webstoreitem', { cache: true }), id);
            }
        };
    } ])


    .factory('Stopwatch', [function () {

        var newStopwatch = function (start) {
            var watch = {};

            watch.startTime = start || new Date();

            watch.stopTime = null;
            watch.stop = function (end) {
                watch.stopTime = end || new Date();
            }

            watch.getSeconds = function () {
                if (!watch.stopTime) {
                    return 0;
                }
                return Math.floor(Math.round((watch.stopTime.getTime() - watch.startTime.getTime()) / 1000));
            }

            watch.getMinutes = function () {
                return Math.floor(watch.getSeconds() / 60);
            }
            watch.getHours = function () {
                return Math.floor(watch.getSeconds() / 60 / 60);
            }
            watch.getDays = function () {
                return Math.floor(watch.getHours() / 24);
            }
            watch.elapsedStr = function () {
                watch.stop();
                return watch.getHours() + ":" + watch.getMinutes() + ":" + watch.getSeconds();
            }

            return watch;
        };

        return function (start) {
            return newStopwatch(start);
        }
    } ])
;

