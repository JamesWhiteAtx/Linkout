'use strict';
/* http://docs-next.angularjs.org/api/angular.module.ng.$compileProvider.directive */

angular.module('linkout.directives', [])
    .directive('selCarSelect', function () {
        return {
            restrict: 'EA',
            replace: true,
            transclude: true,
            scope: {
                secTitle: "@secTitle",
                secName: "@secName",
                secModel: "=secModel",
                slctModel: "=slctModel",
                secList: "=secList",
                secId: "@secId",
                secHide: "@secOnlyOpts"
            },
            template:
                '<div class="sel-select" ng-class="{selected: secModel.isSelected}" > ' +   // ng-show="!((secHide) && (secList.length < 2))" 

                    '<div ng-show="secModel.isLoading" class="loading">Loading {{secTitle}}...</div>' +
                    
                    '<select id="{{secId}}" title="{{secName}}"' +
                        'ng-disabled="secModel.isLoading || secList.length == 0" ' +
                        'ng-hide="(secModel.isLoading) || ((secHide) && (secList.length < 2))" ' +
                        'focus-it="secModel.shouldFocus" ' +
                        'ng-model="slctModel" ' +
                        'ng-options="i.slctId as i.name for i in secList">' +
                        '<option value="">-- Select {{secName}} --</option>' +
                    '</select>' +

                '</div>'
        }
    })
    .directive('focusIt', function ($timeout) {
        return {
            scope: false,
            link: function (scope, element, attrs) {
                var trigger = attrs['focusIt'];
                if (trigger) {
                    scope.$watch(trigger, function (value) {
                        if (value === true) {
                            element[0].focus();
                            scope.trigger = false;
                        }
                    });
                };
            }
        };
    })

    .directive('jqDatepicker', function () {
        return {
            // Enforce the angularJS default of restricting the directive to
            // attributes only
            restrict: 'A',
            // Always use along with an ng-model
            require: '?ngModel',
            // This method needs to be defined and passed in from the
            // passed in to the directive from the view controller
            scope: {
                select: '&'        // Bind the select function we refer to the right scope
            },
            link: function (scope, element, attrs, ngModel) {
                if (!ngModel) return;

                var optionsObj = {};

                optionsObj.dateFormat = 'mm/dd/yy';
                var updateModel = function (dateTxt) {
                    scope.$apply(function () {
                        // Call the internal AngularJS helper to
                        // update the two way binding
                        ngModel.$setViewValue(dateTxt);
                    });
                };

                optionsObj.onSelect = function (dateTxt, picker) {
                    updateModel(dateTxt);
                    if (scope.select) {
                        scope.$apply(function () {
                            scope.select({ date: dateTxt });
                        });
                    }
                };

                optionsObj.minDate = '+7d';

                ngModel.$render = function () {
                    // Use the AngularJS internal 'binding-specific' variable
                    element.datepicker('setDate', ngModel.$viewValue || '');
                };
                element.datepicker(optionsObj);
            }
        };
    })

;
