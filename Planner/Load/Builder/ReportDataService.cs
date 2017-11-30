
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Domain.Models;

namespace Load.Builder
{
    public static class Ext
    {
        
    }

    public class ReportDataService
    {
        public List<DayFormatData> GetDayFormatData(string loadingId, byte semester)
        {
            List<DayFormatData> data;
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                data = (from del in ctx.DayEntryLoads
                        join sb in ctx.Subjects on del.SubjectId equals sb.Id
                        join spt in ctx.Specialties on del.SpecialtyId equals spt.Id
                        join spl in ctx.Specializes on del.SpecializeId equals spl.Id
                        join cs in ctx.Courses on del.CourseId equals cs.Id
                        join des in ctx.DaySemesters on del.Id equals des.DayEntryLoadId
                        join ll in ctx.LoadingLists on del.LoadingListId equals ll.Id
                        where (del.LoadingListId == loadingId && des.Semester == semester)
                        select new DayFormatData()
                        {
                            Year = ll.Year,
                            SubjectName = sb.Name,
                            Faculty = del.FacultyName,
                            Specialty = spt.Code,
                            Specialization = spl.Cipher,
                            Course = cs.Literal,
                            StudentsCount = del.QuantityOfStudents,
                            GroupsCipher = del.CipherOfGroups,
                            RealQuantityOfGroups = del.RealQuantityOfGroups,
                            QuantityOfThreads = del.QuantityOfThreads,

                            InLectures = semester == 1 ? del.F_Lectures : del.S_Lectures,
                            InLabs = semester == 1 ? del.F_Labs : del.S_Labs,
                            InPractices = semester == 1 ? del.F_Practical : del.S_Practical,

                            InIndependentWorks = semester == 1 ? del.F_IndividualWork : del.S_IndividualWork,
                            InCourseProjects = semester == 1 ? del.F_CourseProjects : del.S_CourseProjects,
                            InExams = semester == 1 ? del.F_Exams : del.S_Exams,
                            InEvaluations = semester == 1 ? del.F_Evaluation : del.S_Evaluation,

                            OutLectures = des.Lecture,
                            OutPractices = des.Practice,
                            OutLabs = des.Lab,
                            OutSeminars = des.Semester,
                            OutIndLessons = 0,
                            OutConsultInSemesters = des.ConsultInSemester,
                            OutConsultForExams = des.ConsultForExam,
                            OutVerifyingOfTestsA = des.VerifyingOfTests,
                            OutVerifyingOfTestsB = 0,
                            OutAnalitycalWorks = 0,
                            OutCalculatedWorks = 0,
                            OutKrKp = des.KR_KP,
                            OutControlEvaluations = des.ControlEvaluation,
                            OutControlExams = des.ControlExam,
                            OutPracticePreparations = des.PracticePreparation,
                            OutDeks = des.Dek,
                            OutStateExams = des.StateExam,
                            OutManagedDiplomas = des.ManagedDiploma,
                            OutOther = des.Other,
                            OutActive = des.Active,
                            OutTotal = des.Total 

                        }).Where(s => s.Faculty == "ЕІ").ToList();
            }
            return data;
        }
        public List<ExtraFormatData> GetExtraFormatData(string loadingId, byte semester)
        {
            List<ExtraFormatData> data;
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                data = (from eel in ctx.ExtramuralEntryLoads
                        join sb in ctx.Subjects on eel.SubjectId equals sb.Id
                        join spt in ctx.Specialties on eel.SpecialtyId equals spt.Id
                        join es in ctx.ExtramuralSemesters on eel.Id equals es.ExtramuralEntryLoadId
                        join ll in ctx.LoadingLists on eel.LoadingListId equals ll.Id
                        where (eel.LoadingListId == loadingId && es.Semester == semester)
                        select new ExtraFormatData()
                        {
                            Year = ll.Year,
                            Subject = sb.Name,
                            Specialty = spt.Code,
                            Course = eel.Course,
                            QuantityOfStudents = eel.QuantityOfStudents,
                            GroupsCipher = "",
                            QuantityOfGroups = eel.QuantityOfGroups,
                            QuantityOfThreads = eel.QuantityOfThreads,

                            InLectures = semester == 1 ? eel.F_Lecture : eel.S_Lecture,
                            InPractices = semester == 1 ? eel.F_Practical : eel.S_Practical,
                            InLabs = semester == 1 ? eel.F_Lab : eel.S_Lab,
                            InIndependentWorks = semester == 1 ? eel.F_IndividualWork : eel.S_IndividualWork,
                            InEvaluation = semester == 1 ? eel.F_Evaluation : eel.S_Evaluation,
                            InExam = semester == 1 ? eel.F_Exam : eel.S_Exam,
                            InProjects = semester == 1 ? eel.F_KR : eel.S_KR,
                            InTest = semester == 1 ? eel.F_Test : eel.S_Test,
                            InLimitOnProjects = semester == 1 ? eel.F_LimitOnProjects : eel.S_LimitOnProjects,

                            OutLectures = es.Lecture,
                            OutPractices = es.Practice,
                            OutLabs = es.Lab,
                            OutSemesterConsults = es.ConsultInSemester,
                            OutExamConsults = es.ConsultForExam,
                            OutAnalitycalWorks = es.WrittenWork,
                            OutCalculatedWorks = es.CalcWorks,
                            OutProjects = es.CourseProjects,
                            OutEvaluation = es.Evaluation,
                            OutOralExams = es.OralExam,
                            OutWrittenExams = es.WrittenExam,
                            OutCheckingTest = es.VerifyingOfTest,
                            OutDiplomManagement = es.ManagedDiploma,
                            OutDekParticipation = es.Dek,
                            OutCheckWriteWorks = es.VerifyingOfWrittenWorks,
                            OutProtection = es.Protection,
                            OutTotal = es.Total,
                            OutActive = es.Active

                        }).ToList();
            }
            return data;
        }

        public DayFormatData GetDayFormatSummary(List<DayFormatData> dayData)
        {
            DayFormatData summary = new DayFormatData();

            summary.OutLectures = dayData.Sum(d => d.OutLectures);
            summary.OutPractices = dayData.Sum(d => d.OutPractices);
            summary.OutLabs = dayData.Sum(d => d.OutLabs);
            summary.OutSeminars = dayData.Sum(d => d.OutSeminars);
            summary.OutIndLessons = dayData.Sum(d => d.OutIndLessons);
            summary.OutConsultInSemesters = dayData.Sum(d => d.OutConsultInSemesters);
            summary.OutConsultForExams = dayData.Sum(d => d.OutConsultForExams);
            summary.OutVerifyingOfTestsA = dayData.Sum(d => d.OutVerifyingOfTestsA);
            summary.OutVerifyingOfTestsB = dayData.Sum(d => d.OutVerifyingOfTestsB);
            summary.OutAnalitycalWorks = dayData.Sum(d => d.OutAnalitycalWorks);
            summary.OutCalculatedWorks = dayData.Sum(d => d.OutCalculatedWorks);
            summary.OutKrKp = dayData.Sum(d => d.OutKrKp);
            summary.OutControlEvaluations = dayData.Sum(d => d.OutControlEvaluations);
            summary.OutControlExams = dayData.Sum(d => d.OutControlExams);
            summary.OutPracticePreparations = dayData.Sum(d => d.OutPracticePreparations);
            summary.OutDeks = dayData.Sum(d => d.OutDeks);
            summary.OutStateExams = dayData.Sum(d => d.OutStateExams);
            summary.OutManagedDiplomas = dayData.Sum(d => d.OutManagedDiplomas);
            summary.OutOther = dayData.Sum(d => d.OutOther);
            summary.OutTotal = dayData.Sum(d => d.OutTotal);
            summary.OutActive = dayData.Sum(d => d.OutActive);

            return summary;
        }

        public ExtraFormatData GetExtraFormatSummary(List<ExtraFormatData> extraData)
        {
            ExtraFormatData summary = new ExtraFormatData();

            summary.OutLectures = extraData.Sum(d => d.OutLectures);
            summary.OutPractices = extraData.Sum(d => d.OutPractices);
            summary.OutLabs = extraData.Sum(d => d.OutLabs);
            summary.OutSeminar = extraData.Sum(d => d.OutSeminar);
            summary.OutIndLessons = extraData.Sum(d => d.OutIndLessons);
            summary.OutSemesterConsults = extraData.Sum(d => d.OutSemesterConsults);
            summary.OutExamConsults = extraData.Sum(d => d.OutExamConsults);
            summary.OutWrittenWorks = extraData.Sum(d => d.OutWrittenWorks);
            summary.OutVerifyingOfTestsA = extraData.Sum(d => d.OutVerifyingOfTestsA);
            summary.OutVerifyingOfTestsB = extraData.Sum(d => d.OutVerifyingOfTestsB);
            summary.OutVerifyingOfTestsB = extraData.Sum(d => d.OutVerifyingOfTestsB);
            summary.OutAnalitycalWorks = extraData.Sum(d => d.OutAnalitycalWorks);
            summary.OutProjects = extraData.Sum(d => d.OutProjects);
            summary.OutEvaluation = extraData.Sum(d => d.OutEvaluation);
            summary.OutExam = extraData.Sum(d => d.OutExam);
            summary.OutPracticePreparation = extraData.Sum(d => d.OutPracticePreparation);
            summary.OutDiplomManagement = extraData.Sum(d => d.OutDiplomManagement);
            summary.OutStateExam = extraData.Sum(d => d.OutStateExam);
            summary.OutProtection = extraData.Sum(d => d.OutProtection);
            summary.OutCheckingTest = extraData.Sum(d => d.OutCheckingTest);
            summary.OutAspirants = extraData.Sum(d => d.OutAspirants);
            summary.OutTotal = extraData.Sum(d => d.OutTotal);
            summary.OutActive = extraData.Sum(d => d.OutActive);

            //

            summary.OutOralExams = extraData.Sum(d => d.OutOralExams);
            summary.OutWrittenExams = extraData.Sum(d => d.OutWrittenExams);
            summary.OutDekParticipation = extraData.Sum(d => d.OutDekParticipation);
            summary.OutCheckWriteWorks = extraData.Sum(d => d.OutCheckWriteWorks);

            return summary;
        }

        public List<DayTeachLoadsFormatData> GetDayTeachLoadFormatData(string loadingId, byte semester)
        {
            List<DayTeachLoadsFormatData> data;
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                data = (from del in ctx.DayEntryLoads
                        join sb in ctx.Subjects on del.SubjectId equals sb.Id
                        join spt in ctx.Specialties on del.SpecialtyId equals spt.Id
                        join spl in ctx.Specializes on del.SpecializeId equals spl.Id
                        join cs in ctx.Courses on del.CourseId equals cs.Id
                        join das in ctx.DaySemesters on del.Id equals das.DayEntryLoadId
                        join dt in ctx.DayTeachLoads on del.Id equals dt.DayEntryLoadId
                        join us in ctx.Users on dt.ApplicationUserId equals us.Id
                        join ll in ctx.LoadingLists on del.LoadingListId equals ll.Id
                        where (del.LoadingListId == loadingId && dt.Semester == semester)
                        select new DayTeachLoadsFormatData()
                        {
                            #region Fields
                            Year = ll.Year,
                            Specialization = spl.Cipher,
                            Active = dt.Active,
                            LastName = us.LastName,
                            FirstName = us.FirstName.Substring(0, 1),
                            ThirdName = us.ThirdName.Substring(0, 1),
                            CourseLiteral = cs.Literal,
                            EducationDegree = del.EducationDegree,
                            StudentsCount = del.QuantityOfStudents,
                            GroupsCipher = del.CipherOfGroups,
                            CalcWorks = dt.CalcWorks,
                            ConsultForExam = dt.ConsultForExam, 
                            ConsultInSemester = dt.ConsultInSemester, 
                            Course = dt.Course,
                            CourseProjects = dt.CourseProjects,  
                            Dek = dt.Dek,
                            Evaluation = dt.Evaluation,
                            Lab = dt.Lab, 
                            Lecture = dt.Lecture, 
                            ManagedDiploma = dt.ManagedDiploma,
                            OralExam = dt.OralExam,
                            Practice = dt.Practice, 
                            Protection = dt.Protection,
                            Semester = dt.Semester, 
                            Specialty = spt.Code,
                            SubjectName = sb.Name,
                            Total = dt.Total,
                            VerifyingOfTest = dt.VerifyingOfTest, 
                            VerifyingOfWrittenWorks = dt.VerifyingOfWrittenWorks,
                            WrittenExam = dt.WrittenExam,
                            WrittenWork = dt.WrittenWork 
                            #endregion
                        }).Distinct().ToList();
            }
            return data;
        }

        public List<ExtramuralTeachLoadsFormatData> GetExtramuralTeachLoadsFormatData(string loadingId, byte semester)
        {
            List<ExtramuralTeachLoadsFormatData> data;
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                data = (from eel in ctx.ExtramuralEntryLoads
                        join sb in ctx.Subjects on eel.SubjectId equals sb.Id
                        join spt in ctx.Specialties on eel.SpecialtyId equals spt.Id
                        join es in ctx.ExtramuralSemesters on eel.Id equals es.ExtramuralEntryLoadId
                        join ext in ctx.ExtramuralTeachLoads on eel.Id equals ext.ExtramuralEntryLoadId
                        join dep in ctx.Departments on eel.DepartmentId equals dep.Id
                        join us in ctx.Users on ext.ApplicationUserId equals us.Id
                        join ll in ctx.LoadingLists on eel.LoadingListId equals ll.Id
                        where (eel.LoadingListId == loadingId && ext.Semester == semester)
                        select new ExtramuralTeachLoadsFormatData()
                        {
                            #region Fields
                            Year = ll.Year,
                            Active = ext.Active,
                            LastName = us.LastName,
                            FirstName = us.FirstName.Substring(0, 1),
                            ThirdName = us.ThirdName.Substring(0, 1),
                            CourseLiteral = eel.Course,
                            StudentsCount = eel.QuantityOfStudents,
                            CalcWorks = ext.CalcWorks,
                            ConsultForExam = ext.ConsultForExam,
                            ConsultInSemester = ext.ConsultInSemester,
                            Course = ext.Course,
                            CourseProjects = ext.CourseProjects,
                            Dek = ext.Dek,
                            Evaluation = ext.Evaluation,
                            Lab = ext.Lab,
                            Lecture = ext.Lecture,
                            ManagedDiploma = ext.ManagedDiploma,
                            OralExam = ext.OralExam,
                            Practice = ext.Practice,
                            Protection = ext.Protection,
                            Semester = ext.Semester,
                            Specialty = spt.Code,
                            SubjectName = sb.Name,
                            Total = ext.Total,
                            VerifyingOfTest = ext.VerifyingOfTest,
                            VerifyingOfWrittenWorks = ext.VerifyingOfWrittenWorks,
                            WrittenExam = ext.WrittenExam,
                            WrittenWork = ext.WrittenWork
                            #endregion
                        }).Distinct().ToList();
            }
            return data;
        }

    }

    public class DayFormatData
    {
        public int Year { get; set; }

        public string SubjectName { get; set; }
        public string Faculty { get; set; }
        public string Specialty { get; set; }
        public string Specialization { get; set; }
        public string Course { get; set; }
        public double StudentsCount { get; set; }
        public string GroupsCipher { get; set; }
        public double RealQuantityOfGroups { get; set; }
        public double QuantityOfThreads { get; set; }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public double InLectures { get; set; }
        public double InLabs { get; set; }
        public double InPractices { get; set; }
        public double InSeminars { get; set; } = 0; // [optional]
        public double InIndLessons { get; set; } = 0; // [optional]
        public double InPracticeWeeks { get; set; } = 0; // [optional] 
        public double InDiplomaProjects { get; set; } = 0; // [optional]
        public double InStateSertifications { get; set; } = 0; // [optional]    
        public double InIndependentWorks { get; set; }
        public string InCourseProjects { get; set; } // Kr Kp Dr
        public string InExams { get; set; }
        public string InEvaluations { get; set; }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public double OutLectures { get; set; }
        public double OutPractices { get; set; }
        public double OutLabs { get; set; }
        public double OutSeminars { get; set; } // [optional]
        public double OutIndLessons { get; set; } // [optional]
        public double OutConsultInSemesters { get; set; }
        public double OutConsultForExams { get; set; }
        public double OutVerifyingOfTestsA { get; set; } // audit
        public double OutVerifyingOfTestsB { get; set; } // [optional]
        public double OutAnalitycalWorks { get; set; } // [optional]
        public double OutCalculatedWorks { get; set; } // [optional]
        public double OutKrKp { get; set; }
        public double OutControlEvaluations { get; set; }
        public double OutControlExams { get; set; }
        public double OutPracticePreparations { get; set; }
        public double OutDeks { get; set; } // check
        public double OutStateExams { get; set; }
        public double OutManagedDiplomas { get; set; }
        public double OutOther { get; set; }
        public double OutTotal { get; set; }
        public double OutActive { get; set; }
    }
    public class ExtraFormatData
    {
        public int Year { get; set; }
        public string Subject { get; set; }
        public string Specialty { get; set; }
        public double Course { get; set; }
        public double QuantityOfStudents { get; set; }
        public string GroupsCipher { get; set; }
        public double QuantityOfGroups { get; set; }
        public double QuantityOfThreads { get; set; }

        public double InLectures { get; set; }
        public double InPractices { get; set; }
        public double InLabs { get; set; }
        public double InSeminar { get; set; } = 0; // [optional]
        public double InIndLessons { get; set; } = 0; // [optional]
        public double InPracticeWeeks { get; set; } = 0; // [optional] 
        public double InIndependentWorks { get; set; }
        public double InDiplomaProjects { get; set; } = 0;
        public string InEvaluation { get; set; }
        public string InProjects { get; set; } // Kr Kp Dr
        public string InExam { get; set; }
        public double InTest { get; set; }
        public double InLimitOnProjects { get; set; }


        public double OutLectures { get; set; }
        public double OutPractices { get; set; }
        public double OutLabs { get; set; }
        public double OutSeminar { get; set; } // [optional]
        public double OutIndLessons { get; set; } // [optional]
        public double OutSemesterConsults { get; set; }
        public double OutExamConsults { get; set; }
        public double OutWrittenWorks { get; set; }
        public double OutVerifyingOfTestsA { get; set; } // audit
        public double OutVerifyingOfTestsB { get; set; } // [optional]
        public double OutAnalitycalWorks { get; set; } // [optional]
        public double OutCalculatedWorks { get; set; }
        public double OutProjects { get; set; }
        public double OutEvaluation { get; set; }
        public double OutExam { get; set; }
        public double OutPracticePreparation { get; set; }
        public double OutDiplomManagement { get; set; }
        public double OutStateExam { get; set; }
        public double OutProtection { get; set; }
        public double OutCheckingTest { get; set; }
        public double OutAspirants { get; set; }
        public double OutTotal { get; set; }
        public double OutActive { get; set; }

        //  
        public double OutOralExams { get; set; }
        public double OutWrittenExams { get; set; }
        public double OutDekParticipation { get; set; }
        public double OutCheckWriteWorks { get; set; }

    }


    public class DayTeachLoadsFormatData
    {
        public int Year { get; set; }

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

    public class ExtramuralTeachLoadsFormatData
    {
        public int Year { get; set; }

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
