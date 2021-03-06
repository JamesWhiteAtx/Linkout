﻿/*
Roadwire Costco 
(c) 2014 Roadwire, Inc.
*/
costco
.controller('CostcoCtrl', ['$rootScope', '$scope', '$location', 'WhyInstDlg', function ($rootScope, $scope, $location, WhyInstDlg) {
    $scope.routeLea = function () {
        $location.path('/leather');
    };
    $scope.routeHtr = function () {
        $location.path('/heaters');
    };
    $scope.routeInst = function () {
        $location.path('/install');
    };
    $scope.routeConfirm = function () {
        $location.path('/confirm');
    };
    $scope.whyInstall = function () {
        WhyInstDlg();
    };
}])

.controller('TestCtrl', ['$scope', function ($scope) {
    $scope.test = "test";

    $scope.show2 = false;
    $scope.show3 = false;
    $scope.show4 = false;

    $scope.toggle2 = function () {
        $scope.show2 = !$scope.show2;
    };
    $scope.toggle3 = function () {
        $scope.show3 = !$scope.show3;
    };
    $scope.toggle4 = function () {
        $scope.show4 = !$scope.show4;
    };
}])

.controller('LeaCtrl', ['$scope', 'Data', function ($scope, Data) {
    $scope.trimIsLoading = function () {
        return (($scope.trim) && ($scope.trim.isLoading))
        || (($scope.car) && ($scope.car.isLoading))
        || (($scope.ptrn) && ($scope.ptrn.isLoading));
    }
    $scope.trimHasOpts = function () {
        return (($scope.trim) && ($scope.trim.list) && ($scope.trim.list.length > 1))
        || (($scope.car) && ($scope.car.list) && ($scope.car.list.length > 1))
        || (($scope.ptrn) && ($scope.ptrn.list) && ($scope.ptrn.list.length > 1));
    };
    $scope.trimShowLoading = function () {
        return $scope.trimIsLoading() && !$scope.trimHasOpts();
    };
    $scope.trimShowOpts = function () {
        return $scope.trimHasOpts();
    };


    $scope.intIsLoading = function () {
        return ($scope.int) && (($scope.int.isLoading));
    };
    $scope.intHasOpts = function () {
        return ($scope.int) && (($scope.int.list) && ($scope.int.list.length > 1));
    };
    $scope.intShowLoading = function () {
        return $scope.intIsLoading() && !$scope.intHasOpts();
    };
    $scope.intShowOpts = function () {
        return $scope.intHasOpts();
    };

    $scope.kitHasOpts = function () {
        return ($scope.kit) && (($scope.kit.list) && ($scope.kit.list.length > 0));
    };

    $scope.kitShowLoading = function () {
        $scope.kit.isloading && !$scope.kitHasOpts();
    };
    $scope.kitShowOpts = function () { 
        return $scope.kitHasOpts() || $scope.kit.isloading;
    };

    $scope.pickKit = function (idx) {
        $scope.kit.obj = $scope.kit.list[idx];
        Data.order.loadSlctr();
        $scope.routeConfirm();
    };

    $scope.price = function () {
        if ($scope.ptrn && $scope.ptrn.obj) {
            return Data.prodSrvc.leaPrice($scope.ptrn.obj.rowsid);
        };
    };

    function isUndefinedOrNull(val) {
        return (angular.isUndefined(val) || val == null);
    };

    function sameVals(newVal, oldVal) {
        return (newVal === oldVal) || (isUndefinedOrNull(newVal) && isUndefinedOrNull(oldVal));
    };

    var assgnSlctLevel = function (lvlDefn) {
        $scope[lvlDefn.name] = lvlDefn;

        var watchStr = lvlDefn.name + '.obj';
        $scope.$watch(watchStr, function (newVal, oldVal) {
            if (sameVals(newVal, oldVal)) { return; };

            if (!isUndefinedOrNull(lvlDefn.obj)) {
                lvlDefn.loadNextLvl();
            } else {
                lvlDefn.clearNextLvl();
            };
            //$scope.levelChanged();
        });

        return lvlDefn;
    };

    Data.walkLevels(assgnSlctLevel);

    if ($scope.make.list.length < 1) {
        $scope.make.loadLvl();
    };
}])

.controller('HtrCtrl', ['$scope', 'Data', function ($scope, Data) {
    var rows = Data.order.getRows();
    var heaters = 2;
    $scope.rows = rows;

    $scope.price = Data.prodSrvc.getHtrDiff(rows, heaters);
    
    $scope.addHtrs = function () {
        Data.order.loadHtrs(heaters);
        $scope.routeConfirm();
    };
    $scope.noHtrs = function () {
        Data.order.clearHtrs();
        Data.order.loadHtrs(0);
        $scope.routeConfirm();
    };
}])

.controller('InstCtrl', ['$scope', 'Data', 'Installers', function ($scope, Data, Installers) {
    $scope.installers = [];

    Installers()
    .then(function (locs) {
        $scope.installers = locs;
    });
}])

.controller('ConfirmCtrl', ['$scope', 'Data', function ($scope, Data) {
    $scope.lines = [];
    var data = Data;
    var carLine;
    var leaLine;
    var htrLine;

    var addLine = function (title, installed, url, edtFcn, delFcn) {
        var line = {
            title: title,
            installed: installed,
            url: url,
            items: [],
            edtFcn: edtFcn, 
            delFcn: delFcn
        };

        line.item = function (descr, total) {
            line.items.push({descr: descr, total: total});
            return line;
        };

        $scope.lines.push(line);
        return line;
    };

    var delLine = function (delLn) {
        if (!delLn) {
            return;
        };
        angular.forEach($scope.lines, function (line, idx) {
            if (line == delLn) {
                $scope.lines.splice(idx, 1);
                return false;
            };
        });
    };

    var delHtr = function (idx) {
        Data.order.clearHtrs();
        delLine(htrLine);
        htrLine = null;
    };
    var delLea = function (idx) {
        Data.order.clearLea();
        delLine(leaLine);
        leaLine = null;

        delHtr(htrLine);
    };

    $scope.hasLea = function () {
        return Data.order.hasLea();
    };

    $scope.hasHtrs = function () {
        return Data.order.hasHtrs();
    };

    $scope.hasProd = function () {
        return Data.order.hasProd();
    };
    $scope.hasMember = function () {
        return Data.member.complete();
    };

    $scope.confirmable = function () {
        return Data.confirmable() && (!!$scope.prodUrl());
    };
    
    var objProp = function (str, obj, nm) {
        str = str || '';
        nm = nm || 'name';
        if (obj && obj[nm]) {
            str = str + ' ' + obj[nm];
        };
        return str.trim();
    };

    var descr = '';

    if (Data.order.hasCar()) {
        descr = objProp('', Data.order.car.make);
        descr = objProp(descr, Data.order.car.year);
        descr = objProp(descr, Data.order.car.model);
        descr = objProp(descr, Data.order.car.car);
        carLine = addLine('Your Vehicle', false, null, function () { $scope.routeLea(); }).item(descr);
    };

    if (Data.order.hasLea()) {
        leaLine = addLine('Leather Seat', true, Data.order.lea.dispUrl, function () { $scope.routeLea(); }, delLea);

        leaLine.item(Data.order.lea.rowsDisp, Data.order.lea.price);

        descr = objProp('Color: ', Data.order.lea.color, 'name');
        leaLine.item(descr);

        descr = objProp('Part Number: ', Data.order.lea.kit);
        leaLine.item(descr);

        descr = objProp('Pattern: ', Data.order.lea.ptrn);
        leaLine.item(descr);
    } else {
        leaLine = null;
    };

    if (Data.order.hasHtrs()) {
        htrLine = addLine('Seat Heaters', true, null, function () { $scope.routeHtr(); }, delHtr)
            .item(Data.order.htrs.drv.disp, Data.order.htrs.price);

        if (Data.order.htrs.psg) {
            htrLine.item(Data.order.htrs.psg.disp);
        }
    } else {
        htrLine = null;
    };
    
    $scope.prodPrice = function() {
        return Data.order.getTotal();
    };
    $scope.prodDescr = function() {
        return Data.order.prodDescr();
    };
    $scope.prodUrl = function () {
        return Data.order.prodUrl();
    };

    $scope.linkToCostCo = function () {
        $scope.confirming = true;
        Data.order.uploadSave().catch(function () { $scope.confirming = false; });
    };

    $scope.alerts = [];
    var addAlert = function (quest, info, yes, showFcn, addFcn) {
        var alert = { quest: quest, info: info, yes: yes, showFcn: showFcn, addFcn: addFcn };
        $scope.alerts.push(alert);
    };

    addAlert('Interested in purchasing Leather Seat Covers?',
        'Roadwire offers the finest leather interiors in the business. Take a look at our excellent offers!',
        'Shop for Leather Seat Covers',
        function () { return !$scope.hasLea(); },
        $scope.routeLea
    );

    addAlert('Interested in adding Seat Heaters?',
        'Roadwire offers the finest seat heating systems in the business. Take a look at our excellent offers!',
        'Shop for Seat Heaters',
        function () { return $scope.hasLea() && !$scope.hasHtrs(); },
        $scope.routeHtr
    );

    $scope.member = Data.member;
}])

.controller('MapCtrl', ['$scope', 'gglMaps', function ($scope, gglMaps) {
    $scope.gglMaps = gglMaps;
}])

;