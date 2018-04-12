PlannerApp.controller('distributionController', ['$scope', '$http', 'ngDialog', function ($scope, $http, ngDialog) {
    var me = this;

    $scope.entryLoadings = [];
    $scope.dayEntries = [];
    $scope.extramuralEntries = [];
    $scope.uniWorkers = [];
    $scope.extra = false;
    $scope.Detail = [];
    $scope.teachers = [];
    $scope.SuccessfulResult = '';
    $scope.semesters = [
        { Caption: "Перший семестр", Value: 1 },
        { Caption: "Другий семестр", Value: 2 }];



    me.init = function () {
        $http.get('/Distribution/GetLoadings').then(
                function (response) {
                    $scope.entryLoadings = response.data;
                },
                function (response) {
                    console.log(' UNABLE TO GET DATA FROM SERVER! ' + response.statusText);
                });
    };

    me.teachertLoad = function () {
        $http
            .get(`/Distribution/GetTeachers`)
            .then(
            function (response) {
                $scope.teachers = response.data
            },
            function (response) {
                console.log(' UNABLE TO GET DATA FROM SERVER! ' + response.statusText);
            });
    };

    $scope.fillDayEntryLoad = function () {
        $http
            .get(`/Distribution/GetDayEntryData?loadingId=${$scope.selectedDayEntryLoading}&semester=${$scope.selectedDaySemester}`)
            .then(function (response) {
                $scope.dayEntries = response.data;
                $scope.gridOptions.data = $scope.dayEntries;
                console.log('Data has been received');
            },
                function (response) {
                    console.log(' Unable to load dayEntryData: ' + error.message);
                });
    };


    //var loadTeachersRate = $http
    //          .get(`/Distribution/GetDayEntryData?loadingId=${$scope.selectedDayEntryLoading}&semester=${$scope.selectedDaySemester}`);

    $scope.gridOptions = {
        //enableCellEditOnFocus: true,
        //enableColumnResizing: true,
        enableFiltering: true,
        //enableGridMenu: false,
        //showGridFooter: false,
        //showColumnFooter: false,
        rowIdentity: getRowId,
        getRowIdentity: getRowId,
        enablePaginationControls: true,

        paginationPageSizes: [10, 25, 50, 100],
        paginationPageSize: 10,
        importerDataAddCallback: function importerDataAddCallback(grid, newObjects) {
            $scope.myData = $scope.data.concat(newObjects);
        },
        columnDefs: [
            { field: 'Faculty', displayName: 'Факультет', width: 110 },
            { field: 'Specialty', displayName: 'Спеціальність', width: 130 },
            { field: 'Specialization', displayName: 'Спеціалізація', width: 130 },
            { field: 'Course', displayName: 'Курс', width: 130 },
            { field: 'EducationDegree', displayName: 'Навчальний ступінь', width: 160 },
            { field: 'StudentsCount', displayName: 'Кількість студентів', width: 170 },
            { field: 'ForeignersCount', displayName: 'К-ть іноземців', width: 140 },
            { field: 'GroupsCipher', displayName: 'Шифр груп', width: 130 },
            { field: 'QuantityOfGroupsA', displayName: 'К-ть підгруп (17)', width: 150  },
            { field: 'RealQuantityOfGroups', displayName: 'Реальна к-ть студентів', width: 180 },
            { field: 'QuantityOfGroupsB', displayName: 'К-ть підгруп', width: 140 },
            { field: 'QuantityOfThreads', displayName: 'К-ть потоків', width: 130 },
            { field: 'ConflatedThreads', displayName: 'Об`єднання потоків', width: 180 },
            { field: 'Notes', displayName: 'Примітки', width: 130 },
            { field: 'Subject', displayName: 'Дисципліна', width: 130  },
            { field: 'QuantityOfCredits', displayName: 'К-ть кредитів ЄКТС', width: 170 },
            { field: 'Hours', displayName: 'К-ть годин', width: 130 },
            { field: 'Language', displayName: 'Мова викладання', width: 160 },
            { field: 'QuantityOfWeeksFs', displayName: 'К-ть тижнів (1 сем.)', width: 190 },
            { field: 'QuantityOfWeeksSs', displayName: 'К-ть тижнів (2 сем.)', width: 190 },
            { field: 'CoeficientFs', displayName: 'Коефіцієнт (1 сем.)', width: 190 },
            { field: 'CoeficientSs', displayName: 'Коефіцієнт (2 сем.)', width: 190 },
            { field: 'DepartmentCipher', displayName: 'Департамент', width: 130 },
            { field: 'Projects', displayName: 'КР, КП, ДР', width: 130 },
            { field: 'Practices', displayName: 'Практика', width: 130 },
            { field: 'QuantityOfMembers', displayName: 'К-ть членів ДЕК', width: 140 },
            { field: 'DeS.TotalHours', displayName: 'Всього годин', width: 130 },
            { field: 'DeS.Total', displayName: 'Всього', width: 130  },
            { field: 'DeS.Lectures', displayName: 'Лекції', width: 130 },
            { field: 'DeS.Labs', displayName: 'Лабораторні', width: 130 },
            { field: 'DeS.Practices', displayName: 'Практичні', width: 130 },
            { field: 'DeS.IndependentWorks', displayName: 'Сам. Робота', width: 130 },
            { field: 'DeS.Courses', displayName: 'КР(КП), ДР', width: 130  },
            { field: 'DeS.Exam', displayName: 'Екзамен', width: 130 },
            { field: 'DeS.Evaluation', displayName: 'Залік', width: 130 },
            { field: 'DaySemesterId', displayName: 'DaySemesterId', width: 130, visible: false },
            { field: 'Dd.Semester', displayName: 'Семестр', width: 130 },
            { field: 'Dd.Lecture', displayName: 'Лекції', width: 130 },
            { field: 'Dd.Practice', displayName: 'Практичні', width: 130 },
            { field: 'Dd.Lab', displayName: 'Лабораторні', width: 130 },
            { field: 'Dd.ConsultInSemester', displayName: 'Семестрові консультації', width: 190 },
            { field: 'Dd.ConsultForExam', displayName: 'Екзаменаційні консультації', width: 190 },
            { field: 'Dd.VerifyingOfTests', displayName: 'Перевірка контр. робіт', width: 190  },
            { field: 'Dd.KR_KP', displayName: 'КР, КП', width: 130  },
            { field: 'Dd.ControlEvaluation', displayName: 'Проведення заліку', width: 170 },
            { field: 'Dd.ControlExam', displayName: 'Проведення екзамену', width: 180 },
            { field: 'Dd.PracticePreparation', displayName: 'Керівництво пр. Підготовкою', width: 200 },
            { field: 'Dd.Dek', displayName: 'Участь у ДЕК', width: 130 },
            { field: 'Dd.StateExam', displayName: 'Проведення держ. екзамену', width: 200 },
            { field: 'Dd.ManagedDiploma', displayName: 'Керівництво дипломними роботами', width: 220 },
            { field: 'Dd.Other', displayName: 'Інше', width: 130 },
            { field: 'Dd.Total', displayName: 'Всього за семестр', width: 160 },
            { field: 'Dd.Active', displayName: 'АКТИВНІ', width: 130 },
            { field: 'Dd.EnglishBonus', displayName: 'Бонус', width: 130 },
            { field: 'DayEntryId', displayName: 'DayEntryId', width: 130, visible: false },
            //{ field: 'Faculty', displayName: 'Факультет',width:250 },
              {
                  field: 'Дії', enableFiltering: false, enableSort: false, width:70, pinnedLeft: true,
                  cellTemplate: ' <a href="" ng-click="grid.appScope.showdetailDayEntryLoad(); grid.appScope.detailDayEntryLoad(row.entity)" class="btn btn-default"><span class="glyphicon glyphicon-pencil"></span></a>'
              }
            //{ field: 'RateValue', displayName: 'Ставка' },
            //{ field: 'Total', displayName: 'Часи' },

        ],
        onRegisterApi: function onRegisterApi(registeredApi) {
            $scope.gridApi = registeredApi;
        }
    };

    $scope.extramularGridOptions = {
        enableFiltering: true,
        rowIdentity: getRowId,
        getRowIdentity: getRowId,
        enablePaginationControls: true,

        paginationPageSizes: [10, 25, 50, 100],
        paginationPageSize: 10,
        importerDataAddCallback: function importerDataAddCallback(grid, newObjects) {
            $scope.myData = $scope.data.concat(newObjects);
        },
        columnDefs: [
            { field: 'Ed.Semester', displayName: 'Семестр', width: 110 },
            { field: 'Subject', displayName: 'Дисципліна', width: 130 },
            { field: 'Specialty', displayName: 'Спеціальність', width: 130 },
            { field: 'Extramural', displayName: 'НКП', width: 130 },
            { field: 'Course', displayName: 'Курс', width: 160 },
            { field: 'QuantityOfStudents', displayName: 'Кількість студентів', width: 170 },
            { field: 'QuantityOfGroups', displayName: 'К-ть груп', width: 140 },
            { field: 'QuantityOfThreads', displayName: 'Номер потока', width: 130 },
            { field: 'MajorSpecialty', displayName: 'Осн.спеціальність', width: 150 },
            { field: 'CommonTime', displayName: 'Заг.обсяг годин', width: 180 },
            { field: 'Credits', displayName: 'Кредити', width: 140 },
            { field: 'EeS.Lectures', displayName: 'Лекції', width: 130 },
            { field: 'EeS.Practices', displayName: 'Практичні(вхідне навантаження)', width: 180 },
            { field: 'EeS.Labs', displayName: 'Лабораторні(вхідне навантаження)', width: 130 },
            { field: 'EeS.IndependentWorks', displayName: 'Інд.робота(вхідне навантаження)', width: 130 },
            { field: 'EeS.Exam', displayName: 'Екзамен(вхідне навантаження)', width: 170 },
            { field: 'EeS.Evaluation', displayName: 'Залік(вхідне навантаження)', width: 130 },
            { field: 'EeS.Projects', displayName: 'КР КП ДР ДП(вхідне навантаження)', width: 160 },
            { field: 'EeS.Test', displayName: 'Контр.робота(вхідне навантаження)', width: 190 },
            { field: 'EeS.LimitOnProjects', displayName: 'Норма на КР ДР ДІ(вхідне навантаження)', width: 190 },
            { field: 'Ed.Lectures', displayName: 'Лекції', width: 190 },
            { field: 'Ed.Practices', displayName: 'Практичні', width: 190 },
            { field: 'Ed.Labs', displayName: 'Лабораторні', width: 130 },
            { field: 'Ed.SemesterConsults', displayName: 'Семестрові консультації', width: 130 },
            { field: 'Ed.ExamConsults', displayName: 'Екзаменаційні консультації', width: 130 },
            { field: 'Ed.AnalyticalWorks', displayName: 'Аналітичні огляди(реферати)', width: 140 },
            { field: 'Ed.WrittenWorks', displayName: 'Розрахункові роботи', width: 130 },
            { field: 'Ed.Projects', displayName: 'КР КП', width: 130 },
            { field: 'Ed.Evaluation', displayName: 'Залік', width: 130 },
            { field: 'Ed.OralExams', displayName: 'Іспити(усні)', width: 130 },
            { field: 'Ed.WrittenExams', displayName: 'Іспити(письмові)', width: 130 },
            { field: 'Ed.CheckingTests', displayName: 'Перевірка контр.робіт', width: 130 },
            { field: 'Ed.DiplomManagement', displayName: 'Керівництво переддипломною практикою', width: 130 },
            { field: 'Ed.DekParticipation', displayName: 'Участь в ДЕК', width: 130 },
            { field: 'Ed.CheckWriteWorks', displayName: 'Перевірка письмових робіт', width: 130 },
            { field: 'Ed.Protection', displayName: 'Захист', width: 130 },
            { field: 'Ed.Total', displayName: 'Всього за семестр', width: 130 },
            { field: 'Ed.Active', displayName: 'Активні', width: 130 },
              {
                  field: 'Дії', enableFiltering: false, enableSort: false, width: 70, pinnedLeft: true,
                  cellTemplate: ' <a href="" ng-click="grid.appScope.showdetailExtremularEntryLoad(); grid.appScope.detailExtramuralEntryLoad(row.entity)" class="btn btn-default"><span class="glyphicon glyphicon-pencil"></span></a>'
              }
        ],
        onRegisterApi: function onRegisterApi(registeredApi) {
            $scope.gridApi = registeredApi;
        }
    };

    function getRowId(row) {

        return row.Id;

    }

    $scope.showdetailDayEntryLoad = function () {
        ngDialog.openConfirm({
            template: 'dayEntryLoad',
            controller: 'distributionController',
            className: 'ngdialog-theme-default',
            scope: $scope,
            closeByNavigation: true,
        }).then(function (dE) {

            var DaySemester = {
                DaySemesterId: $scope.DaySemesterId,
                Active: $scope.Detail.Active,
                ConsultForExam: $scope.Detail.ConsultForExam,
                ConsultInSemester: $scope.Detail.ConsultInSemester,
                ControlEvaluation: $scope.Detail.ControlEvaluation,
                ControlExam: $scope.Detail.ControlExam,
                DayEntryLoadId: $scope.Detail.DayEntryLoadId,
                Dek: $scope.Detail.Dek,
                EnglishBonus: $scope.Detail.EnglishBonus,
                KR_KP: $scope.Detail.KR_KP,
                Lab: $scope.Detail.Lab,
                Lecture: $scope.Detail.Lecture,
                ManagedDiploma: $scope.ManagedDiploma,
                Other: $scope.Detail.Other,
                Practice: $scope.Detail.Practice,
                PracticePreparation: $scope.Detail.PracticePreparation,
                StateExam: $scope.Detail.StateExam,
                Total: $scope.Detail.Total,
                VerifyingOfTests: $scope.VerifyingOfTests,
                Semester: $scope.Detail.Dd.Semester
            };

            var request = {
                method: "POST",
                url: "/Distribution/EditDetailWorkersDistributed/",
                data: DaySemester,
                params: { Id: $scope.DaySemesterId, userId: $scope.Detail.selectedTeachEntryLoading, DayEntryId: $scope.Detail.DayEntryId, semestr: $scope.Detail.Dd.Semester }
            }
            $http(request)
                .then(function (response) {
                    //$scope.detailDayEntryLoad(dE);
                    //$scope.clear();
                    $scope.SuccessfulResult = 'Данні успішно оновлені!';
                    //$scope.gridOptions;
                });
            //.then(function (TeacRate) {
            //var saveTeacRate = teacherservice.saveTeachersRate(TeacRate);
            //saveTeacRate.then(function (d) {
            //    //loadTeachersRate();
            //    $scope.gridOptions;
            //});
            //});
        });
    }

    //loadTeachersRate();
    //function loadTeachersRate() {
    //    var TeachersRate = $http
    //        .get(`/Distribution/GetDayEntryData?loadingId=${$scope.selectedDayEntryLoading}&semester=${$scope.selectedDaySemester}`);
    //    TeachersRate.then(function (data) {

    //        $scope.TeacherRate = data.Data;
    //        $scope.gridOptions.data = $scope.TeacherRate;

    //    },
    //        function () {
    //            console("Oops..", "Error occured while loading", "error");
    //        });
    //}

    $scope.showdetailExtremularEntryLoad = function () {
        ngDialog.openConfirm({
            template: 'extrEntryLoad',
            controller: 'distributionController',
            className: 'ngdialog-theme-default',
            scope: $scope,
            closeByNavigation: true,
        }).then(function (dE) {

            var ExtramuralSemester = {
                ExtramuralSemesterId: $scope.DetailExtramural.ExtramuralSemesterId,
                Lecture: $scope.DetailExtramural.ExtramuralLecture, //+
                Practice: $scope.DetailExtramural.ExtramuralPractice, //+
                Lab: $scope.DetailExtramural.ExtramuralLab, //+
                ConsultInSemester: $scope.DetailExtramural.ExtramuralConsultInSemester, //+
                ConsultForExam: $scope.DetailExtramural.ExtramuralConsultForExam, //+
                WrittenWork: $scope.DetailExtramural.ExtramuralWrittenWork, //+
                CalcWorks: $scope.DetailExtramural.ExtramuralCalcWorks, //+
                CourseProjects: $scope.DetailExtramural.ExtramuralCourseProjects, //+
                Evaluation: $scope.DetailExtramural.ExtramuralEvaluation, //+
                OralExam: $scope.DetailExtramural.ExtramuralOralExam, //+
                WrittenExam: $scope.DetailExtramural.ExtramuralWrittenExam, //+
                VerifyingOfTest: $scope.DetailExtramural.ExtramuralVerifyingOfTest, //+
                ManagedDiploma: $scope.DetailExtramural.ExtramuralManagedDiploma, //+
                Dek: $scope.DetailExtramural.ExtramuralDek, //+
                VerifyingOfWrittenWorks: $scope.DetailExtramural.ExVerifyingOfWrittenWorks, //+
                Protection: $scope.DetailExtramural.ExtramuralProtection, //+
                Total: $scope.DetailExtramural.ExtramuralTotal, //+
                Active: $scope.DetailExtramural.ExtramuralActive //+
            };
           // console.log($scope.ExtramuralSemesterId, $scope.Detail.selectedTeachEntryLoading, Detail.ExtramuralEntryId, Detail.Ed.Semester);
        var request = {
            method: "POST",
            url: "/Distribution/EditExtramuralDistributed/",
            data: ExtramuralSemester,
            params: { Id: $scope.ExtramuralSemesterId, userId: $scope.Detail.selectedTeachEntryLoading, ExtramuralEntryId: $scope.DetailExtramural.ExtramuralEntryId, semester: $scope.DetailExtramural.Ed.Semester }
        }
            $http(request)
                .then(function (response) {
                    //$scope.detailDayEntryLoad(dE);
                    //$scope.clear();
                    $scope.SuccessfulResult = 'Данні успішно оновлені!';
                    //$scope.gridOptions;
                });
            //.then(function (TeacRate) {
            //var saveTeacRate = teacherservice.saveTeachersRate(TeacRate);
            //saveTeacRate.then(function (d) {
            //    //loadTeachersRate();
            //    $scope.gridOptions;
            //});
            //});
        });
    }


    $scope.fillExtramuralEntryLoad = function () {
        $http
            .get(`/Distribution/GetExtramuralEntryData?loadingId=${$scope.selectedExtraEntryLoading}&semester=${$scope.selectedExtraSemester}`)
            .then(function (response) {
                $scope.extramuralEntries = response.data;
                $scope.extramularGridOptions.data = $scope.extramuralEntries;
                console.log('Data has been received');
            },
                function (response) {
                    console.log(' Unable to load extramuralEntryData: ' + error.message);
                });
    };

    $scope.fillDayTeachLoad = function () {
        $http
            .get(`/Distribution/GetDayTeachLoad?loadingId=${$scope.selectedDayEntryLoading}&semester=${$scope.selectedDaySemester}`)
            .then(function (response) {
                $scope.dayTeach = response.data;
                console.log('Data has been received');
            },
                function (response) {
                    console.log(' Unable to load dayTeach: ' + error.message);
                });
    };

    $scope.fillExtramuralTeachLoad = function () {
        $http
            .get(`/Distribution/GetExtramuralchLoad?loadingId=${$scope.selectedExtraEntryLoading}&semester=${$scope.selectedExtraSemester}`)
            .then(function (response) {
                $scope.extramuralTeach = response.data;
                console.log('Data has been received');
            },
                function (response) {
                    console.log(' Unable to load extramuralTeach: ' + error.message);
                });
    };

    $scope.detailDayEntryLoad = function (dE) {
        console.log(' Started to load data about detailDayEntryLoad');

        $http
            .get(`/Distribution/GetDetailWorkersDistributed`, { params: { DayEntryId: dE.DayEntryId, semester: dE.Dd.Semester } })
            .then(function (response) {
                $scope.Detail = response.data;
                var record = response.data;
                //$scope.Total = record.DeS.Total;
                $scope.DaySemesterId = record.DaySemesterId;
                console.log('Data has been received');
            },
                function (response) {
                    console.log(' Unable to load dayEntryData: ' + error.message);
                });
    };



    /*Extramural EntryLoad */
    $scope.detailExtramuralEntryLoad = function (eE) {
        console.log(' Started to load data about detailExtramuralEntryLoad');

        $http
            .get(`/Distribution/GetExtramuralDistributed`, { params: { ExtramuralEntryId: eE.ExtramuralEntryId, semester: eE.Ed.Semester } })
            .then(function (response) {
                $scope.DetailExtramural = response.data;
                var record = response.data;
                //$scope.Total = record.DeS.Total;
                $scope.ExtramuralEntryId = record.ExtramuralEntryId;
                $scope.ExtramuralSemesterId = record.ExtramuralSemesterId;
                console.log('Data has been received');
            },
                function (response) {
                    console.log(' Unable to load dayEntryData: ' + error.message);
                });
    };


    $scope.updateExtramuralSemester = function (eE) {
        console.log(' Started to load data about updateExtramuralSemester');

        var ExtramuralSemester = {
            ExtramuralSemesterId: $scope.ExtramuralSemesterId,
            Lecture: $scope.ExtramuralLecture, //+
            Practice: $scope.ExtramuralPractice, //+
            Lab: $scope.ExtramuralLab, //+
            ConsultInSemester: $scope.ExtramuralConsultInSemester, //+
            ConsultForExam: $scope.ExtramuralConsultForExam, //+
            WrittenWork: $scope.ExtramuralWrittenWork, //+
            CalcWorks: $scope.ExtramuralCalcWorks, //+
            CourseProjects: $scope.ExtramuralCourseProjects, //+
            Evaluation: $scope.ExtramuralEvaluation, //+
            OralExam: $scope.ExtramuralOralExam, //+
            WrittenExam: $scope.ExtramuralWrittenExam, //+
            VerifyingOfTest: $scope.ExtramuralVerifyingOfTest, //+
            ManagedDiploma: $scope.ExtramuralManagedDiploma, //+
            Dek: $scope.ExtramuralDek, //+
            VerifyingOfWrittenWorks: $scope.ExVerifyingOfWrittenWorks, //+
            Protection: $scope.ExtramuralProtection, //+
            Total: $scope.ExtramuralTotal, //+
            Active: $scope.ExtramuralActive //+
        };
        var request = {
            method: "POST",
            url: "/Distribution/EditExtramuralDistributed/",
            data: ExtramuralSemester,
            params: { Id: $scope.ExtramuralSemesterId, userId: $scope.selectedTeachEntryLoading, ExtramuralEntryId: eE.ExtramuralEntryId, semester: eE.Ed.Semester }
        }

        $http(request)
            .then(function (response) {
                $scope.detailExtramuralEntryLoad(eE);
                $scope.clear();
                alert('Данні успішно оновлені!');
            },
                function (response) {
                    console.log(' Unable to load updateDaySemester: ' + error.message);
                });
    }

    $scope.clearResult = function () {
        $scope.SuccessfulResult = '';
    }

    $scope.clear = function () {
        $scope.Lecture = '';
        $scope.Active = '';
        $scope.ConsultForExam = '';
        $scope.ConsultInSemester = '';
        $scope.ControlEvaluation = '';
        $scope.ControlExam = '';
        $scope.DayEntryLoadId = '';
        $scope.Dek = '';
        $scope.EnglishBonus = '';
        $scope.KR_KP = '';
        $scope.Lab = '';
        $scope.ManagedDiploma = '';
        $scope.Other = '';
        $scope.Practice = '';
        $scope.PracticePreparation = '';
        $scope.StateExam = '';
        $scope.Total = '';
        $scope.VerifyingOfTests = '';

        $scope.ExtramuralLecture = '';
        $scope.ExtramuralPractice = '';
        $scope.ExtramuralLab = '';
        $scope.ExtramuralConsultInSemester = '';
        $scope.ExtramuralConsultForExam = '';
        $scope.ExtramuralWrittenWork = '';
        $scope.ExtramuralCalcWorks = '';
        $scope.ExtramuralCourseProjects = '';
        $scope.ExtramuralEvaluation = '';
        $scope.ExtramuralOralExam = '';
        $scope.ExtramuralWrittenExam = '';
        $scope.ExtramuralVerifyingOfTest = '';
        $scope.ExtramuralManagedDiploma = '';
        $scope.ExtramuralDek = '';
        $scope.ExVerifyingOfWrittenWorks = '';
        $scope.ExtramuralProtection = '';
        $scope.ExtramuralTotal = '';
        $scope.ExtramuralActive = '';
    }





    $scope.downloadCommonDayFormatReport = function () {
        console.log('downloadCommonDayFormatReport before');
        window.location.assign(`/Distribution/DownloadCommonDayFormatReport?loadingId=${$scope.selectedDayEntryLoading}`);
        console.log('downloadCommonDayFormatReport after');
    };

    $scope.downloadCommonExtraFormatReport = function () {
        window.location.assign(`/Distribution/DownloadCommonExtraFormatReport?loadingId=${$scope.selectedExtraEntryLoading}`);
    };

    $scope.downloadDayFormatReportBySemester = function () {
        window.location.assign(`/Department/DownloadDayFormatReportBySemester?loadingId=${$scope.selectedDayEntryLoading}&semester=${$scope.selectedDaySemester}`);
    };

    $scope.downloadExtraFormatReportBySemester = function () {
        window.location.assign(`/Department/DownloadExtraFormatReportBySemester?loadingId=${$scope.selectedExtraEntryLoading}&semester=${$scope.selectedExtraSemester}`);
    }

    $scope.downloadDayTeachLoadReport = function () {
        window.location.assign(`/Distribution/DownloadDayTeachLoadReport?loadingId=${$scope.selectedDayEntryLoading}`);
    }

    $scope.downloadExtraTeachLoadReport = function () {
        window.location.assign(`/Distribution/DownloadExtraTeachLoadReport?loadingId=${$scope.selectedExtraEntryLoading}`);
    }

}]);