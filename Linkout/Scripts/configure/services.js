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
                return KidService.loadKids(parentNode, RecColList, { carid: parentNode.data.carid, ptrnid: parentNode.data.ptrnid, intcolid: parentNode.data.intcolid }, ColorService);
            }
        }
    } ])
    .factory('AllColService', ['KidService', 'AllColList', 'ColorService', function (KidService, AllColList, ColorService) {
        return {
            type: 'allcol',
            loadKids: function (parentNode) {
                return KidService.loadKids(parentNode, AllColList, { ptrnid: parentNode.data.ptrnid }, ColorService);
            }
        }
    } ])
    .factory('IntColorsService', ['KidService', 'IntColList', 'IntColService', function (KidService, IntColList, IntColService) {
        return {
            type: 'intColors',
            loadKids: function (parentNode) {
                return KidService.loadKids(parentNode, IntColList, { carid: parentNode.data.carid, ptrnid: parentNode.data.ptrnid }, IntColService);
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
                return KidService.loadKids(parentNode, PtrnList, { carid: parentNode.data.carid }, PtrnService);
            }
        }
    } ])
    .factory('TrimService', ['KidService', 'CarList', 'CarService', function (KidService, CarList, CarService) {
        return {
            type: 'trim',
            loadKids: function (parentNode) {
                return KidService.loadKids(parentNode, CarList,
                    { makeid: parentNode.data.makeid, yearid: parentNode.data.yearid, modelid: parentNode.data.modelid, bodyid: parentNode.data.bodyid, trimid: parentNode.data.trimid },
                    CarService);
            }
        }
    } ])
    .factory('BodyService', ['KidService', 'TrimList', 'TrimService', function (KidService, TrimList, TrimService) {
        return {
            type: 'body',
            loadKids: function (parentNode) {
                return KidService.loadKids(parentNode, TrimList,
                    { makeid: parentNode.data.makeid, yearid: parentNode.data.yearid, modelid: parentNode.data.modelid, bodyid: parentNode.data.bodyid },
                    TrimService);
            }
        }
    } ])
    .factory('ModelService', ['KidService', 'BodyList', 'BodyService', function (KidService, BodyList, BodyService) {
        return {
            type: 'model',
            loadKids: function (parentNode) {
                return KidService.loadKids(parentNode, BodyList,
                    { makeid: parentNode.data.makeid, yearid: parentNode.data.yearid, modelid: parentNode.data.modelid },
                    BodyService);
            }
        }
    } ])
    .factory('YearService', ['KidService', 'ModelList', 'ModelService', function (KidService, ModelList, ModelService) {
        return {
            type: 'year',
            loadKids: function (parentNode) {
                return KidService.loadKids(parentNode, ModelList, { makeid: parentNode.data.makeid, yearid: parentNode.data.yearid }, ModelService);
            }
        }
    } ])
    .factory('MakeService', ['KidService', 'YearList', 'YearService', function (KidService, YearList, YearService) {
        return {
            type: 'make',
            loadKids: function (parentNode) {
                return KidService.loadKids(parentNode, YearList, { makeid: parentNode.data.makeid }, YearService);
            }
        }
    } ])
    .factory('RootService', ['KidService', 'MakeList', 'MakeService', function (KidService, MakeList, MakeService) {
        return {
            type: 'root',
            loadKids: function (parentNode) {
                return KidService.loadKids(parentNode, MakeList, {}, MakeService);
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
                        return $.map(result.list, function (item) {

                            if ((item.id) && (item.name)) {
                                var data = {};

                                var parentData = parentNode.data;
                                for (var name in parentData) {
                                    if (parentData.hasOwnProperty(name) && (typeof parentData[name] !== "function")) {
                                        data[name] = parentData[name];
                                    }
                                };

                                $.extend(data, item);

                                var type = nodeSrvc.type;
                                data[type + 'id'] = item.id;
                                data[type + 'name'] = item.name;

                                var newNode = NodeService.newNode(item.display, data, nodeSrvc);
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
            node.kidsError = function () {
                return (node.canHaveKids() && node.loaded && !node.hasKids());
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
            rootNode: function (display) { return NodeService.newNode(display, {}, RootService); }
        };
    } ])

    .factory('NetsuiteLinks', ['$http', function ($http) {
        return {
            makeLink: function (id) {
                return 'https://system.sandbox.netsuite.com/app/common/custom/custrecordentry.nl?rectype=19&id=' + id; //&e=T
            },
            modelLink: function (id) {
                return 'https://system.sandbox.netsuite.com/app/common/custom/custrecordentry.nl?rectype=20&id=' + id; 
            },
            bodyLink: function (id) {
                return 'https://system.sandbox.netsuite.com/app/common/custom/custrecordentry.nl?rectype=21&id=' + id; 
            },
            trimLink: function (id) {
                return 'https://system.sandbox.netsuite.com/app/common/custom/custrecordentry.nl?rectype=69&id=' + id;
            },
            carLink: function (id) {
                return 'https://system.sandbox.netsuite.com/app/common/custom/custrecordentry.nl?rectype=63&id=' + id; 
            },
            patternLink: function (id) {
                return 'https://system.sandbox.netsuite.com/app/common/custom/custrecordentry.nl?rectype=13&id=' + id; 
            },
            invItemLink: function (id) {
                return 'https://system.sandbox.netsuite.com/app/common/item/item.nl?id=' + id;  //&e=T
            }
        };
    } ])

;

