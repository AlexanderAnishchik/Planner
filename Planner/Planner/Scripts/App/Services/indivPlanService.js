PlannerApp.service('indivPlanService', function ($http) {
    var self = this;
    self.saveData = function (data) {
        var model = [];
        for (var el in data) {
            if (data[el] != null) {
                model.push({ SchemaName: el, Value: data[el].Result, PlannedValue: data[el].PlannedValue });
            }
        }
        $http.post('/IndividualPlan/SaveData', { model: model }).then(
            function (response) {
                window.location.reload();
            }, function (response) {

            });
    }
    self.getData = function (type, next) {
        $http.post('/IndividualPlan/GetDataByType', { type: type }).then(
          function (response) {
              console.log(response.data);
             return next(response.data);
          }, function (response) {
          });
    }

});