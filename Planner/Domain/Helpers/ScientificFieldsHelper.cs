using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public   class ScientificFieldsHelper
    {
        [Range(0, 200, ErrorMessage = "Можливі числа від 0 .. 200")]
        public int ImplementationPlannedResearchManager { get; set; }
        [Range(0, 100, ErrorMessage = "Можливі числа від 0 .. 100")]
        public int ImplementationPlannedResearchPerformer { get; set; }
        [Range(0, 300, ErrorMessage = "Можливі числа від 0 .. 300")]
        public int DevelopmentInnovativeResearchProjectsManager { get; set; }
        [Range(0, 200, ErrorMessage = "Можливі числа від 0 .. 200")]
        public int DevelopmentInnovativeResearchProjectsManagerPerformer { get; set; }
        [Range(0, 150, ErrorMessage = "Можливі числа від 0 .. 150")]
        public int InventiveJobTeachingInternationalPatents { get; set; }
        [Range(0, 100, ErrorMessage = "Можливі числа від 0 .. 100")]
        public int InventiveWorkTeachersPatents { get; set; }
        [Range(0, 100, ErrorMessage = "Можливі числа від 0 .. 100")]
        public int InventiveWorkTeacherPatentUtilityModel { get; set; }
        [Range(0, 50, ErrorMessage = "Можливі числа від 0 .. 50")]
        public int InventiveJobTeachingCertificateWork { get; set; }
        [Range(0, 100, ErrorMessage = "Можливі числа від 0 .. 100")]
        public int CollectiveMonographPublishedInDomesticPublishing { get; set; }
        [Range(0, 150, ErrorMessage = "Можливі числа від 0 .. 150")]
        public int SoleMonographPublishedInDomesticPublishing { get; set; }
        [Range(0, 150, ErrorMessage = "Можливі числа від 0 .. 150")]
        public int CollectiveMonographPublishedInForeignPublishingHouses { get; set; }
        [Range(0, 200, ErrorMessage = "Можливі числа від 0 .. 200")]
        public int SoleMonographPublishedInForeignPublishingHouses { get; set; }
        [Range(0, 300, ErrorMessage = "Можливі числа від 0 .. 300")]
        public int PublicationScientificArticlesInJournals { get; set; }
        [Range(0, 200, ErrorMessage = "Можливі числа від 0 .. 200")]
        public int PublicationScientificArticlesAcademicEditions { get; set; }
        [Range(0, 150, ErrorMessage = "Можливі числа від 0 .. 150")]
        public int PublicationScientificPapersInInternationalJournals { get; set; }
        [Range(0, 100, ErrorMessage = "Можливі числа від 0 .. 100")]
        public int PublicationScientificPapersProfessionalNationaJournals { get; set; }
        [Range(0, 70, ErrorMessage = "Можливі числа від 0 .. 70")]
        public int PublicationScientificPapersNationaJournals { get; set; }
        [Range(0, 70, ErrorMessage = "Можливі числа від 0 .. 70")]
        public int PublicationAbstractsForeignJournals { get; set; }
        [Range(0, 50, ErrorMessage = "Можливі числа від 0 .. 50")]
        public int PublicationAbstractsConferencesSymposiaSeminars { get; set; }
        [Range(0, 150, ErrorMessage = "Можливі числа від 0 .. 150")]
        public int CollectiveMonographForeignLanguagePublishedInDomesticPublishing { get; set; }
        [Range(0, 225, ErrorMessage = "Можливі числа від 0 .. 225")]
        public int SoleForeignLanguageMonographPublishedInDomesticPublishing { get; set; }
        [Range(0, 225, ErrorMessage = "Можливі числа від 0 .. 225")]
        public int CollectiveMonographPublishedForeignLanguageForeignPublishers { get; set; }
        [Range(0, 300, ErrorMessage = "Можливі числа від 0 .. 300")]
        public int SoleForeignLanguageMonographPublishedInForeignPublishingHouses { get; set; }
        [Range(0, 450, ErrorMessage = "Можливі числа від 0 .. 450")]
        public int PublicationScientificArticlesForeignLanguageJournalsScopusWeb { get; set; }
        [Range(0, 300, ErrorMessage = "Можливі числа від 0 .. 300")]
        public int PublicationScientificArticlesForeignLanguageInScientificEditions { get; set; }
        [Range(0, 225, ErrorMessage = "Можливі числа від 0 .. 225")]
        public int PublicationScientificPapersForeigLanguageInternationalJournals { get; set; }
        [Range(0, 150, ErrorMessage = "Можливі числа від 0 .. 150")]
        public int PublicationScientificPapersForeignLanguageProfessionalNationaJournals { get; set; }
        [Range(0, 105, ErrorMessage = "Можливі числа від 0 .. 105")]
        public int PublicationScientificPapersForeignLanguageNationaJournals { get; set; }
        [Range(0, 105, ErrorMessage = "Можливі числа від 0 .. 105")]
        public int PublicationAbstractsForeignLanguageForeignJournals { get; set; }
        [Range(0, 75, ErrorMessage = "Можливі числа від 0 .. 75")]
        public int PublicationAbstractsForeignLanguageConferencesSymposiaSeminars { get; set; }
        [Range(0, 500, ErrorMessage = "Можливі числа від 0 .. 500")]
        public int ProtectingThesisForDegreeDoctorOfScience { get; set; }
        [Range(0, 300, ErrorMessage = "Можливі числа від 0 .. 300")]
        public int DefendingTthesisForPhD { get; set; }
        [Range(0, 50, ErrorMessage = "Можливі числа від 0 .. 50")]
        public int TheCandidateExams { get; set; }
        [Range(0, 50, ErrorMessage = "Можливі числа від 0 .. 50")]
        public int GuidStudentsInResearchWorkOnPreparationScientificArticles { get; set; }
        [Range(0, 50, ErrorMessage = "Можливі числа від 0 .. 50")]
        public int GuidStudentsInResearchWorkOnPreparationAbstractsForConference { get; set; }
        [Range(0, 50, ErrorMessage = "Можливі числа від 0 .. 50")]
        public int GuidStudentsInResearchWorkOnPreparationApplicationsForSecurityDocuments { get; set; }
        [Range(0, 100, ErrorMessage = "Можливі числа від 0 .. 100")]
        public int StudentsWinnersParticipateInInternationalCompetitions { get; set; }
        [Range(0, 10, ErrorMessage = "Можливі числа від 0 .. 10")]
        public int StudentsWinnersParticipateInUkrainianLevelPhase1 { get; set; }
        [Range(0, 50, ErrorMessage = "Можливі числа від 0 .. 50")]
        public int StudentsWinnersParticipateInUkrainianLevelPhase2 { get; set; }
        [Range(0, 30, ErrorMessage = "Можливі числа від 0 .. 30")]
        public int StudentsWinnersParticipateInRegionalLevel { get; set; }
        [Range(0, 10, ErrorMessage = "Можливі числа від 0 .. 10")]
        public int StudentsWinnersParticipateInUniversityLevel { get; set; }
        [Range(0, 100, ErrorMessage = "Можливі числа від 0 .. 100")]
        public int StudentsWinnersParticipateInInternationalOlympiad { get; set; }
        [Range(0, 10, ErrorMessage = "Можливі числа від 0 .. 10")]
        public int StudentsWinnersParticipateInUkrainianLevelOlympiadPhase1 { get; set; }
        [Range(0, 50, ErrorMessage = "Можливі числа від 0 .. 50")]
        public int StudentsWinnersParticipateInUkrainianLevelOlympiadPhase2 { get; set; }
        [Range(0, 30, ErrorMessage = "Можливі числа від 0 .. 30")]
        public int StudentsWinnersParticipateInOlympiadRegionalLevel { get; set; }
        [Range(0, 10, ErrorMessage = "Можливі числа від 0 .. 10")]
        public int StudentsWinnersParticipateInOlympiadUniversityLevel { get; set; }
        [Range(0, 50, ErrorMessage = "Можливі числа від 0 .. 50")]
        public int ManagementStudentScienceClub { get; set; }
        [Range(0, 25, ErrorMessage = "Можливі числа від 0 .. 25")]
        public int OrganizationAndHoldingInternationalEventsOrganizingCommitteeChairman { get; set; }
        [Range(0, 35, ErrorMessage = "Можливі числа від 0 .. 35")]
        public int OrganizationAndHoldingInternationalEventsSecretary { get; set; }
        [Range(0, 20, ErrorMessage = "Можливі числа від 0 .. 20")]
        public int OrganizationAndHoldingUkrainianEventsOrganizingCommitteeChairman { get; set; }
        [Range(0, 30, ErrorMessage = "Можливі числа від 0 .. 30")]
        public int OrganizationAndHoldingUkrainianEventsSecretary { get; set; }
        [Range(0, 15, ErrorMessage = "Можливі числа від 0 .. 15")]
        public int OrganizationInterUniversityInterfacultyInterdepartmentalActivitiesOrganizingCommitteeChairman { get; set; }
        [Range(0, 25, ErrorMessage = "Можливі числа від 0 .. 25")]
        public int OrganizationInterUniversityInterfacultyInterdepartmentalActivitiesSecretary { get; set; }
        [Range(0, 50, ErrorMessage = "Можливі числа від 0 .. 50")]
        public int OrganizationAndHoldingInternationalEvents { get; set; }
        [Range(0, 40, ErrorMessage = "Можливі числа від 0 .. 40")]
        public int OrganizationAndHoldingUkrainianEvents { get; set; }
        [Range(0, 50, ErrorMessage = "Можливі числа від 0 .. 50")]
        public int ManagementStudentScienceSchool { get; set; }
        [Range(0, 50, ErrorMessage = "Можливі числа від 0 .. 50")]
        public int ResponsiblForScientificWorkDepartment { get; set; }
        [Range(0, 20, ErrorMessage = "Можливі числа від 0 .. 20")]
        public int ReviewingAbstractsForDegree { get; set; }
        [Range(0, 30, ErrorMessage = "Можливі числа від 0 .. 30")]
        public int ReviewingForeignLanguagePublications { get; set; }
        [Range(0, 100, ErrorMessage = "Можливі числа від 0 .. 100")]
        public int ChairmanEditorialBoardSscientificJournals { get; set; }
        [Range(0, 50, ErrorMessage = "Можливі числа від 0 .. 50")]
        public int DeputyChairmanEditorialBoardSscientificJournals { get; set; }
        [Range(0, 50, ErrorMessage = "Можливі числа від 0 .. 50")]
        public int MemberEditorialBoardSscientificJournals { get; set; }
    }
}
