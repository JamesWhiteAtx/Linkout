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
    $scope.listing = {};

    $scope.invalidForms = [];

    $scope.addInvalidForm = function (id) {
        var idx = $scope.invalidForms.indexOf(id);
        if (idx == -1) {
            $scope.invalidForms.push(id);
        }
    }
    $scope.delInvalidForm = function (id) {
        var idx = $scope.invalidForms.indexOf(id);
        if (idx != -1) {
            $scope.invalidForms.splice(idx, 1);
        }
    }
    $scope.anyInvalidForms = function () {
        return (($scope.invalidForms) && ($scope.invalidForms.length > 0));
    }

    $scope.loadListing = function () {
        ProductList.loadListing().then(
            function (listing) {
                $scope.listing = listing;
            },
            function (err) {
                $scope.listing = null;
            });
    }

    $scope.clearListing = function () {
        $scope.listing = {};
        ProductList.clearListing();
    }

    $scope.cacheListing = function () {
        ProductList.cacheListing($scope.listing);
    }

    $scope.saveListing = function () {
        var prod;
        var saved = false;
        for (var i = 0; i < $scope.listing.prodList.length; i++) {
            prod = $scope.listing.prodList[i];
            if (prod.isChanged()) {
                Product.save({ id: prod.id }, { description: prod.description, price: prod.price });
                saved = true;
            }
        };
        if (saved) {
            $scope.clearListing();
            $scope.loadListing();
        }

    }

    $scope.loadListing();
} ])

.controller('ProdController', ['$scope', function ($scope) {
    $scope.$watch(
        function () { return { description: $scope.p.description, price: $scope.p.price }; },
        function (newVal, oldVal) {
            if ($scope.prodForm.$valid) {
                $scope.delInvalidForm($scope.p.id);
            } else {
                $scope.addInvalidForm($scope.p.id);
            }
        },
        true
    );
} ])

.controller('CarsCtrl', ['$scope', function ($scope) {
} ])

.controller('DropdownCtrl', ['$scope', function ($scope) {
} ])

.controller("TreeCtrl", ['$scope', 'TreeService', 'NetsuiteLinks', function ($scope, TreeService, NetsuiteLinks) {
    $scope.tree = TreeService.rootNode("node0");
    $scope.tree.loadKids();
    $scope.tree.expanded = true;

    function linkPromise(promise, id, name) {
        promise(id)
            .then(
                function (url) {
                    $scope.selectNode.links.push({ name: name, linkurl: url });
                },
                function (err) {
                    alert(err);
                }
            );
    };

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
        //var link = {};
        var detail;
        for (var name in d) {
            if (d.hasOwnProperty(name) && (typeof d[name] !== "function")) {

                detail = {
                    name: name,
                    value: d[name]
                };
                $scope.selectNode.details.push(detail);

                if (name === 'makeid') {
                    linkPromise(NetsuiteLinks.makeLink, d[name], 'Netsuite Make');
                }
                else if (name === 'carid') {
                    linkPromise(NetsuiteLinks.carLink, d[name], 'Netsuite Car');
                }
                else if (name === 'ptrnid') {
                    linkPromise(NetsuiteLinks.patternLink, d[name], 'Netsuite Custom Pattern');
                }
                else if (name === 'invitemid') {
                    linkPromise(NetsuiteLinks.invItemLink, d[name], 'Netsuite Leather Item');
                    linkPromise(NetsuiteLinks.storeItemLink, d[name], 'Web Store Kit');
                }
                else if (name === 'ptrnitemid') {
                    linkPromise(NetsuiteLinks.invItemLink, d[name], 'Netsuite Pattern Item');
                    linkPromise(NetsuiteLinks.storeItemLink, d[name], 'Web Store Pattern');
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
            srvc: getCurMakeList
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
                    var child;
                    for (var i = 0; ((i < result.list.length)); i++) { // && (i < 5)
                        child = result.list[i];
                        if (!(child.included === false)) {
                            childStack.push(makeRun(idx + 1, parm, data, child));
                        }
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

    $scope.limitMakes = false;

    var checkMks = function (check) {
        getCurMakeList().then(
            function (result) {
                for (var i = 0; ((i < $scope.makes.list.length)); i++) {
                    $scope.makes.list[i].checked = check;
                };
            }
        );
    }
    $scope.unCheckMks = function () {
        checkMks(false);
    }
    $scope.checkAllMks = function () {
        checkMks(true);
    }

    $scope.openMksDlg = function (result) {
        getCurMakeList().then(
            function (result) {
                for (var i = 0; ((i < $scope.makes.list.length)); i++) {
                    $scope.makes.list[i].checked = $scope.makes.list[i].included || false;
                };
                $scope.mksDlgOpen = true;
            }
        );
    };
    $scope.closeMksDlg = function (result) {
        $scope.mksDlgOpen = false;
    }
    $scope.okMksDlg = function (result) {
        getCurMakeList().then(
            function (result) {
                for (var i = 0; ((i < $scope.makes.list.length)); i++) {
                    $scope.makes.list[i].included = $scope.makes.list[i].checked || false;
                };
                $scope.mksDlgOpen = false;
            }
        );
    }
    $scope.cancelMksDlg = function (result) {
        $scope.mksDlgOpen = false;
    }

    $scope.mksDlgOpts = {
        backdropFade: true,
        dialogFade: true
    }

    $scope.makes;

    function getCurMakeList() {
        var delay = $q.defer();
        if ($scope.makes) {
            delay.resolve($scope.makes);
        } else {
            MakeList({}).then(
                function (result) {
                    $scope.makes = result;

                    for (var i = 0; ((i < $scope.makes.list.length)); i++) {
                        $scope.makes.list[i].included = true;
                        $scope.makes.list[i].checked = true;
                    };

                    delay.resolve($scope.makes);
                },
                function (result) {
                    $scope.makes = [];
                    delay.reject(result);
                }
            );
        };
        return delay.promise;
    };

    $scope.checkAllMks();

} ])

;
