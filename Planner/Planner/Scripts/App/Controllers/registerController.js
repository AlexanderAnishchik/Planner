/// <reference path="../App.js" />
/// <reference path="../../angular.js" />
PlannerApp.controller('registerController', ['$scope', '$http', 'rolesFactory', function ($scope, $http, rolesFactory) {
	var me = this;
	$scope.rolesFactory = rolesFactory;

	$scope.faculties = [];
	$scope.departments = [];
    $scope.AcademicTitleEnum = "0";
    $scope.DegreeEnum = "0";
    $scope.PositionEnum = "0";
    $scope.roles = [];
    
	me.init = function () {
		$http.get('/Faculty/Get').then(
            function (response) {
            	$scope.faculties = response.data;
            	if (window.userState) {
            		$scope.faculty = $scope.faculties.filter(function (fac) {
            			return fac.Id == userState.facultyId;
            		})[0];
            	}
            	
            }, function (response) {

            });
		$http.get('/Account/GetRoles').then(
            function (response) {
                $scope.roles = response.data;
            }, function (response) {

            });
	};

	$scope.$watch('faculty', function (newVal) {
		if (newVal) {

			$scope.departments = $scope.faculties.filter(function (element, index, array) {
				return element.Id == newVal.Id;
			})[0].Departments;

			if (window.userState && userState.departmentId)
				$scope.department = $scope.departments.filter(function (dep) {
					return dep.Id == userState.departmentId;
				})[0];
		}
		else {
			$scope.departments = null;
		}
	});
}]);