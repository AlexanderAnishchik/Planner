/// <reference path="../App.js" />
/// <reference path="../../angular.js" />
PlannerApp.controller('individualPlanController', ['$scope', 'indivPlanService', function ($scope, indivPlanService) {
    var me = this;
    me.type = "";
    $scope.data = [];
    $scope.model = {};
    $scope.currentTab = "";
    me.init = function (type) {
        me.type = type.Name;
        console.log(type);
        $scope.getData();
    }
    $scope.checkTab = function (value) {
        return $scope.currentTab == value;
    }
    $scope.changeTab = function (value) {
        $scope.currentTab = value;
    }
    $scope.getData = function () {
        indivPlanService.getData(me.type, function (result) {
            $scope.data = result;
            $scope.model = {};
            $scope.data.forEach(function (type) {
                type.Fields.forEach(function (el) {
                    if (el.Result)
                        $scope.model[el.SchemaName] = el.Result;
                });
            });
            $scope.currentTab = $scope.data[0].TabName;
        });
    }
    $scope.saveData = function () {
        indivPlanService.saveData($scope.model);
    }

}]);
