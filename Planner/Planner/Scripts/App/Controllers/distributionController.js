PlannerApp.controller('distributionController', ['$scope', '$http', function ($scope, $http) {
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
                console.log('Data has been received');
            },
                function (response) {
                    console.log(' Unable to load dayEntryData: ' + error.message);
                });
    };

    $scope.fillExtramuralEntryLoad = function () {
        $http
            .get(`/Distribution/GetExtramuralEntryData?loadingId=${$scope.selectedExtraEntryLoading}&semester=${$scope.selectedExtraSemester}`)
            .then(function (response) {
                $scope.extramuralEntries = response.data;
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

    //UpdateDaySemester
    $scope.updateDaySemester = function (dE) {
        console.log(' Started to load data about detailDayEntryLoad');

        var DaySemester = {
            DaySemesterId: $scope.DaySemesterId,
            Active: $scope.Active,
            ConsultForExam: $scope.ConsultForExam,
            ConsultInSemester: $scope.ConsultInSemester,
            ControlEvaluation: $scope.ControlEvaluation,
            ControlExam: $scope.ControlExam,
            DayEntryLoadId: $scope.DayEntryLoadId,
            Dek: $scope.Dek,
            EnglishBonus: $scope.EnglishBonus,
            KR_KP: $scope.KR_KP,
            Lab: $scope.Lab,
            Lecture: $scope.Lecture,
            ManagedDiploma: $scope.ManagedDiploma,
            Other: $scope.Other,
            Practice: $scope.Practice,
            PracticePreparation: $scope.PracticePreparation,
            StateExam: $scope.StateExam,
            Total: $scope.Total,
            VerifyingOfTests: $scope.VerifyingOfTests
        };
        var request = {
            method: "POST",
            url: "/Distribution/EditDetailWorkersDistributed/",
            data: DaySemester,
            params: { Id: $scope.DaySemesterId, userId: $scope.selectedTeachEntryLoading, DayEntryId: dE.DayEntryId, semestr: dE.Dd.Semester }
        }

        $http(request)
            .then(function (response) {
                $scope.detailDayEntryLoad(dE);
                $scope.clear();
                //alert('Данні успішно оновлені!');
                $scope.SuccessfulResult = 'Данні успішно оновлені!';
            },
                function (response) {
                    alert(error.message);
                    console.log(' Unable to load updateDaySemester: ' + error.message);
                });
    }

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