PlannerApp.controller('teachersRatesController', function ($scope, teacherservice,ngDialog) {

    var TeacRate = teacherservice.getAllTeachersRate();
    var FactTeacRate = teacherservice.getFactTeachersRate();
    var CurrentTeachersRate = teacherservice.currentTeachersRate();
    $scope.TeacRates = [];



    $scope.gridCurrentTeachersRate = {
        enableCellEditOnFocus: true,
        enableColumnResizing: true,
        enableFiltering: true,
        enableGridMenu: false,
        showGridFooter: false,
        showColumnFooter: false,
        rowIdentity: getRowId,
        getRowIdentity: getRowId,
        enablePaginationControls: true,

        paginationPageSizes: [10, 25, 50, 100],
        paginationPageSize: 10,
        importerDataAddCallback: function importerDataAddCallback(grid, newObjects) {
            $scope.myData = $scope.data.concat(newObjects);
        },
        columnDefs: [
          
            { field: 'UserName', displayName: 'Викладач', width: "40%"},
            { field: 'RateValue', displayName: 'Ставка' },
            { field: 'Current', displayName: 'Поточні' },
            { field: 'Total', displayName: 'Усього' },

        ],
        onRegisterApi: function onRegisterApi(registeredApi) {
            $scope.gridApi = registeredApi;
        }
    };


    $scope.gridOptions = {
        enableCellEditOnFocus: true,
        enableColumnResizing: true,
        enableFiltering: true,
        enableGridMenu: false,
        showGridFooter: false,
        showColumnFooter: false,
        rowIdentity: getRowId,
        getRowIdentity: getRowId,
        enablePaginationControls: true,

        paginationPageSizes: [10, 25, 50, 100],
        paginationPageSize: 10,
        importerDataAddCallback: function importerDataAddCallback(grid, newObjects) {
            $scope.myData = $scope.data.concat(newObjects);
        },
        columnDefs: [
          
            { field: 'UserName', displayName: 'Викладач' },
            { field: 'RateValue',  displayName: 'Ставка' },
            { field: 'Total',  displayName: 'Часи' },

        ],
        onRegisterApi: function onRegisterApi(registeredApi) {
            $scope.gridApi = registeredApi;
        }
    };
    //$scope.gridOptions.multiSelect = false;
    $scope.gridFactOptions = {
        enableCellEditOnFocus: true,
        enableColumnResizing: true,
        enableFiltering: true,
        enableGridMenu: false,
        showGridFooter: false,
        showColumnFooter: false,
        rowIdentity: getRowId,
        getRowIdentity: getRowId,
        enablePaginationControls: true,

        paginationPageSizes: [10, 25, 50, 100],
        paginationPageSize: 10,
        importerDataAddCallback: function importerDataAddCallback(grid, newObjects) {
            $scope.myData = $scope.data.concat(newObjects);
        },
        columnDefs: [

            { field: 'UserName', displayName: 'Викладач' },
            { field: 'RateValue', displayName: 'Ставка' },
            { field: 'Total', displayName: 'Часи' },
                       {
                           field: 'Дії', enableFiltering: false, enableSort: false,
                           cellTemplate: ' <a href="" ng-click="grid.appScope.openEditDialog(); grid.appScope.getFactTeacRateById(row.entity)" class="btn btn-default">Редагувати</a>'
                       }
        ],
        onRegisterApi: function onRegisterApi(registeredApi) {
            $scope.gridApi = registeredApi;
        }
    };


    function getRowId(row) {

        return row.Id;

    }


     loadCurrentTeachersRate();
    function loadCurrentTeachersRate() {
        var TeachersRate = teacherservice.currentTeachersRate();
        TeachersRate.then(function (data) {
            
            $scope.CurrentTeachersRate = data.data;
            $scope.gridCurrentTeachersRate.data = $scope.CurrentTeachersRate;
            
        },
            function () {
                console("Oops..", "Error occured while loading", "error");
            });
    }

    loadTeachersRate();
    function loadTeachersRate() {
        var TeachersRate = teacherservice.getAllTeachersRate();
        TeachersRate.then(function (data) {
            
            $scope.TeacherRate = data.data;
            $scope.gridOptions.data = $scope.TeacherRate;
            
        },
            function () {
                console("Oops..", "Error occured while loading", "error");
            });
    }


    loadTeachers();
    function loadTeachers() {
        var teach = teacherservice.getAllTeachers();
        teach.then(function (data) {
            $scope.teachers = data.data;
        },
            function () {
                console("Oops..", "Error occured while loading", "error");
            });
    }
    
    loadRates();
    function loadRates() {
        var rate = teacherservice.getAllRates();
        rate.then(function (data) {
            $scope.rates = data.data;
        },
            function () {
                console("Oops..", "Error occured while loading", "error");
            });
    }

    loadFactTeachersRate();
    function loadFactTeachersRate() {
        var TeachersRate = teacherservice.getFactTeachersRate();
        TeachersRate.then(function (data) {

            $scope.TeacherFactRate = data.data;
            $scope.gridFactOptions.data = $scope.TeacherFactRate;

        },
            function () {
                console("Oops..", "Error occured while loading", "error");
            });
    }

    var TeacRate =
   {
       Id: $scope.Id,
       RateId: $scope.RateId,
       UserId: $scope.UserId,
       Total: $scope.Total
   };
    //open create dialog
    $scope.showAlertDetails = function () {
     
        ngDialog.openConfirm({
            template: 'addWarh',
            controller: 'teachersRatesController',
            className: 'ngdialog-theme-default',
            scope: $scope,
            closeByNavigation: true,
        }).then(function (TeacRate) {
            var saveTeacRate = teacherservice.saveTeachersRate(TeacRate);
            saveTeacRate.then(function (d) {
                loadTeachersRate();
                loadFactTeachersRate();
            });
        });
    }

      $scope.openEditDialog = function () {
        ngDialog.openConfirm({
            template: 'getProd',
            controller: 'teachersRatesController',
            className: 'ngdialog-theme-default',
            scope: $scope,
            closeByNavigation: true,
        }).then(function (d) {

            var TeacRate =
            {
            Id: $scope.TeacRates.Id,
            //RateId: $scope.RateId,
            //UserId: $scope.UserId,
            Total: $scope.TeacRates.Total
            };
            var updaterecords = teacherservice.editFactTeachersRate(TeacRate);
            updaterecords.then(function (d) {
                 loadFactTeachersRate();
            },
                function () {
                    console("Oops..", "Error occured while loading", "error");
                });
        });
    }

      $scope.getFactTeacRateById = function (FactTeacRate) {
          var singlerecord = teacherservice.getFactTeachersRateId(FactTeacRate.Id);
          singlerecord.then(function (d) {
              $scope.TeacRates = d.data;
          },
              function () {
                  console("Oops...", "Error occured while getting record", "error");
              });
      }

    //close dialog window
    $scope.closeAll = function () {
        ngDialog.closeAll();
    };

});