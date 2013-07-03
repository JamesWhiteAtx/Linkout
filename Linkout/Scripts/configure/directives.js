'use strict';
/* Configure Directives */
/* http://docs-next.angularjs.org/api/angular.module.ng.$compileProvider.directive */

angular.module('configure.directives', [])
    .directive('ccProdEditor', function () {
        return {
            restrict: 'EA',
            replace: true,
            transclude: true,
            scope: {
                prodModel: "=prodModel",
                prodName: "@prodName",
                discName: "@discName"
            },
            template:
                '<form name="form">'+

                    '<strong>{{prodModel.type}}</strong>' +
                    '<div class="row">'+
                        '<div class="span1">Description:</div>'+ 
                        '<div class="span5"> <input type="text" ng-model="prodModel.description" class="span5" placeholder="Descripton" /> </div>'+ 
                    '</div>'+
                    '<div class="row">'+
                        '<div class="span1">Price:</div>'+ 
                        '<div class="span5">'+
                            '<input type="text" name="price" id="price" class="span2" ng-model="prodModel.price" ng-pattern="/^\\d+\\.?\\d*$/" placeholder="Price"/>'+
                            '<span class="text-warning" ng-show="form.price.$error.pattern"> Enter a valid number </span>'+
                            '<span class="text-warning" ng-show="form.$error.required"> Enter a number </span>'+
                        '</div>'+
                    '</div>' +

                '</form>'
        }
    })