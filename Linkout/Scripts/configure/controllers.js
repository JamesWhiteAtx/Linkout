'use strict';
/* Configure Controllers */

configure
.controller('ConfigureCtrl', ['$scope', '$location', function ($scope, $location) {

    $scope.routeMenu = function () {
        $location.path('/menu');
    }
    $scope.routeCars = function () {
        $location.path('/cars');
    }

} ])
.controller('MenuCtrl', ['$scope', function ($scope) { } ])
.controller('CarsCtrl', ['$scope', function ($scope) { } ])

.controller("TreeCtrl", ['$scope', function($scope) {

    $scope.makeNode = function(name, kids) {
        var node = {}
        
        //Public property    
        node.name = name;
        node.kids = kids || [];
        node.parentNode = null;
        node.expanded = false;
 
        //Public methods
        node.addKid = function(name, kids) {
            var newKid = $scope.makeNode(name, kids);
            node.kids.push(newKid);
            newKid.parentNode = node;
            return newKid;
        };
        node.clearKids = function() {
            node.nodes = [];
        };
 
        node.hasKids = function() {
            return ( angular.isArray(node.kids)  && (node.kids.length > 0) );
        };

        node.canExpand = function() {
            return (node.hasKids() && (!node.expanded));
        }
        node.canCollapse = function() {
            return (node.hasKids() && node.expanded);
        }

        node.expand = function() {
            node.expanded = true;
        }
        node.collapse = function() {
            node.expanded = false;
        }

        node.showing = function() {
            if (node.parentNode) {
                return ((node.parentNode.expanded) && (node.parentNode.showing()));
            } else {
                return true;
            }
        };
 
        //Return just the public parts
        return node;
    };

    $scope.tree = $scope.makeNode("node0");
    $scope.tree.expanded = true;
    $scope.tree.addKid("node01");
    var n2 = $scope.tree.addKid("node02")
    n2.addKid("node021");
    var n3 = n2.addKid("node022");
    n3.addKid("node0221");
    $scope.tree.addKid("node03");

}])

.controller("TreeController", ['$scope', function($scope) {

    $scope.delete = function(data) {
        data.nodes = [];
    };

    $scope.add = function(data) {
        var post = data.nodes.length + 1;
        var newName = data.name + '-' + post;
        data.nodes.push({name: newName, nodes: []});
    };


    $scope.tree = [{name: "Node", nodes: []}];
}])

;
