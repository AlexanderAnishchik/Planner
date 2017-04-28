namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DayEntryLoad : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DayTeachLoads",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Semester = c.Int(nullable: false),
                        Specialty = c.String(),
                        Course = c.Double(nullable: false),
                        Lecture = c.Double(nullable: false),
                        Practice = c.Double(nullable: false),
                        Lab = c.Double(nullable: false),
                        ConsultInSemester = c.Double(nullable: false),
                        ConsultForExam = c.Double(nullable: false),
                        WrittenWork = c.Double(nullable: false),
                        CalcWorks = c.Double(nullable: false),
                        CourseProjects = c.Double(nullable: false),
                        Evaluation = c.Double(nullable: false),
                        OralExam = c.Double(nullable: false),
                        WrittenExam = c.Double(nullable: false),
                        VerifyingOfTest = c.Double(nullable: false),
                        ManagedDiploma = c.Double(nullable: false),
                        Dek = c.Double(nullable: false),
                        VerifyingOfWrittenWorks = c.Double(nullable: false),
                        Protection = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                        Active = c.Double(nullable: false),
                        SubjectId = c.String(maxLength: 128),
                        DayEntryLoadId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DayEntryLoads", t => t.DayEntryLoadId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId)
                .Index(t => t.SubjectId)
                .Index(t => t.DayEntryLoadId);
            
            CreateTable(
                "dbo.DayEntryLoads",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Language = c.String(),
                        Note = c.String(),
                        EducationDegree = c.String(),
                        ConflatedThreads = c.String(),
                        CountOfCredits = c.Double(nullable: false),
                        CountOfHours = c.Double(nullable: false),
                        HoursPerCredit = c.Double(nullable: false),
                        FSemCoefficient = c.Double(nullable: false),
                        SSemCoefficient = c.Double(nullable: false),
                        F_TotalHour = c.Double(nullable: false),
                        F_Total = c.Double(nullable: false),
                        F_Lectures = c.Double(nullable: false),
                        F_Labs = c.Double(nullable: false),
                        F_Practical = c.Double(nullable: false),
                        F_IndividualWork = c.Double(nullable: false),
                        F_CourseProjects = c.String(),
                        F_Exams = c.String(),
                        F_Evaluation = c.String(),
                        S_TotalHour = c.Double(nullable: false),
                        S_Total = c.Double(nullable: false),
                        S_Lectures = c.Double(nullable: false),
                        S_Labs = c.Double(nullable: false),
                        S_Practical = c.Double(nullable: false),
                        S_IndividualWork = c.Double(nullable: false),
                        S_CourseProjects = c.String(),
                        S_Exams = c.String(),
                        S_Evaluation = c.String(),
                        DepartmentCipher = c.String(),
                        FS_CountOfWeeks = c.Double(nullable: false),
                        SS_CountOfWeeks = c.Double(nullable: false),
                        QuantityOfStudents = c.Double(nullable: false),
                        QuantityOfForeigners = c.Double(nullable: false),
                        CipherOfGroups = c.String(),
                        QuantityOfGroupsCritOne = c.Double(nullable: false),
                        RealQuantityOfGroups = c.Double(nullable: false),
                        QuantityOfGroupsCritTwo = c.Double(nullable: false),
                        QuantityOfThreads = c.Double(nullable: false),
                        CipherOfThreads = c.Double(nullable: false),
                        KR_KP_DR = c.Double(nullable: false),
                        Practice = c.Double(nullable: false),
                        QuantityOfDek = c.Double(nullable: false),
                        LoadingListId = c.String(maxLength: 128),
                        DepartmentId = c.String(maxLength: 128),
                        SpecialtyId = c.String(maxLength: 128),
                        SpecializeId = c.String(maxLength: 128),
                        CourseId = c.String(maxLength: 128),
                        SubjectId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.LoadingLists", t => t.LoadingListId)
                .ForeignKey("dbo.Specialties", t => t.SpecialtyId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId)
                .ForeignKey("dbo.Specializes", t => t.SpecializeId)
                .Index(t => t.LoadingListId)
                .Index(t => t.DepartmentId)
                .Index(t => t.SpecialtyId)
                .Index(t => t.SpecializeId)
                .Index(t => t.CourseId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Literal = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DaySemesters",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Semester = c.Byte(nullable: false),
                        Lecture = c.Double(nullable: false),
                        Practice = c.Double(nullable: false),
                        Lab = c.Double(nullable: false),
                        ConsultInSemester = c.Double(nullable: false),
                        ConsultForExam = c.Double(nullable: false),
                        VerifyingOfTests = c.Double(nullable: false),
                        KR_KP = c.Double(nullable: false),
                        ControlEvaluation = c.Double(nullable: false),
                        ControlExam = c.Double(nullable: false),
                        PracticePreparation = c.Double(nullable: false),
                        Dek = c.Double(nullable: false),
                        StateExam = c.Double(nullable: false),
                        ManagedDiploma = c.Double(nullable: false),
                        Other = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                        Active = c.Double(nullable: false),
                        EnglishBonus = c.Double(nullable: false),
                        DayEntryLoadId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DayEntryLoads", t => t.DayEntryLoadId)
                .Index(t => t.DayEntryLoadId);
            
            CreateTable(
                "dbo.ExtramuralEntryLoads",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DepartmentCipher = c.String(),
                        Extramural = c.String(),
                        Course = c.Double(nullable: false),
                        QuantityOfStudents = c.Double(nullable: false),
                        QuantityOfGroups = c.Double(nullable: false),
                        QuantityOfThreads = c.Double(nullable: false),
                        NumOfThread = c.Double(nullable: false),
                        MajorSpecialty = c.String(),
                        CommonTime = c.Double(nullable: false),
                        Credits = c.Double(nullable: false),
                        F_Lecture = c.Double(nullable: false),
                        F_Practical = c.Double(nullable: false),
                        F_Lab = c.Double(nullable: false),
                        F_IndividualWork = c.Double(nullable: false),
                        F_Exam = c.String(),
                        F_Evaluation = c.String(),
                        F_KR = c.String(),
                        F_Test = c.Double(nullable: false),
                        S_Lecture = c.Double(nullable: false),
                        S_Practical = c.Double(nullable: false),
                        S_Lab = c.Double(nullable: false),
                        S_IndividualWork = c.Double(nullable: false),
                        S_Exam = c.String(),
                        S_Evaluation = c.String(),
                        S_KR = c.String(),
                        S_Test = c.Double(nullable: false),
                        LoadingListId = c.String(maxLength: 128),
                        DepartmentId = c.String(maxLength: 128),
                        SubjectId = c.String(maxLength: 128),
                        SpecialtyId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.LoadingLists", t => t.LoadingListId)
                .ForeignKey("dbo.Specialties", t => t.SpecialtyId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId)
                .Index(t => t.LoadingListId)
                .Index(t => t.DepartmentId)
                .Index(t => t.SubjectId)
                .Index(t => t.SpecialtyId);
            
            CreateTable(
                "dbo.ExtramuralSemesters",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Semester = c.Int(nullable: false),
                        Lecture = c.Double(nullable: false),
                        Practice = c.Double(nullable: false),
                        Lab = c.Double(nullable: false),
                        ConsultInSemester = c.Double(nullable: false),
                        ConsultForExam = c.Double(nullable: false),
                        WrittenWork = c.Double(nullable: false),
                        CalcWorks = c.Double(nullable: false),
                        CourseProjects = c.Double(nullable: false),
                        Evaluation = c.Double(nullable: false),
                        OralExam = c.Double(nullable: false),
                        WrittenExam = c.Double(nullable: false),
                        VerifyingOfTest = c.Double(nullable: false),
                        ManagedDiploma = c.Double(nullable: false),
                        Dek = c.Double(nullable: false),
                        VerifyingOfWrittenWorks = c.Double(nullable: false),
                        Protection = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                        Active = c.Double(nullable: false),
                        ExtramuralEntryLoadId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExtramuralEntryLoads", t => t.ExtramuralEntryLoadId)
                .Index(t => t.ExtramuralEntryLoadId);
            
            CreateTable(
                "dbo.LoadingLists",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Comment = c.String(),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DDataStorages",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Semester = c.Int(nullable: false),
                        N = c.Int(nullable: false),
                        Subject = c.String(),
                        Faculty = c.String(),
                        Specialty = c.String(),
                        Specialize = c.String(),
                        Course = c.String(),
                        EduDegree = c.String(),
                        CountOfStud = c.Double(nullable: false),
                        CipherOfGroup = c.String(),
                        QuanOfGroupCritOne = c.Double(nullable: false),
                        RealQuanGr = c.Double(nullable: false),
                        QuanOfGroupCritTwo = c.String(),
                        QuanOfThread = c.Double(nullable: false),
                        TotalHour = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                        Lecture = c.Double(nullable: false),
                        Practice = c.Double(nullable: false),
                        Lab = c.Double(nullable: false),
                        IndWork = c.Double(nullable: false),
                        CourseProjects = c.String(),
                        Exam = c.String(),
                        Eval = c.String(),
                        CLecture = c.Double(nullable: false),
                        CPractice = c.Double(nullable: false),
                        CLab = c.Double(nullable: false),
                        CConsultInSem = c.Double(nullable: false),
                        CConsultForExam = c.Double(nullable: false),
                        CCheckOfTests = c.Double(nullable: false),
                        CKR_KP = c.Double(nullable: false),
                        CEval = c.Double(nullable: false),
                        CExam = c.Double(nullable: false),
                        CPracticePreparation = c.Double(nullable: false),
                        CDek = c.Double(nullable: false),
                        CStateExam = c.Double(nullable: false),
                        CManagedDiploma = c.Double(nullable: false),
                        COther = c.Double(nullable: false),
                        CTotal = c.Double(nullable: false),
                        CActive = c.Double(nullable: false),
                        LoadingListId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LoadingLists", t => t.LoadingListId)
                .Index(t => t.LoadingListId);
            
            CreateTable(
                "dbo.EDataStorages",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Semester = c.Int(nullable: false),
                        N = c.Int(nullable: false),
                        Subject = c.String(),
                        Specialty = c.String(),
                        Extramural = c.String(),
                        Course = c.Double(nullable: false),
                        QuanOfStud = c.Double(nullable: false),
                        QuanOfThread = c.Double(nullable: false),
                        CommonTime = c.Double(nullable: false),
                        Lecture = c.Double(nullable: false),
                        Practice = c.Double(nullable: false),
                        Lab = c.Double(nullable: false),
                        IndWork = c.Double(nullable: false),
                        Exam = c.String(),
                        Eval = c.String(),
                        Test = c.Double(nullable: false),
                        NormKR_KP = c.Double(nullable: false),
                        CLecture = c.Double(nullable: false),
                        CPractice = c.Double(nullable: false),
                        CLab = c.Double(nullable: false),
                        CConsultInSem = c.Double(nullable: false),
                        CConsultForExam = c.Double(nullable: false),
                        CVerifyingTest = c.Double(nullable: false),
                        CCourseProject = c.Double(nullable: false),
                        CEval = c.Double(nullable: false),
                        COralExam = c.Double(nullable: false),
                        CManagedDiploma = c.Double(nullable: false),
                        CDek = c.Double(nullable: false),
                        CVerifyingOfWrWork = c.Double(nullable: false),
                        CProtection = c.Double(nullable: false),
                        CTotal = c.Double(nullable: false),
                        CActive = c.Double(nullable: false),
                        LoadingListId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LoadingLists", t => t.LoadingListId)
                .Index(t => t.LoadingListId);
            
            CreateTable(
                "dbo.Specialties",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Code = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Specializes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Cipher = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExtramuralTeachLoads",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Semester = c.Int(nullable: false),
                        Specialty = c.String(),
                        Course = c.Double(nullable: false),
                        Lecture = c.Double(nullable: false),
                        Practice = c.Double(nullable: false),
                        Lab = c.Double(nullable: false),
                        ConsultInSemester = c.Double(nullable: false),
                        ConsultForExam = c.Double(nullable: false),
                        WrittenWork = c.Double(nullable: false),
                        CalcWorks = c.Double(nullable: false),
                        CourseProjects = c.Double(nullable: false),
                        Evaluation = c.Double(nullable: false),
                        OralExam = c.Double(nullable: false),
                        WrittenExam = c.Double(nullable: false),
                        VerifyingOfTest = c.Double(nullable: false),
                        ManagedDiploma = c.Double(nullable: false),
                        Dek = c.Double(nullable: false),
                        VerifyingOfWrittenWorks = c.Double(nullable: false),
                        Protection = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                        Active = c.Double(nullable: false),
                        SubjectId = c.String(maxLength: 128),
                        ExtramuralEntryLoadId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExtramuralEntryLoads", t => t.ExtramuralEntryLoadId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId)
                .Index(t => t.SubjectId)
                .Index(t => t.ExtramuralEntryLoadId);
            
            AddColumn("dbo.AspNetUsers", "DayTeachLoadId", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "ExtramuralTeachLoadId", c => c.String(maxLength: 128));
            AddColumn("dbo.Departments", "Code", c => c.Double());
            AddColumn("dbo.Faculties", "ShortName", c => c.String());
            CreateIndex("dbo.AspNetUsers", "DayTeachLoadId");
            CreateIndex("dbo.AspNetUsers", "ExtramuralTeachLoadId");
            AddForeignKey("dbo.AspNetUsers", "DayTeachLoadId", "dbo.DayTeachLoads", "Id");
            AddForeignKey("dbo.AspNetUsers", "ExtramuralTeachLoadId", "dbo.ExtramuralTeachLoads", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "ExtramuralTeachLoadId", "dbo.ExtramuralTeachLoads");
            DropForeignKey("dbo.ExtramuralTeachLoads", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.ExtramuralTeachLoads", "ExtramuralEntryLoadId", "dbo.ExtramuralEntryLoads");
            DropForeignKey("dbo.AspNetUsers", "DayTeachLoadId", "dbo.DayTeachLoads");
            DropForeignKey("dbo.DayTeachLoads", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.DayTeachLoads", "DayEntryLoadId", "dbo.DayEntryLoads");
            DropForeignKey("dbo.DayEntryLoads", "SpecializeId", "dbo.Specializes");
            DropForeignKey("dbo.ExtramuralEntryLoads", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.DayEntryLoads", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.ExtramuralEntryLoads", "SpecialtyId", "dbo.Specialties");
            DropForeignKey("dbo.DayEntryLoads", "SpecialtyId", "dbo.Specialties");
            DropForeignKey("dbo.ExtramuralEntryLoads", "LoadingListId", "dbo.LoadingLists");
            DropForeignKey("dbo.EDataStorages", "LoadingListId", "dbo.LoadingLists");
            DropForeignKey("dbo.DDataStorages", "LoadingListId", "dbo.LoadingLists");
            DropForeignKey("dbo.DayEntryLoads", "LoadingListId", "dbo.LoadingLists");
            DropForeignKey("dbo.ExtramuralSemesters", "ExtramuralEntryLoadId", "dbo.ExtramuralEntryLoads");
            DropForeignKey("dbo.ExtramuralEntryLoads", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.DayEntryLoads", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.DaySemesters", "DayEntryLoadId", "dbo.DayEntryLoads");
            DropForeignKey("dbo.DayEntryLoads", "CourseId", "dbo.Courses");
            DropIndex("dbo.ExtramuralTeachLoads", new[] { "ExtramuralEntryLoadId" });
            DropIndex("dbo.ExtramuralTeachLoads", new[] { "SubjectId" });
            DropIndex("dbo.EDataStorages", new[] { "LoadingListId" });
            DropIndex("dbo.DDataStorages", new[] { "LoadingListId" });
            DropIndex("dbo.ExtramuralSemesters", new[] { "ExtramuralEntryLoadId" });
            DropIndex("dbo.ExtramuralEntryLoads", new[] { "SpecialtyId" });
            DropIndex("dbo.ExtramuralEntryLoads", new[] { "SubjectId" });
            DropIndex("dbo.ExtramuralEntryLoads", new[] { "DepartmentId" });
            DropIndex("dbo.ExtramuralEntryLoads", new[] { "LoadingListId" });
            DropIndex("dbo.DaySemesters", new[] { "DayEntryLoadId" });
            DropIndex("dbo.DayEntryLoads", new[] { "SubjectId" });
            DropIndex("dbo.DayEntryLoads", new[] { "CourseId" });
            DropIndex("dbo.DayEntryLoads", new[] { "SpecializeId" });
            DropIndex("dbo.DayEntryLoads", new[] { "SpecialtyId" });
            DropIndex("dbo.DayEntryLoads", new[] { "DepartmentId" });
            DropIndex("dbo.DayEntryLoads", new[] { "LoadingListId" });
            DropIndex("dbo.DayTeachLoads", new[] { "DayEntryLoadId" });
            DropIndex("dbo.DayTeachLoads", new[] { "SubjectId" });
            DropIndex("dbo.AspNetUsers", new[] { "ExtramuralTeachLoadId" });
            DropIndex("dbo.AspNetUsers", new[] { "DayTeachLoadId" });
            DropColumn("dbo.Faculties", "ShortName");
            DropColumn("dbo.Departments", "Code");
            DropColumn("dbo.AspNetUsers", "ExtramuralTeachLoadId");
            DropColumn("dbo.AspNetUsers", "DayTeachLoadId");
            DropTable("dbo.ExtramuralTeachLoads");
            DropTable("dbo.Specializes");
            DropTable("dbo.Subjects");
            DropTable("dbo.Specialties");
            DropTable("dbo.EDataStorages");
            DropTable("dbo.DDataStorages");
            DropTable("dbo.LoadingLists");
            DropTable("dbo.ExtramuralSemesters");
            DropTable("dbo.ExtramuralEntryLoads");
            DropTable("dbo.DaySemesters");
            DropTable("dbo.Courses");
            DropTable("dbo.DayEntryLoads");
            DropTable("dbo.DayTeachLoads");
        }
    }
}
