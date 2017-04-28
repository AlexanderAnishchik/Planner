/// <reference path="../App.js" />
/// <reference path="../../angular.js" />
PlannerApp.controller('getUsersController',['$scope', '$http', function($scope, $http){
    var me = this;

    me.ToggleActive = ToggleActive;
	me.users = [];
	$scope.searchName = "";

	me.init = function () {
		$http.get('/Account/GetUsersData')
		.then(function (res) {
		    me.users = res.data.UserList;
			
		}, function (err) {
			console.log(err);
		});
	};

	function ToggleActive(user) {
	    $http.post("/Account/ToggleActive?userName=" + user.Email).then(successFunc, errorFunc);

	    function successFunc(response) {
	        if (response.data.success)
	            user.IsActive = !user.IsActive;
	        else
	            console.log(response.data.message);
	    }

	    function errorFunc(response) {
	        console.log(response);
	    }
	}
}]);