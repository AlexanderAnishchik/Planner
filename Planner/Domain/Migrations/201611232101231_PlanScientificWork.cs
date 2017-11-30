namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlanScientificWork : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlanScientificWorks", "ImplementationPlannedResearchManager", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "ImplementationPlannedResearchPerformer", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "DevelopmentInnovativeResearchProjectsManager", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "DevelopmentInnovativeResearchProjectsManagerPerformer", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "InventiveJobTeachingInternationalPatents", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "InventiveWorkTeachersPatents", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "InventiveWorkTeacherPatentUtilityModel", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "InventiveJobTeachingCertificateWork", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "CollectiveMonographPublishedInDomesticPublishing", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "SoleMonographPublishedInDomesticPublishing", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "CollectiveMonographPublishedInForeignPublishingHouses", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "SoleMonographPublishedInForeignPublishingHouses", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "PublicationScientificArticlesInJournals", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "PublicationScientificArticlesAcademicEditions", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "PublicationScientificPapersInInternationalJournals", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "PublicationScientificPapersProfessionalNationaJournals", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "PublicationScientificPapersNationaJournals", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "PublicationAbstractsForeignJournals", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "PublicationAbstractsConferencesSymposiaSeminars", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "CollectiveMonographForeignLanguagePublishedInDomesticPublishing", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "SoleForeignLanguageMonographPublishedInDomesticPublishing", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "CollectiveMonographPublishedForeignLanguageForeignPublishers", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "SoleForeignLanguageMonographPublishedInForeignPublishingHouses", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "PublicationScientificArticlesForeignLanguageJournalsScopusWeb", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "PublicationScientificArticlesForeignLanguageInScientificEditions", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "PublicationScientificPapersForeigLanguageInternationalJournals", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "PublicationScientificPapersForeignLanguageProfessionalNationaJournals", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "PublicationScientificPapersForeignLanguageNationaJournals", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "PublicationAbstractsForeignLanguageForeignJournals", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "PublicationAbstractsForeignLanguageConferencesSymposiaSeminars", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "ProtectingThesisForDegreeDoctorOfScience", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "DefendingTthesisForPhD", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "TheCandidateExams", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "GuidStudentsInResearchWorkOnPreparationScientificArticles", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "GuidStudentsInResearchWorkOnPreparationAbstractsForConference", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "GuidStudentsInResearchWorkOnPreparationApplicationsForSecurityDocuments", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "StudentsWinnersParticipateInInternationalCompetitions", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "StudentsWinnersParticipateInUkrainianLevelPhase1", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "StudentsWinnersParticipateInUkrainianLevelPhase2", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "StudentsWinnersParticipateInRegionalLevel", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "StudentsWinnersParticipateInUniversityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "StudentsWinnersParticipateInInternationalOlympiad", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "StudentsWinnersParticipateInUkrainianLevelOlympiadPhase1", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "StudentsWinnersParticipateInUkrainianLevelOlympiadPhase2", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "StudentsWinnersParticipateInOlympiadRegionalLevel", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "StudentsWinnersParticipateInOlympiadUniversityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "ManagementStudentScienceClub", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "OrganizationAndHoldingInternationalEventsOrganizingCommitteeChairman", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "OrganizationAndHoldingInternationalEventsSecretary", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "OrganizationAndHoldingUkrainianEventsOrganizingCommitteeChairman", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "OrganizationAndHoldingUkrainianEventsSecretary", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "OrganizationInterUniversityInterfacultyInterdepartmentalActivitiesOrganizingCommitteeChairman", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "OrganizationInterUniversityInterfacultyInterdepartmentalActivitiesSecretary", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "OrganizationAndHoldingInternationalEvents", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "OrganizationAndHoldingUkrainianEvents", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "ManagementStudentScienceSchool", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "ResponsiblForScientificWorkDepartment", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "ReviewingAbstractsForDegree", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "ReviewingForeignLanguagePublications", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "ChairmanEditorialBoardSscientificJournals", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "DeputyChairmanEditorialBoardSscientificJournals", c => c.Int(nullable: false));
            AddColumn("dbo.PlanScientificWorks", "MemberEditorialBoardSscientificJournals", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlanScientificWorks", "MemberEditorialBoardSscientificJournals");
            DropColumn("dbo.PlanScientificWorks", "DeputyChairmanEditorialBoardSscientificJournals");
            DropColumn("dbo.PlanScientificWorks", "ChairmanEditorialBoardSscientificJournals");
            DropColumn("dbo.PlanScientificWorks", "ReviewingForeignLanguagePublications");
            DropColumn("dbo.PlanScientificWorks", "ReviewingAbstractsForDegree");
            DropColumn("dbo.PlanScientificWorks", "ResponsiblForScientificWorkDepartment");
            DropColumn("dbo.PlanScientificWorks", "ManagementStudentScienceSchool");
            DropColumn("dbo.PlanScientificWorks", "OrganizationAndHoldingUkrainianEvents");
            DropColumn("dbo.PlanScientificWorks", "OrganizationAndHoldingInternationalEvents");
            DropColumn("dbo.PlanScientificWorks", "OrganizationInterUniversityInterfacultyInterdepartmentalActivitiesSecretary");
            DropColumn("dbo.PlanScientificWorks", "OrganizationInterUniversityInterfacultyInterdepartmentalActivitiesOrganizingCommitteeChairman");
            DropColumn("dbo.PlanScientificWorks", "OrganizationAndHoldingUkrainianEventsSecretary");
            DropColumn("dbo.PlanScientificWorks", "OrganizationAndHoldingUkrainianEventsOrganizingCommitteeChairman");
            DropColumn("dbo.PlanScientificWorks", "OrganizationAndHoldingInternationalEventsSecretary");
            DropColumn("dbo.PlanScientificWorks", "OrganizationAndHoldingInternationalEventsOrganizingCommitteeChairman");
            DropColumn("dbo.PlanScientificWorks", "ManagementStudentScienceClub");
            DropColumn("dbo.PlanScientificWorks", "StudentsWinnersParticipateInOlympiadUniversityLevel");
            DropColumn("dbo.PlanScientificWorks", "StudentsWinnersParticipateInOlympiadRegionalLevel");
            DropColumn("dbo.PlanScientificWorks", "StudentsWinnersParticipateInUkrainianLevelOlympiadPhase2");
            DropColumn("dbo.PlanScientificWorks", "StudentsWinnersParticipateInUkrainianLevelOlympiadPhase1");
            DropColumn("dbo.PlanScientificWorks", "StudentsWinnersParticipateInInternationalOlympiad");
            DropColumn("dbo.PlanScientificWorks", "StudentsWinnersParticipateInUniversityLevel");
            DropColumn("dbo.PlanScientificWorks", "StudentsWinnersParticipateInRegionalLevel");
            DropColumn("dbo.PlanScientificWorks", "StudentsWinnersParticipateInUkrainianLevelPhase2");
            DropColumn("dbo.PlanScientificWorks", "StudentsWinnersParticipateInUkrainianLevelPhase1");
            DropColumn("dbo.PlanScientificWorks", "StudentsWinnersParticipateInInternationalCompetitions");
            DropColumn("dbo.PlanScientificWorks", "GuidStudentsInResearchWorkOnPreparationApplicationsForSecurityDocuments");
            DropColumn("dbo.PlanScientificWorks", "GuidStudentsInResearchWorkOnPreparationAbstractsForConference");
            DropColumn("dbo.PlanScientificWorks", "GuidStudentsInResearchWorkOnPreparationScientificArticles");
            DropColumn("dbo.PlanScientificWorks", "TheCandidateExams");
            DropColumn("dbo.PlanScientificWorks", "DefendingTthesisForPhD");
            DropColumn("dbo.PlanScientificWorks", "ProtectingThesisForDegreeDoctorOfScience");
            DropColumn("dbo.PlanScientificWorks", "PublicationAbstractsForeignLanguageConferencesSymposiaSeminars");
            DropColumn("dbo.PlanScientificWorks", "PublicationAbstractsForeignLanguageForeignJournals");
            DropColumn("dbo.PlanScientificWorks", "PublicationScientificPapersForeignLanguageNationaJournals");
            DropColumn("dbo.PlanScientificWorks", "PublicationScientificPapersForeignLanguageProfessionalNationaJournals");
            DropColumn("dbo.PlanScientificWorks", "PublicationScientificPapersForeigLanguageInternationalJournals");
            DropColumn("dbo.PlanScientificWorks", "PublicationScientificArticlesForeignLanguageInScientificEditions");
            DropColumn("dbo.PlanScientificWorks", "PublicationScientificArticlesForeignLanguageJournalsScopusWeb");
            DropColumn("dbo.PlanScientificWorks", "SoleForeignLanguageMonographPublishedInForeignPublishingHouses");
            DropColumn("dbo.PlanScientificWorks", "CollectiveMonographPublishedForeignLanguageForeignPublishers");
            DropColumn("dbo.PlanScientificWorks", "SoleForeignLanguageMonographPublishedInDomesticPublishing");
            DropColumn("dbo.PlanScientificWorks", "CollectiveMonographForeignLanguagePublishedInDomesticPublishing");
            DropColumn("dbo.PlanScientificWorks", "PublicationAbstractsConferencesSymposiaSeminars");
            DropColumn("dbo.PlanScientificWorks", "PublicationAbstractsForeignJournals");
            DropColumn("dbo.PlanScientificWorks", "PublicationScientificPapersNationaJournals");
            DropColumn("dbo.PlanScientificWorks", "PublicationScientificPapersProfessionalNationaJournals");
            DropColumn("dbo.PlanScientificWorks", "PublicationScientificPapersInInternationalJournals");
            DropColumn("dbo.PlanScientificWorks", "PublicationScientificArticlesAcademicEditions");
            DropColumn("dbo.PlanScientificWorks", "PublicationScientificArticlesInJournals");
            DropColumn("dbo.PlanScientificWorks", "SoleMonographPublishedInForeignPublishingHouses");
            DropColumn("dbo.PlanScientificWorks", "CollectiveMonographPublishedInForeignPublishingHouses");
            DropColumn("dbo.PlanScientificWorks", "SoleMonographPublishedInDomesticPublishing");
            DropColumn("dbo.PlanScientificWorks", "CollectiveMonographPublishedInDomesticPublishing");
            DropColumn("dbo.PlanScientificWorks", "InventiveJobTeachingCertificateWork");
            DropColumn("dbo.PlanScientificWorks", "InventiveWorkTeacherPatentUtilityModel");
            DropColumn("dbo.PlanScientificWorks", "InventiveWorkTeachersPatents");
            DropColumn("dbo.PlanScientificWorks", "InventiveJobTeachingInternationalPatents");
            DropColumn("dbo.PlanScientificWorks", "DevelopmentInnovativeResearchProjectsManagerPerformer");
            DropColumn("dbo.PlanScientificWorks", "DevelopmentInnovativeResearchProjectsManager");
            DropColumn("dbo.PlanScientificWorks", "ImplementationPlannedResearchPerformer");
            DropColumn("dbo.PlanScientificWorks", "ImplementationPlannedResearchManager");
        }
    }
}
