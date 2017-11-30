using System.Collections.Generic;
using System.Linq;
using Domain.Models;

namespace Load.Services
{
    public class ReportDataService
    {
        public List<DayFormatData> GetDayFormatData(string loadingId, byte semester)
        {
            List<DayFormatData> data;
            using(ApplicationDbContext ctx =  new ApplicationDbContext())
            {
                data = (from del in ctx.DayEntryLoads
                        join sb in ctx.Subjects on del.SubjectId equals sb.Id
                        join spt in ctx.Specialties on del.SpecialtyId equals spt.Id
                        join spl in ctx.Specializes on del.SpecializeId equals spl.Id
                        join cs in ctx.Courses on del.CourseId equals cs.Id
                        join des in ctx.DaySemesters on del.Id equals des.DayEntryLoadId
                        where (del.LoadingListId == loadingId && des.Semester == semester)
                        select new DayFormatData()
                        {
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
                            InCourseProject = semester == 1 ? del.F_CourseProjects : del.S_CourseProjects,
                            InExam = semester == 1 ? del.F_Exams : del.S_Exams,
                            InEvaluation = semester == 1 ? del.F_Evaluation : del.S_Evaluation

                        }).ToList();
            }
            return data;
        }

        public List<ExtraFormatData> GetExtraFormatData(string loadingId, byte semester)
        {
            List<ExtraFormatData> data;
            using(ApplicationDbContext ctx = new ApplicationDbContext())
            {
                data = (from eel in ctx.ExtramuralEntryLoads
                       join sb in ctx.Subjects on eel.SubjectId equals sb.Id
                       join spt in ctx.Specialties on eel.SpecialtyId equals spt.Id
                       join es in ctx.ExtramuralSemesters on eel.Id equals es.ExtramuralEntryLoadId
                       where (eel.LoadingListId == loadingId && es.Semester == semester)
                       select new ExtraFormatData() {
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
                            InProjects = semester == 1? eel.F_KR : eel.S_KR,
                            InTest = semester == 1 ? eel.F_Test : eel.S_Test,
                            InLimitOnProjects = semester == 1 ? eel.F_LimitOnProjects : eel.S_LimitOnProjects,

                            OutLectures = es.Lecture,                        
                            OutPractices = es.Practice,
                            OutLabs = es.Lab,
                            OutSemesterConsults = es.ConsultInSemester,
                            OutExamConsults = es.ConsultForExam,
                            OutWrittenWorks = es.WrittenWork,
                            OutAnalitycalWorks = es.CalcWorks,
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
    }

    public class DayFormatData
    {
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
        public double InSeminar { get; set; } = 0; // [optional]
        public double InIndLessons { get; set; } = 0; // [optional]
        public double InPracticeWeeks { get; set; } = 0; // [optional] 
        public double DiplomaProjects { get; set; } = 0; // [optional]
        public double StateSertification { get; set; } = 0; // [optional]    
        public double InIndependentWorks { get; set; }
        public string InCourseProject { get; set; } // Kr Kp Dr
        public string InExam { get; set; }
        public string InEvaluation { get; set; }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public double OutLecture { get; set; }
        public double OutPractice { get; set; }
        public double OutLab { get; set; }
        public double OutSeminar { get; set; } // [optional]
        public double OutIndLessons { get; set; } // [optional]
        public double OutConsultInSemester { get; set; }
        public double OutConsultForExam { get; set; }
        public double OutVerifyingOfTestsA { get; set; } // audit
        public double OutVerifyingOfTestsB { get; set; } // [optional]
        public double OutAnalitycalWorks { get; set; } // [optional]
        public double OutCalculatedWorks { get; set; } // [optional]
        public double OutKrKp { get; set; }
        public double OutControlEvaluation { get; set; }
        public double OutControlExam { get; set; }
        public double OutPracticePreparation { get; set; }
        public double OutDek { get; set; }
        public double OutStateExam { get; set; }
        public double OutManagedDiploma { get; set; }
        public double OutOther { get; set; }
        public double OutTotal { get; set; }
        public double OutActive { get; set; }
    }

    public class ExtraFormatData
    {
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
        public double OutProjects { get; set; }
        public double OutEvaluation { get; set; }
        public string OutExam { get; set; }
        public double OutPracticePreparation { get; set; }
        public double OutDiplomManagement { get; set; }
        public double OutStateExam { get; set; }
        public double OutProtection { get; set; }
        public double OutCheckingTest { get; set; }
        public string OutAspirants { get; set; }
        public double OutTotal { get; set; }
        public double OutActive { get; set; }

        //  
        public double OutOralExams { get; set; }
        public double OutWrittenExams { get; set; }
        public double OutDekParticipation { get; set; }
        public double OutCheckWriteWorks { get; set; }

    }
}
