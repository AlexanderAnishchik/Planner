using System;

namespace Planner.Models
{
    public class DayEntryViewModel
    {
        #region Common part
        public string DayEntryId { get; set; }
        public string Faculty { get; set; }
        public string Specialty { get; set; }
        public string Specialization { get; set; }
        public string Course { get; set; }
        public string EducationDegree { get; set; }
        public double StudentsCount { get; set; }
        public double ForeignersCount { get; set; }
        public string GroupsCipher { get; set; }
        public double QuantityOfGroupsA { get; set; }
        public double RealQuantityOfGroups { get; set; }
        public double QuantityOfGroupsB { get; set; }
        public double QuantityOfThreads { get; set; }
        public string ConflatedThreads { get; set; }
        public string Notes { get; set; }
        public string Subject { get; set; }
        public double QuantityOfCredits { get; set; }
        public double Hours { get; set; }
        public string Language { get; set; }
        public double QuantityOfWeeksFs { get; set; }
        public double QuantityOfWeeksSs { get; set; }
        public double CoeficientFs { get; set; }
        public double CoeficientSs { get; set; }
        public string DepartmentCipher { get; set; }
        public double Projects { get; set; }
        public double Practices { get; set; }
        public double QuantityOfMembers { get; set; }
        public String DayTeachId { get; set; }

        #endregion

        #region Additional part
        public DayEntrySemester DeS { get; set; }
        #endregion

        #region Calculated part
        public string DaySemesterId { get; set; }
        public DayDistribution Dd { get; set; }
        #endregion
    }
    public class DayEntrySemester
    {
        public double TotalHours { get; set; }
        public double Total { get; set; }
        public double Lectures { get; set; }
        public double Labs { get; set; }
        public double Practices { get; set; }
        public double IndependentWorks { get; set; }
        public string Courses { get; set; }
        public string Exam { get; set; }
        public string Evaluation { get; set; }
    }
    public class DayDistribution
    {
        public byte Semester { get; set; }
        public double Lecture { get; set; }
        public double Practice { get; set; }
        public double Lab { get; set; }
        public double ConsultInSemester { get; set; }
        public double ConsultForExam { get; set; }
        public double VerifyingOfTests { get; set; }
        public double KR_KP { get; set; }
        public double ControlEvaluation { get; set; }
        public double ControlExam { get; set; }
        public double PracticePreparation { get; set; }
        public double Dek { get; set; }
        public double StateExam { get; set; }
        public double ManagedDiploma { get; set; }
        public double Other { get; set; }
        public double Total { get; set; }
        public double Active { get; set; }
        public double EnglishBonus { get; set; }
    }

    public class ExtramuralEntryViewModel
    {
        #region Common part
        public string ExtramuralEntryId { get; set; }
        public string DepartmentCipher { get; set; }
        public string Subject { get; set; }
        public string Specialty { get; set; }
        public string Extramural { get; set; }
        public double Course { get; set; }
        public double QuantityOfStudents { get; set; }
        public double QuantityOfGroups { get; set; }
        public double QuantityOfThreads { get; set; }
        public double NumOfThread { get; set; }
        public string MajorSpecialty { get; set; }
        public double CommonTime { get; set; }
        public double Credits { get; set; }
        #endregion

        #region Additional part
        public ExtraEntrySemester EeS { get; set; }
        #endregion

        #region Calculated part
        public string ExtramuralSemesterId { get; set; }
        public ExtraDistribution Ed { get; set; }
        #endregion

    }
    public class ExtraEntrySemester
    {
        public double Lectures { get; set; }
        public double Labs { get; set; }
        public double Practices { get; set; }
        public double IndependentWorks { get; set; }
        public string Exam { get; set; }
        public string Evaluation { get; set; }
        public string Projects { get; set; }
        public double Test { get; set; }
        public double LimitOnProjects { get; set; }
    }
    public class ExtraDistribution
    {
        public int Semester { get; set; }
        #region Cut version for 1 semester
        public double Lectures { get; set; }
        public double Practices { get; set; }
        public double Labs { get; set; }
        public double SemesterConsults { get; set; }
        public double ExamConsults { get; set; }
        public double WrittenWorks { get; set; }
        public double AnalyticalWorks { get; set; }
        public double Projects { get; set; }
        public double Evaluation { get; set; }
        public double OralExams { get; set; }
        public double WrittenExams { get; set; }
        public double Total { get; set; }
        public double Active { get; set; }
        #endregion

        #region Full version for 2 semester
        public double CheckingTests { get; set; }
        public double DiplomManagement { get; set; }
        public double DekParticipation { get; set; }
        public double CheckWriteWorks { get; set; }
        public double Protection { get; set; }
        #endregion
    }

    public class DaySemesterDistributionViewModel
    {
        // navigation
        public string DayEntryId { get; set; }
        public string DaySemesterId { get; set; } // we need this one to up/down calculated part during the distribution

        // header
        public string Faculty { get; set; }
        public string Specialty { get; set; }
        public string Specialization { get; set; }
        public string Course { get; set; }
        public string EducationDegree { get; set; }
        public string Subject { get; set; }
        public byte Semester { get; set; }

        public DayDistribution DistributedSemester { get; set; }
    }
    public class ExtraSemesterDistributionViewModel
    {
        //navigation
        public string ExtramuralEntryId { get; set; }
        public string ExtramuralSemesterId { get; set; }

        // header
        public string Subject { get; set; }
        public string Specialty { get; set; }
        public string Extramural { get; set; }
        public double Course { get; set; }

        public ExtraDistribution DistributedSemester { get; set; }
    }

    public class DayTeachLoads
    {
        public String DayTeachLoadId { get; set; }

        public int Semester { get; set; }
        public string Specialty { get; set; }
        public double Course { get; set; }
        public double Lecture { get; set; }
        public double Practice { get; set; }
        public double Lab { get; set; }
        public double ConsultInSemester { get; set; }
        public double ConsultForExam { get; set; }
        public double WrittenWork { get; set; }
        public double CalcWorks { get; set; }
        public double CourseProjects { get; set; }
        public double Evaluation { get; set; }
        public double OralExam { get; set; }
        public double WrittenExam { get; set; }
        public double VerifyingOfTest { get; set; }
        public double ManagedDiploma { get; set; }
        public double Dek { get; set; }
        public double VerifyingOfWrittenWorks { get; set; }
        public double Protection { get; set; }
        public double Total { get; set; }
        public double Active { get; set; }

        public String SubjectName { get; set; }
        public String DayEntryLoadId { get; set; }
        public String LastName { get; set; }
        public String FirstName { get; set; }
        public String ThirdName { get; set; }
        public String Specialization { get; set; }
        public String CourseLiteral { get; set; }
        public String EducationDegree { get; set; }
        public double StudentsCount { get; set; }
        public String GroupsCipher { get; set; }
    }

    public class ExtramuralTeachLoads
    {
        public String ExtramuralTeachLoadsId { get; set; }
        public int Semester { get; set; }
        public string Specialty { get; set; }
        public double Course { get; set; }
        public double Lecture { get; set; }
        public double Practice { get; set; }
        public double Lab { get; set; }
        public double ConsultInSemester { get; set; }
        public double ConsultForExam { get; set; }
        public double WrittenWork { get; set; }
        public double CalcWorks { get; set; }
        public double CourseProjects { get; set; }
        public double Evaluation { get; set; }
        public double OralExam { get; set; }
        public double WrittenExam { get; set; }
        public double VerifyingOfTest { get; set; }
        public double ManagedDiploma { get; set; }
        public double Dek { get; set; }
        public double VerifyingOfWrittenWorks { get; set; }
        public double Protection { get; set; }
        public double Total { get; set; }
        public double Active { get; set; }
        public String SubjectName { get; set; }
        public String ExtramuralEntryLoadId { get; set; }

        public String LastName { get; set; }
        public String FirstName { get; set; }
        public String ThirdName { get; set; }
        public double StudentsCount { get; set; }
        public double CourseLiteral { get; set; }
    }
}