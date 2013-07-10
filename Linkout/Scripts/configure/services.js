'use strict';
/* Configure Services */

angular.module('configure.services', ['ngResource'])

//AllCols
//RecCols
//IntCol

    .factory('PtrnService', [function () {
    } ])
    .factory('CarService', ['$q', 'Ptrns', 'NodeService', 'PtrnService', function ($q, Ptrns, NodeService, PtrnService) {
    } ])
    .factory('TrimService', ['$q', 'Cars', 'NodeService', 'CarService', function ($q, Cars, NodeService, CarService) {
    } ])
    .factory('BodyService', ['$q', 'Trims', 'NodeService', 'TrimService', function ($q, Trims, NodeService, TrimService) {
    } ])
    .factory('ModelService', ['$q', 'Bodies', 'NodeService', 'BodyService', function ($q, Bodies, NodeService, BodyService) {
    } ])
    .factory('YearService', ['$q', 'Models', 'NodeService', 'ModelService', function ($q, Models, NodeService, ModelService) {
        return {
            loadKids: function (parentNode) {
                var delay = $q.defer();
                Models.get({ makeid: parentNode.data.id, yearid: '' },
                    function (result) {
                        if ((result) && (result.success)) {
                            var list = $.map(result.models, function (item) {
                                return NodeService.newNode("Model", item.name, item, ModelService);
                            });
                            delay.resolve(list);
                        } else { delay.reject(result); }
                    },
                    function (result) { delay.reject(result); }
                );
                return delay.promise;
            }
        }
    } ])
    .factory('MakeService', ['KidService', 'Years', 'YearService', function (KidService, Years, YearService) {
        return {
            loadKids: function (parentNode) {
                return KidService.loadKids(parentNode, Years, "years", { makeid: parentNode.data.id }, "Year", YearService);
            }
        }
    } ])
    .factory('RootService', ['KidService', 'Makes', 'MakeService', function (KidService, Makes, MakeService) {
        return {
            loadKids: function (parentNode) {
                return KidService.loadKids(parentNode, Makes, "makes", {}, "Make", MakeService);
            }
        }
    } ])

    .factory('KidService', ['$q', 'NodeService', function ($q, NodeService) {
        return {
            loadKids: function (parentNode, apiSrvc, listArr, parm, type, nodeSrvc) {
                var delay = $q.defer();
                apiSrvc.get(parm,
                    function (result) {
                        if ((result) && (result.success)) {
                            var list = $.map(result[listArr], function (item) {
                                var newNode = NodeService.newNode(type, item.name, item, nodeSrvc);
                                return newNode;
                            });
                            delay.resolve(list);
                        } else { delay.reject(result); }
                    },
                    function (result) { delay.reject(result); }
                );
                return delay.promise;
            }
        }
    } ])

    .factory('NodeService', [function (RootService) {
        var newNode = function (type, name, data, service) {
            var node = {}
            var nodeService = service;

            //Public property    
            node.type = type; //nodeService.getType();
            node.name = name;
            node.data = data;
            node.kids = [];
            node.loaded = false;
            node.parentNode = null;
            node.isLoading = false;
            node.expanded = false;

            //Public methods
            node.canHaveKids = function () {
                if (!nodeService) {
                    return true;
                };
                var canHaveFcn = nodeService.canHaveKids;
                if (canHaveFcn) {
                    return canHaveFcn();
                } else {
                    return true;
                };
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
                //var newKid = newNode(name, nodeService);
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
            rootNode: function (name) { return NodeService.newNode("Root", name, {}, RootService); }
        };
    } ])

;
