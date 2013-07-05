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
.controller('CarsCtrl', ['$scope', function ($scope) { } ])

.controller('TestCtrl', ['$scope', 'ProductList', 'ProductDefn', function ($scope, ProductList, ProductDefn) { 
    $scope.val = 'no val';
    $scope.reVal = function()
    {
        $scope.val = ProductList.getListing();
    }
    $scope.clearListing = function()
    {
        ProductList.clearListing();
    }

    var x = ProductDefn.makeProdList()
    x.l1.description = 'one';

    var y = ProductDefn.makeProdList()
    y.l1.description = 'Why';

} ])

.controller('PricingCtrl', ['$scope', function ($scope) { 
    var makeProduct = function(typ) {
        return {
            type: typ,
            descirption: '',
            price: '',
            ccItemId: '',
            ccUrl: '',
            discount: function() {return 0;}
        };
    };
    var makePricer = function() {
        var self = {};
        
        self.l1 = makeProduct('1 Row Kit');
        self.l2 = makeProduct('2 Row Kit');
        self.l3 = makeProduct('3 Row Kit');

        self.h1 = makeProduct('1 Heater');
        self.h2 = makeProduct('2 Heaters');

        self.l1h1 = makeProduct(self.l1.type+' '+self.h1.type);
        self.l2h1 = makeProduct(self.l2.type+' '+self.h1.type);
        self.l3h1 = makeProduct(self.l3.type+' '+self.h1.type);

        self.l1h2 = makeProduct(self.l1.type+' '+self.h2.type);
        self.l2h2 = makeProduct(self.l2.type+' '+self.h2.type);
        self.l3h2 = makeProduct(self.l3.type+' '+self.h2.type);

        function safeNum(v) {
            var f = parseFloat(v);
            if (angular.isNumber(f)) {
                return f;
            } else {
                return 0;
            }
        }

        function discount(lp, hp, lhp) {
            return (safeNum(lp) + safeNum(hp)) - safeNum(lhp);
        }

        self.h2.discount = function() {
            return discount(self.h1.price, self.h1.price, self.h2.price);
        };

        self.l1h1.discount = function() {
            return discount(self.l1.price, self.h1.price, self.l1h1.price);
        };
        self.l1h2.discount = function() {
            return discount(self.l1.price, self.h2.price, self.l1h2.price);
        };

        self.l2h1.discount = function() {
            return discount(self.l2.price, self.h1.price, self.l2h1.price);
        };
        self.l2h2.discount = function() {
            return discount(self.l2.price, self.h2.price, self.l2h2.price);
        };

        self.l3h1.discount = function() {
            return discount(self.l3.price, self.h1.price, self.l3h1.price);
        };
        self.l3h2.discount = function() {
            return discount(self.l3.price, self.h2.price, self.l3h2.price);
        };

        self.prodList = [];

        self.prodList.push(self.h1);
        self.prodList.push(self.h2);

        self.prodList.push(self.l1);
        self.prodList.push(self.l1h1);
        self.prodList.push(self.l1h2);
        
        self.prodList.push(self.l2);
        self.prodList.push(self.l2h1);
        self.prodList.push(self.l2h2);

        self.prodList.push(self.l3);
        self.prodList.push(self.l3h1);
        self.prodList.push(self.l3h2);

        return self;
    };

    $scope.pricer = makePricer();

    $scope.pricer.l1.description = 'one';
    $scope.pricer.l1.price = 100;

    $scope.pricer.l2.description = 'two';
    $scope.pricer.l2.price = 200;

    $scope.pricer.l3.description = 'three';
    $scope.pricer.l3.price = 300;

    $scope.pricer.h1.description = 'h 1';
    $scope.pricer.h1.price = 50;

    $scope.pricer.h2.description = 'h 2';
    $scope.pricer.h2.price = 100;

} ])

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
