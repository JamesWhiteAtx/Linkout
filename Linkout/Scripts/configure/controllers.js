'use strict';
/* Configure Controllers */

configure
.controller('ConfigureCtrl', ['$scope', '$location', function ($scope, $location) {

    $scope.routeMenu = function () {
        $location.path('/menu');
    }
    $scope.routePricing = function () {
        $location.path('/pricing');
    }
    $scope.routeCars = function () {
        $location.path('/cars');
    }
    $scope.routeCarPatterns = function () {
        $location.path('/carPatterns');
    }

} ])
.controller('MenuCtrl', ['$scope', function ($scope) { } ])

.controller('PricingCtrl', ['$scope', 'ProductList', function ($scope, ProductList) {

    $scope.listing = {};

    $scope.loadListing = function () {
        ProductList.loadListing().then(function (listing) {
            $scope.listing = listing;
        }, function (err) {
            $scope.listing = null;
        });
    }

    $scope.clearListing = function () {
        ProductList.clearListing();
    }

    $scope.cacheListing = function () {
        ProductList.cacheListing($scope.listing);
    }

    $scope.loadListing();
} ])

.controller('CarsCtrl', ['$scope', function ($scope) {
} ])

.controller("TreeCtrl", ['$scope', 'TreeService', function ($scope, TreeService) {
    $scope.tree = TreeService.rootNode("node0");
    $scope.tree.loadKids();
    $scope.tree.expanded = true;

    $scope.nodeSelect = function (node) {
        $scope.selectNode = { type: node.type, name: node.name, details: [] };

        var d = node.data;
        var detail;
        for (var name in d) {
            if (d.hasOwnProperty(name) && (typeof d[name] !== "function")) {
                detail = {
                    name: name,
                    value: d[name]
                };
                if (name === 'makeid') {
                    detail.editurl = 'https://system.sandbox.netsuite.com/app/common/custom/custrecordentry.nl?rectype=19&e=T&id=' + d[name];
                }
                if (name === 'carid') {
                    detail.editurl = 'https://system.sandbox.netsuite.com/app/common/custom/custrecordentry.nl?rectype=63&e=T&id=' + d[name];
                }

                $scope.selectNode.details.push(detail);
            }
        };
    }
} ])

.controller("CarPatternsCtrl", ['$scope', 'MakeList', 'YearList', function ($scope, MakeList, YearList) {

    $scope.errs = [{ name: 'fistual', isLoading: true}];

    //    MakeList()
    //        .then(
    //            function (result) {
    //                var x = result.list;
    //                $scope.errs[0].isLoading = false;
    //            },
    //            function (err) {
    //                alter(err);
    //            }
    //        );

    MakeList()
        .then(function (result) {
            $.each(result.list, function (index, value) {
                var parm = { makeid: value.id };
                return YearList(parm);
            });

            //return asyncFn2(data)
        })
        .then(function (result) {
            var x = 'x';
        })
    ;

} ])

;
