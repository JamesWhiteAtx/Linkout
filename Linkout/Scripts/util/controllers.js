'use strict';
/* util Controllers */

util
.controller('UtilCtrl', ['$scope', '$location', function ($scope, $location) {

    $scope.routeMenu = function () {
        $location.path('/menu');
    }

} ])

.controller('MenuCtrl', ['$scope', function ($scope) { 
} ])

.controller('ItemCarsCtrl', ['$scope', '$location', function ($scope, $location) {
    $scope.id = $location.search().id;

    $scope.myData = [{ pellets: "Moroni", age: 50 },
                     { name: "Tiancum", age: 43 },
                     { name: "Jacob", age: 27 },
                     { name: "Nephi", age: 29 },
                     { gopher: "Nephi", beans: 29 },
                     { name: "Enos", age: 34}];

    $scope.gridOptions = { data: 'myData' };
} ])

;
