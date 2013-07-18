'use strict';
/* Configure Controllers */

configure
.controller('ConfigureCtrl', ['$scope', '$location', function ($scope, $location) {

    $scope.routeMenu = function () {
        $location.path('/menu');
    }
    $scope.routeProducts = function () {
        $location.path('/products');
    }
    $scope.routeCars = function () {
        $location.path('/cars');
    }
    $scope.routeCarPatterns = function () {
        $location.path('/carPatterns');
    }

} ])
.controller('MenuCtrl', ['$scope', function ($scope) { } ])

.controller('ProductsCtrl', ['$scope', 'ProductList', 'Product', function ($scope, ProductList, Product) {
    $scope.modified = false;
    $scope.listing = {};

    var unWatchListing;
    function setListingWatch() {
        freeListingWatch();

        unWatchListing = $scope.$watch('listing', function (newVal, oldVal) {
            if (!$scope.firstWatch) {
                $scope.modified = true;
            };
            $scope.firstWatch = false;
        }, true);

    }
    function freeListingWatch() {
        if (unWatchListing) {
            unWatchListing();
        };
    }

    $scope.loadListing = function () {
        freeListingWatch();
        $scope.modified = false;
        ProductList.loadListing().then(
            function (listing) {
                $scope.listing = listing;
                $scope.firstWatch = true;
                setListingWatch();
            },
            function (err) {
                $scope.listing = null;
            });
    }

    $scope.clearListing = function () {
        ProductList.clearListing();
    }

    $scope.cacheListing = function () {
        ProductList.cacheListing($scope.listing);
    }

    $scope.saveListing = function () {
        Product.save({description:'hot cake', price: 123.34})
    }

    $scope.loadListing();
} ])

.controller('CarsCtrl', ['$scope', function ($scope) {
} ])

.controller("TreeCtrl", ['$scope', 'TreeService', 'NetsuiteLinks', function ($scope, TreeService, NetsuiteLinks) {
    $scope.tree = TreeService.rootNode("node0");
    $scope.tree.loadKids();
    $scope.tree.expanded = true;

    $scope.nodeSelect = function (node) {
        if (!node) {
            return;
        }

        if (($scope.selectNode) && ($scope.selectNode.lastSelected)) {
            $scope.selectNode.lastSelected.selected = false;
        }
        node.selected = true;

        $scope.selectNode = { type: node.type, name: node.name, display: node.display, links: [], details: [], lastSelected: node };

        var d = node.data;
        var link;
        var detail;
        for (var name in d) {
            if (d.hasOwnProperty(name) && (typeof d[name] !== "function")) {

                detail = {
                    name: name,
                    value: d[name]
                };
                $scope.selectNode.details.push(detail);

                if (name === 'makeid') {
                    link = {
                        linkurl: NetsuiteLinks.makeLink(d[name]),
                        name: 'Netsuite Make'
                    }
                }
                else if (name === 'carid') {
                    link = {
                        linkurl: NetsuiteLinks.carLink(d[name]),
                        name: 'Netsuite Car'
                    }
                }
                else if (name === 'ptrnid') {
                    link = {
                        linkurl: NetsuiteLinks.patternLink(d[name]),
                        name: 'Netsuite Pattern'
                    }
                }
                else if (name === 'invitemid') {
                    link = {
                        linkurl: NetsuiteLinks.invItemLink(d[name]),
                        name: 'Netsuite Leather Kit'
                    }
                } else {
                    link = null;
                }

                if (link) {
                    $scope.selectNode.links.push(link);
                }

            }
        };
    }
} ])

.controller("CarPatternsCtrl", ['$q', '$scope', 'MakeList', 'YearList', 'ModelList', 'BodyList', 'TrimList', 'CarList', 'PtrnList', 'IntColList', 'NetsuiteLinks', 'Stopwatch',
function ($q, $scope, MakeList, YearList, ModelList, BodyList, TrimList, CarList, PtrnList, IntColList, NetsuiteLinks, Stopwatch) {
    $scope.stopwatch = null;
    $scope.started = false;
    $scope.waiting = false;
    $scope.paused = false;
    $scope.isLoading = 0;
    $scope.errs = [];
    $scope.maxStack = 0
    $scope.async = 0
    $scope.maxAsync = 0

    var stack = [];

    $scope.levels = [
        { type: 'Make',
            checked: true,
            srvc: MakeList
        },
        { type: 'Year',
            checked: true,
            srvc: YearList,
            parmFcn: function (make) { return { makeid: make.id} },
            dataFcn: function (make) { return { makename: make.name} }
        },
        { type: 'Model',
            checked: true,
            srvc: ModelList,
            parmFcn: function (year) { return { yearid: year.id} },
            dataFcn: function (year) { return { yearname: year.name} }
        },
        { type: 'Body',
            checked: true,
            srvc: BodyList,
            parmFcn: function (model) { return { modelid: model.id} },
            dataFcn: function (model) { return { modelname: model.name, carid: model.carid} }
        },
        { type: 'Trim',
            checked: true,
            srvc: TrimList,
            parmFcn: function (body) { return { bodyid: body.id} },
            dataFcn: function (body) { return { bodyname: body.name, carid: body.carid} }
        },
        { type: 'Car',
            checked: true,
            srvc: CarList,
            parmFcn: function (trim) { return { trimid: trim.id} },
            dataFcn: function (trim) { return { trimname: trim.name, carid: trim.carid} }
        },
        { type: 'Pattern',
            checked: true,
            srvc: PtrnList,
            parmFcn: function (car) { return { carid: car.id} },
            dataFcn: function (car) { return { carname: car.name, carid: car.carid} }
        },
        { type: 'Int Color',
            checked: true,
            srvc: IntColList,
            parmFcn: function (ptrn) { return { ptrnid: ptrn.id} },
            dataFcn: function (ptrn) { return { ptrnname: ptrn.name} }
        }
    ];

    //
    $scope.$watch('levels', function () {
        var chkd = true;
        for (var i = 0; i < $scope.levels.length; i++) {
            if (chkd) {
                chkd = $scope.levels[i].checked;
            } else {
                $scope.levels[i].checked = false;
            }
        };
    }, true);

    function runLevel(idx, lastParm, lastData, item) {
        var delay = $q.defer();

        if ((idx >= $scope.levels.length) || (!$scope.levels[idx].checked)) {
            delay.resolve(null);
            return delay.promise;
        }

        var allDefined = true;

        var srvc = $scope.levels[idx].srvc;
        var parm = {};
        var parmFcn = $scope.levels[idx].parmFcn;
        if (parmFcn) {
            var p = parmFcn(item);
            for (var name in p) {
                if (!p[name]) {
                    allDefined = false;
                }
            };
            parm = $.extend({}, lastParm, p);
        };

        var data = {};
        var dataFcn = $scope.levels[idx].dataFcn;
        if (dataFcn) {
            var data = dataFcn(item);
        };
        $.extend(data, lastData);
        $.extend(data, parm);

        if (!allDefined) {
            delay.resolve($.extend({}, data, { message: 'Undefined ' + $scope.levels[idx - 1].type }));
            return delay.promise;
        }

        srvc(parm).then(
            function (result) {
                if (angular.isArray(result.list) || ((result.list.length > 0))) {
                    var childStack = [];
                    for (var i = 0; ((i < result.list.length)); i++) { // && (i < 5)
                        childStack.push(makeRun(idx + 1, parm, data, result.list[i]));
                    };
                    delay.resolve(childStack);
                } else {
                    delay.resolve($.extend({}, data, { message: 'no list' }));
                }
            },
            function (result) {
                delay.resolve($.extend({}, data, { message: result.message }));
            }
        );

        return delay.promise;
    };

    function errStackPush(data) {
        if (data.makeid) {
            data.makeLinkurl = NetsuiteLinks.makeLink(data.makeid);
        }
        if (data.modelid) {
            data.modelLinkurl = NetsuiteLinks.modelLink(data.modelid);
        }
        if (data.bodyid) {
            data.bodyLinkurl = NetsuiteLinks.bodyLink(data.bodyid);
        }
        if (data.trimid) {
            data.trimLinkurl = NetsuiteLinks.trimLink(data.trimid);
        }
        if (data.carid) {
            data.carLinkurl = NetsuiteLinks.carLink(data.carid);
        }
        if (data.ptrnid) {
            data.ptrnLinkurl = NetsuiteLinks.patternLink(data.ptrnid);
        }
        if (data.invitemid) {
            data.invitemLinkurl = NetsuiteLinks.invItemLink(data.invitemid);
        }

        $scope.errs.push(data);
    }

    function makeRun(idx, parm, data, item) {
        return { idx: idx, parm: parm, data: data, item: item };
    };

    function runNextLevel() {
        var run = stack.splice(0, 1)[0];
        if (run) {

            $scope.isLoading = $scope.isLoading + 1;

            $scope.currStack = $.extend({}, run.data, run.item);

            $scope.async = $scope.async + 1;
            if ($scope.async > $scope.maxAsync) {
                $scope.maxAsync = $scope.async;
            }

            $scope.waiting = true;
            runLevel(run.idx, run.parm, run.data, run.item)
            .then(
                function (result) {
                    $scope.async = $scope.async - 1;
                    if (result) {
                        if (angular.isArray(result)) {
                            stack = result.concat(stack);
                            if (stack.length > $scope.maxStack) {
                                $scope.maxStack = stack.length
                            }
                        } else {
                            errStackPush(result);
                        }
                    }
                    $scope.waiting = false;
                    $scope.elapStr = $scope.stopwatch.elapsedStr();
                    $scope.isLoading = $scope.isLoading - 1;
                    if (!$scope.paused) {
                        runNextLevel();
                    };
                },
                function (err) {
                    $scope.async = $scope.async - 1;
                    alert(err);
                    $scope.waiting = false;
                    $scope.elapStr = $scope.stopwatch.elapsedStr();
                    $scope.isLoading = $scope.isLoading - 1;
                }
            );

        }
    }
    $scope.reportRunning = function () {
        return $scope.waiting || (stack.length > 0);
    }
    $scope.runReport = function () {

        $scope.paused = false;
        $scope.isLoading = 0;
        $scope.errs = [];

        stack = [];
        stack.push(makeRun(0, {}, {}));

        $scope.stopwatch = Stopwatch();
        $scope.elapStr = null;

        $scope.started = true;
        $scope.waiting = false;

        runNextLevel();
    }
    $scope.pauseReport = function () {
        $scope.paused = true;
    }
    $scope.resumeReport = function () {
        $scope.paused = false;
        runNextLevel();
    }

} ])
;
