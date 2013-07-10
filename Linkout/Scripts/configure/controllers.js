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
    $scope.imthecarscontroller = 'imthecarscontroller';
} ])
.controller("TreeCtrl", ['$scope', 'TreeService', function ($scope, TreeService) {

    $scope.tree = TreeService.rootNode("node0");
    $scope.tree.loadKids();
    //$scope.tree.addKid("node01");
    //$scope.tree.addKid("node02")
    //$scope.tree.addKid("node03")
    $scope.tree.expanded = true;
} ])

//.controller("TreeController", ['$scope', function($scope) {

//    $scope.delete = function(data) {
//        data.nodes = [];
//    };

//    $scope.add = function(data) {
//        var post = data.nodes.length + 1;
//        var newName = data.name + '-' + post;
//        data.nodes.push({name: newName, nodes: []});
//    };


//    $scope.tree = [{name: "Node", nodes: []}];
//}])

;
