'use strict';
/* Linkout Controllers */

linkout
.controller('LinkoutCtrl', ['$scope', '$location', '$http', 'ProductPrice', function ($scope, $location, $http, ProductPrice) {
    $scope.breadcrumbs = [];

    $scope.clearCrumbs = function () {
        $scope.breadcrumbs = [];
    }
    $scope.addCrumb = function (crumb) {
        if (!crumb) { return; }
        if (angular.isArray($scope.breadcrumbs)) {
            var idx = $.inArray(crumb, $scope.breadcrumbs);
            if (idx === -1) {
                $scope.breadcrumbs.push(crumb);
            }
        }
    }
    $scope.delCrumb = function (crumb) {
        if (!crumb) { return; }
        if (angular.isArray($scope.breadcrumbs) && ($scope.breadcrumbs.length > 0)) {
            var idx = $.inArray(crumb, $scope.breadcrumbs);
            if (idx > 0) {
                $scope.breadcrumbs.splice(idx, 1);
            }
        }
    }

    $scope.routeType = function (crumb) {
        $location.path('/type');
    }
    $scope.routeLeather = function (crumb) {
        $location.path('/leather');
    }
    $scope.routeHeaters = function (crumb) {
        $location.path('/heaters');
    }
    $scope.routeSchedule = function (crumb) {
        $location.path('/schedule');
    }
    $scope.routeConfirm = function (crumb) {
        $location.path('/confirm');
    }
    $scope.routeCheckout = function (crumb) {
        if ($scope.pickedProduct()) {
            $scope.routeConfirm();
        }
    }

    $scope.leather = {};
    $scope.heater = {};
    $scope.schedule = {};

    $scope.pickedLeather = function () {
        if (($scope.leather.leaCol) && ($scope.leather.leaCol.id) && ($scope.leather.ptrn)) {
            return true;
        } else {
            return false;
        }
    }


    $scope.pickedDriver = function () {
        if (($scope.heater) && ($scope.heater.driver)) {
            return true;
        } else {
            return false;
        }
    }
    $scope.pickedPassenger = function () {
        if (($scope.heater) && ($scope.heater.passenger)) {
            return true;
        } else {
            return false;
        }
    }
    $scope.pickedHeater = function () {
        if ($scope.pickedPassenger() || $scope.pickedDriver()) {
            return true;
        } else {
            return false;
        }
    }
    $scope.pickedProduct = function () {
        var h = $scope.pickedHeater();
        var l = $scope.pickedLeather();
        return h || l;
    }

    $scope.productCount = function () {
        var count = 0;
        if ($scope.heater) {
            if ($scope.heater.driver) {
                count = count + 1;
            }
            if ($scope.heater.passenger) {
                count = count + 1;
            }
        }
        if ($scope.pickedLeather()) {
            count = count + 1;
        }
        return count;
    }

    $scope.pickedSchedule = function () {
        if (
            ($scope.schedule)
            && ($scope.schedule.installer)
            && ($scope.schedule.installer.id)
        ) {
            return true;
        } else {
            return false;
        }
    }

    $scope.leatherPtrnDescr = function () {
        if ($scope.pickedLeather()) {
            return $scope.leather.ptrn.seldescr;
        } else {
            return null;
        }
    }

    $scope.driverDescr = 'Driver Side Seat Heater';
    $scope.passengerDescr = 'Passenger Side Seat Heater';

    ProductPrice.heater()
    .success(function (data, status, headers, config) {
        $scope.heaterPrice = data.Price;
    })
    .error(function (data, status, headers, config) {
        delete $scope.heaterPrice;
    });

    $scope.productPriceTotal = function () {
        var total = 0;
        if ($scope.heater) {
            if ($scope.heater.driver) {
                total = total + $scope.heaterPrice;
            }
            if ($scope.heater.passenger) {
                total = total + $scope.heaterPrice;
            }
        }
        if ($scope.pickedLeather()) {
            total = total + $scope.leather.ptrn.price;
        }
        return total;
    }


} ])
.controller('BreadCtrl', ['$scope', function ($scope) { } ])

.controller('TypeCtrl', ['$scope', function ($scope) {
    $scope.clearCrumbs();
    //$scope.addCrumb("Select Product Type");
} ])

.controller('CarouselCtrl', ['$scope', function ($scope) {
    $scope.myInterval = 5000;
    var slides = $scope.slides = [];

    slides.push({
        image: 'leather-interior.png',
        title: 'Leather Interiors',
        text: 'Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.'
    });

    slides.push({
        image: 'HeatersRatio.jpg',
        title: 'Driving Comfort',
        text: 'Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.'
    });
} ])

.controller('HeaterCtrl', ['$scope', function ($scope) {
    $scope.driver;
    $scope.passenger;

    $scope.toggleDriver = function () {
        $scope.driver = !$scope.driver;
    }

    $scope.togglePassenger = function () {
        $scope.passenger = !$scope.passenger;
    }

    function writeLocals() {
        $scope.heater.driver = $scope.driver;
        $scope.heater.passenger = $scope.passenger;
    }

    function readLocal(prop, deflt) {
        $scope[prop] = $scope.heater[prop] || deflt;
        //delete $scope.heater[prop];
    }

    function readLocals() {
        if ($scope.heater) {
            readLocal("driver", false);
            readLocal("passenger", false);
        }
    }

    $scope.schedInstall = function () {
        $scope.routeSchedule();
    }

    $scope.$on("$destroy", function () {
        writeLocals();
    });

    //setup();

    readLocals();

    $scope.clearCrumbs();
    $scope.addCrumb("Select Heater Option");
} ])

.controller('LeatherCtrl', ['$scope', '$location', 'Makes', 'Years', 'Models', 'Bodies', 'Trims', 'Cars', 'Ptrns', 'IntCols', 'RecCols', 'AllCols', 'ProductPrice',
    function ($scope, $location, Makes, Years, Models, Bodies, Trims, Cars, Ptrns, IntCols, RecCols, AllCols, ProductPrice) {

        function addAppProps(obj) {
            return $.extend(obj, {
                isLoading: obj.isLoading || false,
                slctId: obj.slctId || null,
                isSelecting: obj.isSelecting || false,
                isSelected: obj.isSelected || false,
                shouldFocus: obj.shouldFocus || false
            });
        }
        function makeIdName(id, name) {
            var obj = { id: id || null, name: name || null };
            return addAppProps(obj);
        }

        function loadList(srvc, parm, crumb, srcArr, trgArr, selProp, itemFcn, errFcn) {
            var slctProp = selProp + 'Idx';
            $scope[selProp] = makeIdName();
            $scope[slctProp] = null;


            if (angular.isArray($scope[trgArr])) {
                if ($scope[trgArr].length > 0) {
                    $scope[trgArr] = [];
                    $scope.delCrumb(crumb);
                }
            } else {
                $scope[trgArr] = [];
            }
            // exit if parm object has any falsy properties
            if (parm) {
                for (var key in parm) {
                    if (!parm[key]) {
                        return;
                    }
                }
            }
            $scope[selProp].isLoading = true;
            srvc.get(
                parm,
                function (data, status, headers, config) {
                    if ((data) && (data.success)) {
                        var idx = 0;
                        $scope[trgArr] = $.map(data[srcArr], function (item) {
                            idx = idx + 1;
                            if (itemFcn) {
                                item.slctId = idx;
                                return itemFcn(item);
                            } else {
                                if (item.name) {
                                    item.slctId = idx;
                                    return item;
                                }
                            }
                        });
                        if (angular.isArray($scope[trgArr])) {
                            if ($scope[trgArr].length > 0) {
                                if ($scope[trgArr].length === 1) {
                                    $scope[selProp] = $scope[trgArr][0];
                                    $scope[slctProp] = $scope[selProp].slctId;
                                } else {
                                    $scope[selProp].shouldFocus = true;
                                }
                                $scope.addCrumb(crumb);
                            }
                        }
                    } else {
                        $scope[trgArr] = [];
                    }
                    $scope[selProp].isLoading = false;
                },
                function (data, status, headers, config) {
                    if (errFcn) {
                        $scope[trgArr] = errFcn(data, status, headers, config);
                    } else {
                        $scope[trgArr] = [];
                    }
                    $scope[selProp].isLoading = false;
                }
            );
        }

        function getListItem(arr, idx) {
            if ((arr) && (idx >= 0) && (idx < arr.length)) {
                var item = arr[idx];
                return addAppProps(item);
            } else {
                return makeIdName();
            }
        }

        function makeIdParm(parms) {
            if (angular.isArray(parms)) {
                var parmObj = {};
                for (var i = 0; i < parms.length; i++) {
                    var root = parms[i];
                    var parm = root.toLowerCase() + 'id';
                    parmObj[parm] = null;
                    var selObj = $scope[root];
                    if (selObj) {
                        parmObj[parm] = selObj.id;
                    }
                }
                return parmObj;
            } else {
                return null;
            }
        }

        function selPropFixUp(selProp) {
            if (!$scope[selProp]) {
                $scope[selProp] = makeIdName();
            }
            if (!angular.isDefined($scope[selProp].isSelected)) {
                $scope[selProp] = addAppProps($scope[selProp]);
            }
            if (($scope[selProp].id) && (!$scope[selProp].isSelected)) {
                $scope[selProp].isSelected = true;
            }
        }

        function scltProp(selProp, list, id) {
            if ((angular.isNumber(id)) && (id > 0)) {
                var idx = id - 1;
                $scope[selProp] = getListItem($scope[list], idx);
            } else {
                $scope[selProp] = makeIdName();
            }
        }

        function makeColItem(item) {
            if (item.swatchimgurl) {
                item.colorurl = 'https://system.sandbox.netsuite.com' + item.swatchimgurl;
            } else {
                item.colorurl = '/Content/Images/img_not_avail.png';
            }
            return item;
        }

        var unWatchMake;
        var unWatchYear;
        var unWatchModel;
        var unWatchBody;
        var unWatchTrim;
        var unWatchCar;
        var unWatchPtrn;
        var unWatchPtrnFocus;
        var unWatchIntCol;
        //var unWatchColType;
        //var unWatchLeaCol;

        function enableWatches() {
            unWatchMake = $scope.$watch('makeIdx', function (newVal, oldVal) {
                if (newVal === oldVal) { return; };
                scltProp('make', 'makes', newVal)
                loadList(Years, makeIdParm(["make"]), "Year",
                'years', 'years', 'year', null, null);
            });
            unWatchYear = $scope.$watch('yearIdx', function (newVal, oldVal) {
                if (newVal === oldVal) { return; };
                scltProp('year', 'years', newVal);
                loadList(Models, makeIdParm(["make", "year"]), "Model",
                'models', 'models', 'model', null, null);
            });
            unWatchModel = $scope.$watch('modelIdx', function (newVal, oldVal) {
                if (newVal === oldVal) { return; };
                scltProp('model', 'models', newVal);
                loadList(Bodies, makeIdParm(["make", "year", "model"]), "Body",
                'bodies', 'bodys', 'body', null, null);
            });
            unWatchBody = $scope.$watch('bodyIdx', function (newVal, oldVal) {
                if (newVal === oldVal) { return; };
                scltProp('body', 'bodys', newVal);
                loadList(Trims, makeIdParm(["make", "year", "model", "body"]), "Trim",
                'trims', 'trims', 'trim', null, null);
            });
            unWatchTrim = $scope.$watch('trimIdx', function (newVal, oldVal) {
                if (newVal === oldVal) { return; };
                scltProp('trim', 'trims', newVal);
                loadList(Cars, makeIdParm(["make", "year", "model", "body", "trim"]), "Car",
                'cars', 'cars', 'car', null, null);
            });
            unWatchCar = $scope.$watch('carIdx', function (newVal, oldVal) {
                if (newVal === oldVal) { return; };
                scltProp('car', 'cars', newVal);
                loadList(Ptrns, makeIdParm(["car"]), "Pattern",
                    'patterns', 'ptrns', 'ptrn', null, null);
            });
            unWatchPtrn = $scope.$watch('ptrn', function (newVal, oldVal) {
                if (newVal === oldVal) { return; };
                $scope.ptrn.isSelecting = false;
                loadList(IntCols, makeIdParm(["car", "ptrn"]), "Match Interior",
                    'colors', 'intCols', 'intCol', null, null);

                if (($scope.ptrn) && ($scope.ptrn.rowsname)) {
                    ProductPrice.leather($scope.ptrn.rowsname)
                    .success(function (data, status, headers, config) {
                        $scope.ptrn.price = data.Price;
                    })
                    .error(function (data, status, headers, config) {
                        delete $scope.ptrn.price;
                    });
                }
            });
            unWatchPtrnFocus = $scope.$watch('ptrn.shouldFocus', function (newVal, oldVal) {
                if (newVal === oldVal) { return; };
                if (newVal) {
                    $scope.ptrn.isSelecting = true;
                }
            });
            unWatchIntCol = $scope.$watch('intColIdx', function (newVal, oldVal) {
                if (newVal === oldVal) { return; };
                scltProp('intCol', 'intCols', newVal);
                loadList(RecCols, makeIdParm(["car", "ptrn", "intCol"]), "Select Color",
                    'colors', 'leaCols', 'leaCol', makeColItem, null);
            });
        }

        function disableWatches() {
            if (unWatchMake) {
                unWatchMake();
            }
            if (unWatchYear) {
                unWatchYear();
            }
            if (unWatchModel) {
                unWatchModel();
            }
            if (unWatchBody) {
                unWatchBody();
            }
            if (unWatchTrim) {
                unWatchTrim();
            }
            if (unWatchCar) {
                unWatchCar();
            }
            if (unWatchPtrn) {
                unWatchPtrn();
            }
            if (unWatchPtrnFocus) {
                unWatchPtrnFocus();
            }
            if (unWatchIntCol) {
                unWatchIntCol();
            }
        }

        $scope.pickPtrn = function (index) {
            $scope.ptrn = getListItem($scope.ptrns, index);
        }
        $scope.pickColor = function (index) {
            $scope.leaCol = getListItem($scope.leaCols, index);
        }

        $scope.schedInstall = function () {
            $scope.routeSchedule();
        }

        function readLocal(prop) {
            if (($scope.leather) && ($scope.leather[prop])) {
                $scope[prop] = $scope.leather[prop];
                $scope[idxName(prop)] = $scope.leather[idxName(prop)];
                $scope[listName(prop)] = $scope.leather[listName(prop)];

                //delete $scope.leather[prop];
                //delete $scope.leather[idxName(prop)];
                //delete $scope.leather[listName(prop)];
            } else {
                setupNewProp(prop);
            }
        }
        function readLocals() {
            readLocal('make');
            readLocal("year");
            readLocal("model");
            readLocal("body");
            readLocal("trim");
            readLocal("car");
            readLocal("ptrn");
            readLocal("intCol");
            //readLocal("colType");
            readLocal("leaCol");
            if (($scope.leather) && ($scope.leather.savedCrumbs)) {
                for (var i = 0; i < $scope.leather.savedCrumbs.length; i++) {
                    $scope.addCrumb($scope.leather.savedCrumbs[i]);
                }
                delete $scope.leather.savedCrumbs;
            }
        }

        function writeLocal(prop) {
            $scope.leather[prop] = $scope[prop];
            $scope.leather[idxName(prop)] = $scope[idxName(prop)];
            $scope.leather[listName(prop)] = $scope[listName(prop)];
        }
        function writeLocals() {
            //$scope.make = makeIdName();
            if ($scope.leather) {
                writeLocal('make');
                writeLocal("year");
                writeLocal("model");
                writeLocal("body");
                writeLocal("trim");
                writeLocal("car");
                writeLocal("ptrn");
                writeLocal("intCol");
                //writeLocal("colType");
                writeLocal("leaCol");
                $scope.leather.savedCrumbs = $scope.breadcrumbs;
            }
        }
        function idxName(prop) {
            return prop + 'Idx';
        }
        function listName(prop) {
            return prop + 's';
        }
        function setupNewProp(prop) {
            $scope[prop] = makeIdName();
            $scope[idxName(prop)] = null;
            $scope[listName(prop)] = [];
        }
        function setup() {
            setupNewProp("make");
            setupNewProp("year");
            setupNewProp("model");
            setupNewProp("body");
            setupNewProp("trim");
            setupNewProp("car");
            setupNewProp("ptrn");
            setupNewProp("intCol");
            //setupNewProp("colType");
            setupNewProp("leaCol");
        }

        $scope.loadMakes = function () {
            loadList(Makes, null, "Make", 'makes', 'makes', 'make', null, null);
        }

        $scope.$on("$destroy", function () {
            writeLocals();
        });

        setup();

        $scope.clearCrumbs();

        readLocals();

        if (!angular.isArray($scope.breadcrumbs) || ($scope.breadcrumbs.length < 1)) {
            $scope.addCrumb("Select Leather");
        }

        enableWatches();

        if (!angular.isArray($scope.makes) || ($scope.makes.length < 1)) {
            $scope.loadMakes();
        }

    } ])

.controller('ScheduleCtrl', ['$scope', '$location', 'Installers',
    function ($scope, $location, Installers) {

        function foundInstId() {
            var found = false;
            if (angular.isArray($scope.installers) || ($scope.installers.length > 0) && ($scope.installer.id)) {
                for (var i = 0; i < $scope.installers.length; i++) {
                    if ($scope.installers[i].id == $scope.installer.id) {
                        found = true;
                        break;
                    };
                }
            }
            return found;
        }

        $scope.loadInstallers = function () {
            if ($scope.lastZipcode === $scope.zipcode) {
                return;
            }
            if (!$scope.zipcode) {
                delete $scope.installer.id;
                $scope.installers = [];
                return;
            }

            $scope.lastZipcode = $scope.zipcode;
            $scope.installer.isLoading = true;
            $scope.installers = Installers.query(
                { zipcode: $scope.zipcode },
                function (data, status, headers, config) {
                    if (!foundInstId()) {
                        delete $scope.installer.id;
                    }
                    $scope.installer.isLoading = false;
                },
                function (data, status, headers, config) {
                    $scope.installer.isLoading = false;
                    delete $scope.installer.id;
                },
                true
            );
        };

        $scope.installerPicked = function () {
            return (angular.isDefined($scope.installer.id));
        }

        function setup() {

            $scope.zipcode = null;
            $scope.lastZipcode = null;
            $scope.installer = {};
            $scope.schedDate = '';
            $scope.tod = "Either";
            $scope.installer = {};
            $scope.installer.isLoading = false;
            $scope.installers = [];
        }

        //$scope.updateDate = function (date) {        };

        function writeLocals() {
            $scope.schedule.zipcode = $scope.zipcode;
            $scope.schedule.installers = $scope.installers;
            $scope.schedule.installer = $scope.installer;
            $scope.schedule.schedDate = $scope.schedDate;
            $scope.schedule.note = $scope.note
            $scope.schedule.tod = $scope.tod;
            $scope.schedule.savedCrumbs = $scope.breadcrumbs;
        }

        function readLocal(prop, deflt) {
            $scope[prop] = $scope.schedule[prop] || deflt;
            delete $scope.schedule[prop];
        }

        function readLocals() {
            if ($scope.schedule) {
                readLocal("zipcode");
                readLocal("installers");
                readLocal("installer", {});
                readLocal("schedDate" || '');
                readLocal("note");
                readLocal("tod", "Either");

                if ($scope.schedule.savedCrumbs) {
                    for (var i = 0; i < $scope.schedule.savedCrumbs.length; i++) {
                        $scope.addCrumb($scope.schedule.savedCrumbs[i]);
                    }
                    delete $scope.schedule.savedCrumbs;
                }
            }
        }

        $scope.confirmOrder = function () {
            $scope.routeConfirm();
        }

        $scope.$watch('installer.id', function (newVal, oldVal) {

            if (($scope.installer) && angular.isDefined($scope.installer.id)) {
                if (angular.isArray($scope.installers) || ($scope.installers.length > 0)) {
                    for (var i = 0; i < $scope.installers.length; i++) {
                        if ($scope.installers[i].id == $scope.installer.id) {
                            $scope.installer.miles = $scope.installers[i].miles;
                            $scope.installer.descr = $scope.installers[i].descr;
                            break;
                        };
                    }
                }
            } else {
                $scope.installer = {};
            }
        });

        $scope.$on("$destroy", function () {
            writeLocals();
        });

        setup();

        $scope.clearCrumbs();

        readLocals();

        if (!angular.isArray($scope.breadcrumbs) || ($scope.breadcrumbs.length < 1)) {
            $scope.addCrumb("Select Product");
            $scope.addCrumb("Schedule Installation");
        }

    } ])
.controller('ConfirmCtrl', ['$scope', function ($scope) {
    var x = 1;
    $scope.clearCrumbs();
    $scope.addCrumb("Select Product");
    $scope.addCrumb("Schedule Installation");
    $scope.addCrumb("Confirm Your Order");
} ])
;
