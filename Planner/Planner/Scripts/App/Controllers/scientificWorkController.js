/// <reference path="../App.js" />
/// <reference path="../../angular.js" />
PlannerApp.controller('scientificWorkController', ['$scope', '$http', 'scientificWorkFormFieldFactory', function ($scope, $http, scientificWorkFormFieldFactory) {
    var me = this;

    $scope.fieldFactory = scientificWorkFormFieldFactory;
    $scope.scientificWorkModel = {};
    //tab1
    $scope.scientificWorkModel.ImplementationPlannedResearchManager = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.ImplementationPlannedResearchPerformer = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.DevelopmentInnovativeResearchProjectsManager = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.DevelopmentInnovativeResearchProjectsManagerPerformer = { Result: null, PlannedValue: null };
    //tab2
    $scope.scientificWorkModel.InventiveJobTeachingInternationalPatents = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.InventiveWorkTeachersPatents = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.InventiveWorkTeacherPatentUtilityModel = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.InventiveJobTeachingCertificateWork = { Result: null, PlannedValue: null };
    //tab3
    $scope.scientificWorkModel.CollectiveMonographPublishedInDomesticPublishing = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.SoleMonographPublishedInDomesticPublishing = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.CollectiveMonographPublishedInForeignPublishingHouses = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.SoleMonographPublishedInForeignPublishingHouses = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.PublicationScientificArticlesInJournals = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.PublicationScientificArticlesAcademicEditions = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.PublicationScientificPapersInInternationalJournals = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.PublicationScientificPapersProfessionalNationaJournals = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.PublicationScientificPapersNationaJournals = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.PublicationAbstractsForeignJournals = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.PublicationAbstractsConferencesSymposiaSeminars = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.CollectiveMonographForeignLanguagePublishedInDomesticPublishing = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.SoleForeignLanguageMonographPublishedInDomesticPublishing = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.CollectiveMonographPublishedForeignLanguageForeignPublishers = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.SoleForeignLanguageMonographPublishedInForeignPublishingHouses = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.PublicationScientificArticlesForeignLanguageJournalsScopusWeb = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.PublicationScientificArticlesForeignLanguageInScientificEditions = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.PublicationScientificPapersForeigLanguageInternationalJournals = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.PublicationScientificPapersForeignLanguageProfessionalNationaJournals = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.PublicationScientificPapersForeignLanguageNationaJournals = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.PublicationAbstractsForeignLanguageForeignJournals = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.PublicationAbstractsForeignLanguageConferencesSymposiaSeminars = { Result: null, PlannedValue: null };
    //tab4
    $scope.scientificWorkModel.ProtectingThesisForDegreeDoctorOfScience = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.DefendingTthesisForPhD = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.TheCandidateExams = { Result: null, PlannedValue: null };
    //tab5
    $scope.scientificWorkModel.GuidStudentsInResearchWorkOnPreparationScientificArticles = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.GuidStudentsInResearchWorkOnPreparationAbstractsForConference = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.GuidStudentsInResearchWorkOnPreparationApplicationsForSecurityDocuments = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.StudentsWinnersParticipateInInternationalCompetitions = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.StudentsWinnersParticipateInUkrainianLevelPhase1 = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.StudentsWinnersParticipateInUkrainianLevelPhase2 = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.StudentsWinnersParticipateInRegionalLevel = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.StudentsWinnersParticipateInUniversityLevel = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.StudentsWinnersParticipateInInternationalOlympiad = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.StudentsWinnersParticipateInUkrainianLevelOlympiadPhase1 = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.StudentsWinnersParticipateInUkrainianLevelOlympiadPhase2 = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.StudentsWinnersParticipateInOlympiadRegionalLevel = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.StudentsWinnersParticipateInOlympiadUniversityLevel = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.ManagementStudentScienceClub = { Result: null, PlannedValue: null };
    //tab6
    $scope.scientificWorkModel.OrganizationAndHoldingInternationalEventsOrganizingCommitteeChairman = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.OrganizationAndHoldingInternationalEventsSecretary = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.OrganizationAndHoldingUkrainianEventsOrganizingCommitteeChairman = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.OrganizationAndHoldingUkrainianEventsSecretary = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.OrganizationInterUniversityInterfacultyInterdepartmentalActivitiesOrganizingCommitteeChairman = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.OrganizationInterUniversityInterfacultyInterdepartmentalActivitiesSecretary = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.OrganizationAndHoldingInternationalEvents = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.OrganizationAndHoldingUkrainianEvents = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.ManagementStudentScienceSchool = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.ResponsiblForScientificWorkDepartment = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.ReviewingAbstractsForDegree = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.ReviewingForeignLanguagePublications = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.ChairmanEditorialBoardSscientificJournals = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.DeputyChairmanEditorialBoardSscientificJournals = { Result: null, PlannedValue: null };
    $scope.scientificWorkModel.MemberEditorialBoardSscientificJournals = { Result: null, PlannedValue: null };

    $scope.saveData = function () {
        var model = [];
        for (var el in $scope.scientificWorkModel) {
            if ($scope.scientificWorkModel[el] != { Result: null, PlannedValue: null }) {
                model.push({ SchemaName: el, Value: $scope.scientificWorkModel[el].Result, PlannedValue: $scope.scientificWorkModel[el].PlannedValue, Name: $scope.fieldFactory[el].name });
            }
        }
        $http.post('/IndividualPlan/SaveData', { model: model }).then(
            function (response) {
                window.location.reload();
            }, function (response) {

            });
    }
    $scope.getData = function () {
        $http.get('/IndividualPlan/GetPlanScientificWork').then(
            function (response) {
                if (response.data != null) {
                    Object.keys($scope.scientificWorkModel).forEach(function (el) {
                        var val = response.data.find(function (element) { return el == element.SchemaName });
                        if (val) {
                            $scope.scientificWorkModel[el].Result = Number(val.Result);
                            $scope.scientificWorkModel[el].PlannedValue = Number(val.PlannedValue);
                        }
                    });
                }
            }, function (response) {

            });
    }
}]);
