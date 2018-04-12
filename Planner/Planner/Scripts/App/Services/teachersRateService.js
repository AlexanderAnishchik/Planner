PlannerApp.service('teacherservice', function ($http) {

    this.getAllTeachersRate = function () {
        return $http.get("/Distribution/AllTeachersRate");
    }
    this.getFactTeachersRate = function () {
        return $http.get("/Distribution/FactTeachersRate");
    }
    this.getFactTeachersRateId = function (Id) {
        return $http.get("/Distribution/GetFactTeachersRateById?Id=" + Id);
    }

    this.getAllTeachers = function () {
        return $http.get("/Distribution/GetTeachers");
    }
    this.getAllRates = function () {
        return $http.get("/Distribution/GetRates");
    }
    this.currentTeachersRate = function () {
        return $http.get("/Distribution/CurrentTeachersRate");
    }


    this.saveTeachersRate = function (TeacRate) {
        var save = $http({
            method: 'POST',
            url: '/Distribution/AddTeachersRate/',
            data: TeacRate
        });
        return save;
    }

    this.editFactTeachersRate = function (TeacRate) {
        var save = $http({
            method: 'POST',
            url: '/Distribution/EditFactTeachersRate/',
            data: TeacRate
        });
        return save;
    }

});