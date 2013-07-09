'use strict';
/* Configure Services */

angular.module('configure.services', ['ngResource'])

//AllCols
//RecCols
//IntCol

    .factory('PtrnService', [function () {
        return {
            getType: function () { return "Pattern"; },
            getSubService: function () { return null; }
        }
    } ])
    .factory('CarService', ['PtrnService', function (PtrnService) {
        return {
            getType: function () { return "Car"; },
            getSubService: function () { return PtrnService; }
        }
    } ])
    .factory('TrimService', ['CarService', function (CarService) {
        return {
            getType: function () { return "Trim"; },
            getSubService: function () { return CarService; }
        }
    } ])
    .factory('BodyService', ['TrimService', function (TrimService) {
        return {
            getType: function () { return "Body"; },
            getSubService: function () { return TrimService; }
        }
    } ])
    .factory('ModelService', ['BodyService', function (BodyService) {
        return {
            getType: function () { return "Model"; },
            getSubService: function () { return BodyService; }
        }
    } ])
    .factory('YearService', ['ModelService', function (ModelService) {
        return {
            getType: function () { return "Year"; },
            getSubService: function () { return ModelService; }
        }
    } ])
    .factory('MakeService', ['YearService', function (YearService) {
        return {
            getType: function () { return "Make"; },
            getSubService: function () { return YearService; }
        }
    } ])
    .factory('RootService', ['MakeService', function (MakeService) {
        return {
            getType: function () { return "Root"; },
            getSubService: function () { return MakeService; }
        }
    } ])
    .factory('TreeService', ['RootService', function (RootService) {
        var newNode = function (name, subService) {
            var subServer = subService;
            var node = {}

            //Public property    
            node.type = subServer.getType();
            node.name = name;
            node.kids = [];
            node.parentNode = null;
            node.expanded = false;

            //Public methods

            node.canHaveKids = function () {
                var subService = subServer.getSubService();
                return (subService);
                //return true; // (!!subServer.getSubService());
            };
            node.addKid = function (name) {
                var subService = subServer.getSubService();
                if (subService) {
                    var newKid = newNode(name, subService);
                    node.kids.push(newKid);
                    newKid.parentNode = node;
                    node.expanded = true;
                    return newKid;
                } else {
                    return null;
                }
            };
            node.clearKids = function () {
                node.nodes = [];
            };

            node.hasKids = function () {
                return (angular.isArray(node.kids) && (node.kids.length > 0));
            };

            node.canExpand = function () {
                return (node.hasKids() && (!node.expanded));
            }
            node.canCollapse = function () {
                return (node.hasKids() && node.expanded);
            }

            node.expand = function () {
                node.expanded = true;
            }
            node.collapse = function () {
                node.expanded = false;
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
            rootNode: function (name) { return newNode(name, RootService); }
        };

    } ])

;
