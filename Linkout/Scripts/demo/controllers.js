'use strict';
/* Demo Controllers */

configure
.controller('DemoCtrl', ['$scope', '$location', function ($scope, $location) {

} ])

.controller('NsOrderCtrl', ['$scope', '$q', 'MakeList', 'YearList', 'ModelList', 'BodyList', 'TrimList', 'CarList', 'PtrnList', 'IntColList', 'RecColList', 'AllColList',
function ($scope, $q, MakeList, YearList, ModelList, BodyList, TrimList, CarList, PtrnList, IntColList, RecColList, AllColList) {

    var carLevels = [
    {
        srvc: MakeList,
        parmFcn: function (make) { return { makeid: make.id} }
    },
    {
        srvc: YearList,
        parmFcn: function (year) { return { yearid: year.id} }
    },
    {
        srvc: ModelList,
        parmFcn: function (model) { return { modelid: model.id} }
    },
    {
        srvc: BodyList,
        parmFcn: function (body) { return { bodyid: body.id} }
    },
    {
        srvc: TrimList,
        parmFcn: function (trim) { return { trimid: trim.id} }
    },
    {
        srvc: CarList,
        parmFcn: function (car) { return { carid: car.id} }
    }
    ];


    var kitLevels = [
    {
        srvc: PtrnList,
        parmFcn: function (ptrn) { return { ptrnid: ptrn.id} }
    },
    {
        srvc: promiseColType
    },
    {
        srvc: undefined
    }
    ];

    function promiseColType() {
        var delay = $q.defer();

        var result = {
            type: 'Select By',
            list: [{ id: 0, display: 'Recommended Colors' }, { id: 1, display: 'All Colors'}]
        };

        delay.resolve(result);
        return delay.promise;
    };

    function finishKitLevels(recId) {
        kitLevels.splice(2);
        if (recId === 0) {
            kitLevels.push({
                srvc: IntColList,
                parmFcn: function (intCol) { return { intcolid: intCol.id} }
            });
            kitLevels.push({ srvc: RecColList });
        } else if (recId === 1) {
            kitLevels.push({ srvc: AllColList });
        }
    };


    $scope.carLevel = 0;
    $scope.carType = undefined;
    $scope.carList = [];

    $scope.kitLevel = 0;
    $scope.kitType = undefined;
    $scope.kitList = [];

    function assignCurCarLvlToList(result) {
        if (angular.isDefined(result)) {
            carLevels[$scope.carLevel].type = result.type;
            carLevels[$scope.carLevel].list = result.list;
        }
        $scope.carType = carLevels[$scope.carLevel].type;
        $scope.carList = carLevels[$scope.carLevel].list;
    }

    function assignCurKitLvlToList(result) {
        if (angular.isDefined(result)) {
            kitLevels[$scope.kitLevel].type = result.type;
            kitLevels[$scope.kitLevel].list = result.list;
        }
        $scope.kitType = kitLevels[$scope.kitLevel].type;
        $scope.kitList = kitLevels[$scope.kitLevel].list;
    }

    function makeCarParm() {
        var parm = {};
        var lvl, lvlPrm;
        for (var i = 0; i < $scope.carLevel; i++) {
            lvl = carLevels[i];
            if ((lvl.data) && (lvl.parmFcn)) {
                lvlPrm = lvl.parmFcn(lvl.data);
                $.extend(parm, lvlPrm);
            }
        }
        return parm;
    }

    function makeKitParm() {
        var parm = { carid: $scope.currentCar.id };
        var lvl, lvlPrm;
        for (var i = 0; i < $scope.kitLevel; i++) {
            lvl = kitLevels[i];
            if ((lvl.data) && (lvl.parmFcn)) {
                lvlPrm = lvl.parmFcn(lvl.data);
                $.extend(parm, lvlPrm);
            }
        }
        return parm;
    }

    function loadCurCarLevel() {
        var delay = $q.defer();

        $scope.carLoading = true;
        assignCurCarLvlToList();

        if (angular.isArray($scope.carList) && ($scope.carList.length > 0)) {
            delay.resolve($scope.carList);
            $scope.carLoading = false;
        } else {
            var parm = makeCarParm();
            carLevels[$scope.carLevel].srvc(parm)
            .then(
                function (result) {
                    assignCurCarLvlToList(result);
                    $scope.carLoading = false;
                    delay.resolve($scope.carList);
                },
                function (result) {
                    assignCurCarLvlToList();
                    $scope.carLoading = false;
                    delay.reject(result);
                }
            );
        };
        return delay.promise;
    };

    function loadCurKitLevel() {
        var delay = $q.defer();

        $scope.kitLoading = true;
        assignCurKitLvlToList();

        if (angular.isArray($scope.kitList) && ($scope.kitList.length > 0)) {
            delay.resolve($scope.kitList);
            $scope.kitLoading = false;
        } else {
            var parm = makeKitParm();
            kitLevels[$scope.kitLevel].srvc(parm)
            .then(
                function (result) {
                    assignCurKitLvlToList(result);
                    //$scope.kitType = result.type;
                    $scope.kitLoading = false;
                    delay.resolve($scope.kitList);
                },
                function (result) {
                    assignCurKitLvlToList();
                    $scope.kitLoading = false;
                    delay.reject(result);
                }
            );
        };
        return delay.promise;
    };

    $scope.selectCar = function (item) {
        carLevels[$scope.carLevel].data = item;
        assignCarSelections();

        if ($scope.carLevel < (carLevels.length - 1)) {
            $scope.carLevel = $scope.carLevel + 1;
            loadCurCarLevel();
        }
    }

    $scope.selectKit = function (item) {
        kitLevels[$scope.kitLevel].data = item;

        if ($scope.kitLevel === 1) {
            finishKitLevels(item.id);
        }

        assignKitSelections();

        if ($scope.kitLevel < (kitLevels.length - 1)) {
            $scope.kitLevel = $scope.kitLevel + 1;
            loadCurKitLevel();
        }
    }

    $scope.setCarLevel = function (level) {
        if ((level < 0) || (level >= carLevels.length)) {
            return;
        }

        $scope.carLevel = level;
        carLevels[$scope.carLevel].data = undefined;

        for (var i = ($scope.carLevel + 1); i < carLevels.length; i++) {
            carLevels[i].data = null;
            carLevels[i].list = [];
        }

        loadCurCarLevel();
        assignCarSelections();
    }

    $scope.setKitLevel = function (level) {
        if ((level < 0) || (level >= kitLevels.length)) {
            return;
        }

        $scope.kitLevel = level;
        kitLevels[$scope.kitLevel].data = undefined;

        for (var i = ($scope.kitLevel + 1); i < kitLevels.length; i++) {
            kitLevels[i].data = null;
            kitLevels[i].list = [];
        }

        loadCurKitLevel();
        assignKitSelections();
    }

    $scope.prevCarLevel = function () {
        $scope.setCarLevel($scope.carLevel - 1);
    }
    $scope.prevKitLevel = function () {
        $scope.setKitLevel($scope.kitLevel - 1);
    }

    $scope.thisCarLevel = function (idx) {
        $scope.setCarLevel(idx + 1);
    }
    $scope.thisKitLevel = function (idx) {
        $scope.setKitLevel(idx + 1);
    }

    $scope.isCarSelected = function () {
        if ($scope.carLevel === (carLevels.length - 1)) {
            if (angular.isDefined(carLevels[carLevels.length - 1].data)) {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

    $scope.isKitSelected = function () {
        if ($scope.kitLevel === (kitLevels.length - 1)) {
            if (angular.isDefined(kitLevels[kitLevels.length - 1].data)) {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

    $scope.selectedCar = function () {
        if ($scope.isCarSelected()) {
            return carLevels[carLevels.length - 1].data;
        } else {
            return undefined;
        }
    }
    $scope.selectedKit = function () {
        if ($scope.isKitSelected()) {
            return kitLevels[kitLevels.length - 1].data;
        } else {
            return undefined;
        }
    }

    function assignCarSelections() {
        $scope.carSelections = [];

        var dta;
        for (var i = 0; i <= $scope.carLevel; i++) {
            dta = carLevels[i].data;
            if (dta) {
                $scope.carSelections.push({ display: dta.display });
            }
        }
    }
    function assignKitSelections() {
        $scope.kitSelections = [];

        var dta;
        for (var i = 0; i <= $scope.kitLevel; i++) {
            dta = kitLevels[i].data;
            if (dta) {
                $scope.kitSelections.push({ display: dta.display });
            }
        }
    }

    $scope.openCarDlg = function () {
        loadCurCarLevel().then(
            function (result) {
                $scope.CarSelOpen = true;
            }
        );
    };
    $scope.openKitDlg = function () {
        loadCurKitLevel().then(
            function (result) {
                $scope.KitSelOpen = true;
            }
        );
    };

    $scope.closeCarDlg = function () {
        $scope.CarSelOpen = false;
    }
    $scope.closeKitDlg = function () {
        $scope.KitSelOpen = false;
    }

    $scope.okCarDlg = function (result) {
        $scope.CarSelOpen = false;
        var car = $scope.selectedCar();
        if (car) {
            $scope.currentCar = car;
        } else {
            $scope.currentCar = undefined;
        }
    }
    $scope.okKitDlg = function (result) {
        $scope.KitSelOpen = false;
        var kit = $scope.selectedKit();
        var item = {};
        if (kit) {
            item.name = kit.name;
            item.qty = 1;
            item.color = kit.leacolorname;

            if (kitLevels[0].data) {
                item.description = kitLevels[0].data.descr;
                item.pattern = kitLevels[0].data.name;
            }

            var idx = $scope.lookupLineIdx;
            if (!angular.isNumber(idx)) {
                $scope.addLine();
                idx = $scope.lines.length - 1;
            }

            $scope.lines[idx] = item;

            $scope.lookupLineIdx = undefined;

        } else {

        }
    }

    $scope.cancelCarDlg = function (result) {
        $scope.CarSelOpen = false;
    }
    $scope.cancelKitDlg = function (result) {
        $scope.KitSelOpen = false;
    }

    $scope.dlgOpts = {
        backdropFade: true,
        keyboard: true,
        backdropClick: false,
        dialogFade: true
    }

    $scope.clearCar = function () {
        $scope.currentCar = undefined;
    }

    $scope.$watch('currentCar', function () {
        for (var i = 0; i < kitLevels.length; i++) {
            kitLevels[i].data = null;
            kitLevels[i].list = [];
        }
        $scope.kitLevel = 0;
        assignKitSelections();
    });

    $scope.addLine = function () {
        $scope.lines.push({ qty: 0 });
    }
    $scope.deleteLine = function (idx) {
        if (idx < $scope.lines.length) {
            $scope.lines.splice(idx, 1);
        }
    }
    $scope.clearLines = function () {
        $scope.lines = [];
    }

    $scope.lookupPattern = function (idx) {
        $scope.lookupLineIdx = idx;
        $scope.openKitDlg();
    }
    $scope.addKitForCar = function () {
        $scope.lookupLineIdx = undefined;
        $scope.openKitDlg();
    }

    $scope.carSelections = [];
    $scope.carLoading = false;
    $scope.clearCar();

    $scope.kitSelections = [];
    $scope.kitLoading = false;
    $scope.clearLines();

    $scope.CarSelOpen = false;
    $scope.KitSelOpen = false;

    loadCurCarLevel();
} ])

;
