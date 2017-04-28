/// <reference path="../App.js" />
/// <reference path="../../angular.js" />
PlannerApp.controller('scientificWorkController', ['$scope', '$http', 'scientificWorkFormFieldFactory', function ($scope, $http, scientificWorkFormFieldFactory) {
	var me = this;

	$scope.fieldFactory = scientificWorkFormFieldFactory;
	$scope.scientificWorkModel = {};
	//tab1
	$scope.scientificWorkModel.ImplementationPlannedResearchManager = null;
	$scope.scientificWorkModel.ImplementationPlannedResearchPerformer = null;
	$scope.scientificWorkModel.DevelopmentInnovativeResearchProjectsManager = null;
	$scope.scientificWorkModel.DevelopmentInnovativeResearchProjectsManagerPerformer = null;
	//tab2
	$scope.scientificWorkModel.InventiveJobTeachingInternationalPatents = null;
	$scope.scientificWorkModel.InventiveWorkTeachersPatents = null;
	$scope.scientificWorkModel.InventiveWorkTeacherPatentUtilityModel = null;
	$scope.scientificWorkModel.InventiveJobTeachingCertificateWork = null;
	//tab3
	$scope.scientificWorkModel.CollectiveMonographPublishedInDomesticPublishing = null;
	$scope.scientificWorkModel.SoleMonographPublishedInDomesticPublishing = null;
	$scope.scientificWorkModel.CollectiveMonographPublishedInForeignPublishingHouses = null;
	$scope.scientificWorkModel.SoleMonographPublishedInForeignPublishingHouses = null;
	$scope.scientificWorkModel.PublicationScientificArticlesInJournals = null;
	$scope.scientificWorkModel.PublicationScientificArticlesAcademicEditions = null;
	$scope.scientificWorkModel.PublicationScientificPapersInInternationalJournals = null;
	$scope.scientificWorkModel.PublicationScientificPapersProfessionalNationaJournals = null;
	$scope.scientificWorkModel.PublicationScientificPapersNationaJournals = null;
	$scope.scientificWorkModel.PublicationAbstractsForeignJournals = null;
	$scope.scientificWorkModel.PublicationAbstractsConferencesSymposiaSeminars = null;
	$scope.scientificWorkModel.CollectiveMonographForeignLanguagePublishedInDomesticPublishing = null;
	$scope.scientificWorkModel.SoleForeignLanguageMonographPublishedInDomesticPublishing = null;
	$scope.scientificWorkModel.CollectiveMonographPublishedForeignLanguageForeignPublishers = null;
	$scope.scientificWorkModel.SoleForeignLanguageMonographPublishedInForeignPublishingHouses = null;
	$scope.scientificWorkModel.PublicationScientificArticlesForeignLanguageJournalsScopusWeb = null;
	$scope.scientificWorkModel.PublicationScientificArticlesForeignLanguageInScientificEditions = null;
	$scope.scientificWorkModel.PublicationScientificPapersForeigLanguageInternationalJournals = null;
	$scope.scientificWorkModel.PublicationScientificPapersForeignLanguageProfessionalNationaJournals = null;
	$scope.scientificWorkModel.PublicationScientificPapersForeignLanguageNationaJournals = null;
	$scope.scientificWorkModel.PublicationAbstractsForeignLanguageForeignJournals = null;
	$scope.scientificWorkModel.PublicationAbstractsForeignLanguageConferencesSymposiaSeminars = null;
	//tab4
	$scope.scientificWorkModel.ProtectingThesisForDegreeDoctorOfScience = null;
	$scope.scientificWorkModel.DefendingTthesisForPhD = null;
	$scope.scientificWorkModel.TheCandidateExams = null;
	//tab5
	$scope.scientificWorkModel.GuidStudentsInResearchWorkOnPreparationScientificArticles = null;
	$scope.scientificWorkModel.GuidStudentsInResearchWorkOnPreparationAbstractsForConference = null;
	$scope.scientificWorkModel.GuidStudentsInResearchWorkOnPreparationApplicationsForSecurityDocuments = null;
	$scope.scientificWorkModel.StudentsWinnersParticipateInInternationalCompetitions = null;
	$scope.scientificWorkModel.StudentsWinnersParticipateInUkrainianLevelPhase1 = null;
	$scope.scientificWorkModel.StudentsWinnersParticipateInUkrainianLevelPhase2 = null;
	$scope.scientificWorkModel.StudentsWinnersParticipateInRegionalLevel = null;
	$scope.scientificWorkModel.StudentsWinnersParticipateInUniversityLevel = null;
	$scope.scientificWorkModel.StudentsWinnersParticipateInInternationalOlympiad = null;
	$scope.scientificWorkModel.StudentsWinnersParticipateInUkrainianLevelOlympiadPhase1 = null;
	$scope.scientificWorkModel.StudentsWinnersParticipateInUkrainianLevelOlympiadPhase2 = null;
	$scope.scientificWorkModel.StudentsWinnersParticipateInOlympiadRegionalLevel = null;
	$scope.scientificWorkModel.StudentsWinnersParticipateInOlympiadUniversityLevel = null;
	$scope.scientificWorkModel.ManagementStudentScienceClub = null;
	//tab6
	$scope.scientificWorkModel.OrganizationAndHoldingInternationalEventsOrganizingCommitteeChairman = null;
	$scope.scientificWorkModel.OrganizationAndHoldingInternationalEventsSecretary = null;
	$scope.scientificWorkModel.OrganizationAndHoldingUkrainianEventsOrganizingCommitteeChairman = null;
	$scope.scientificWorkModel.OrganizationAndHoldingUkrainianEventsSecretary = null;
	$scope.scientificWorkModel.OrganizationInterUniversityInterfacultyInterdepartmentalActivitiesOrganizingCommitteeChairman = null;
	$scope.scientificWorkModel.OrganizationInterUniversityInterfacultyInterdepartmentalActivitiesSecretary = null;
	$scope.scientificWorkModel.OrganizationAndHoldingInternationalEvents = null;
	$scope.scientificWorkModel.OrganizationAndHoldingUkrainianEvents = null;
	$scope.scientificWorkModel.ManagementStudentScienceSchool = null;
	$scope.scientificWorkModel.ResponsiblForScientificWorkDepartment = null;
	$scope.scientificWorkModel.ReviewingAbstractsForDegree = null;
	$scope.scientificWorkModel.ReviewingForeignLanguagePublications = null;
	$scope.scientificWorkModel.ChairmanEditorialBoardSscientificJournals = null;
	$scope.scientificWorkModel.DeputyChairmanEditorialBoardSscientificJournals = null;
	$scope.scientificWorkModel.MemberEditorialBoardSscientificJournals = null;

	$scope.saveData = function () {
	    var model = [];
	    for(var el in $scope.scientificWorkModel)
	    {
	        if ($scope.scientificWorkModel[el] != null) {
	            model.push({ SchemaName: el, Value: $scope.scientificWorkModel[el], Name: $scope.fieldFactory[el].name });
	        }
	    }
	    $http.post('/IndividualPlan/SaveScientificData', { model: model }).then(
            function (response) {
                window.location.reload();
            }, function (response) {

            });
	}

}]);
