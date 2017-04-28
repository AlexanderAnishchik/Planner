PlannerApp.controller('tabController',
[
    '$scope',
    function ($scope) {
        var self = this;

        $scope.tabControllerTestTrash = "TAB IS WORKING!";
        $scope.tab = 1;
        self.setTab = function(newTab) {
            $scope.tab = newTab;
        };

        self.isSet = function(chosenTab) {
            return $scope.tab === chosenTab;
        };
    }
]);