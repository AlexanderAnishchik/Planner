using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Domain.Models;
using Load;
using Load.Builder;
using Load.Mapper.SemesterFormat;
using Planner.Models;

namespace Planner.Controllers
{
    [Authorize]
    public class DistributionController : Controller
    {
        private readonly DataManager _dataManager = new DataManager();
        private readonly ReportService _reportService = new ReportService();

        [HttpGet]
        public ActionResult Index() => View();
        [HttpGet]
        public ActionResult DayEntryLoad() => View();
        [HttpGet]
        public ActionResult ExtramuralEntryLoad() => View();
        [HttpGet]
        public ActionResult UniWorkersDistributedLoad() => View();
        [HttpGet]
        public ActionResult UploadEntry() => View();
        [HttpPost]
        public ActionResult UploadEntry(HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string creationTime = DateTime.Today.ToString("dd-MM-yyyy");
                    string extFileName = String.Concat(creationTime, " ", fileName);
                    string serverPathToFile = Path.Combine(Server.MapPath("~/UploadedFiles"), extFileName);

                    file.SaveAs(serverPathToFile);
                    _dataManager.UploadEntryFile(serverPathToFile);
                }
                ViewBag.UploadResult = "Файл успешно загружен!";

                return RedirectToAction("Index");
            }
            catch (IOException exception)
            {
                ViewBag.UploadResult =
                    "Во время загрузки файла произошла ошибка! Обратитесь к администратору приложения.";
                Debug.WriteLine($" UploadEntry: {exception.Message}");
                return View();
            }
        }
        public ActionResult TeachLoad() => View();

        #region Reports

        public FileResult DownloadCommonDayFormatReport(string loadingId)
        {
            var report = _reportService.Construct(ReportType.CommonDayFormatReport, new EntryQueryContext { Loading = loadingId, Semester = (byte)SemesterType.Both });
            var stream = new MemoryStream();
            report.Document.SaveAs(stream);
            stream.Position = 0;

            return File(stream, report.Schema, report.Name);
        }

        [HttpGet]
        public ActionResult DownloadDayFormatReportBySemester(string loadingId, int semester)
        {
            var report = _reportService.Construct(ReportType.CommonDayFormatReport, new EntryQueryContext { Loading = loadingId, Semester = (byte)semester });
            var stream = new MemoryStream();
            report.Document.SaveAs(stream);
            stream.Position = 0;

            return File(stream, report.Schema, report.Name);
        }

        [HttpGet]
        public ActionResult DownloadCommonExtraFormatReport(string loadingId)
        {
            var report = _reportService.Construct(ReportType.CommonExtraFormatReport, new EntryQueryContext { Loading = loadingId, Semester = (byte)SemesterType.Both});
            var stream = new MemoryStream();
            report.Document.SaveAs(stream);
            stream.Position = 0;

            return File(stream, report.Schema, report.Name);
        }

        [HttpGet]
        public ActionResult DownloadExtraFormatReportBySemester(string loadingId, int semester)
        {
            var report = _reportService.Construct(ReportType.CommonExtraFormatReport, new EntryQueryContext { Loading = loadingId, Semester = (byte)semester });
            var stream = new MemoryStream();
            report.Document.SaveAs(stream);
            stream.Position = 0;

            return File(stream, report.Schema, report.Name);
        }

        [HttpGet]
        public ActionResult DownloadDayTeachLoadReport(string loadingId)
        {
            var report = _reportService.Construct(ReportType.TeacherDayLoading, new EntryQueryContext { Loading = loadingId, Semester = (byte)SemesterType.Both });
            var stream = new MemoryStream();
            report.Document.SaveAs(stream);
            stream.Position = 0;

            return File(stream, report.Schema, report.Name);
        }

        [HttpGet]
        public ActionResult DownloadExtraTeachLoadReport(string loadingId)
        {
            var report = _reportService.Construct(ReportType.TeacherExtraLoading, new EntryQueryContext { Loading = loadingId, Semester = (byte)SemesterType.Both });
            var stream = new MemoryStream();
            report.Document.SaveAs(stream);
            stream.Position = 0;

            return File(stream, report.Schema, report.Name);
        }
        #endregion

        #region Json

        [HttpGet]
        public JsonResult GetLoadings()
        {
            ApplicationDbContext ctx =   new ApplicationDbContext();
            
            var data = (from obj in ctx.LoadingLists
                orderby obj.Year
                select new {Id = obj.Id, Year = obj.Year, Comment = obj.Comment});

            return this.Json(data,JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetSubjects()
        {
            List<Subject> subjects;
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                subjects = context.Subjects.OrderBy(s => s.Name).Select(s => s).ToList();
            }
            return new JsonResult() { Data = subjects, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpGet]
        public JsonResult GetTeachers()
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var teachers = (from us in ctx.Users
                                //from tc in ctx.DepartmentUsers
                                //join dep in ctx.Departments on tc.DepartmentId equals dep.Id
                                //join us in ctx.Users on tc.UserId equals us.Id
                                //where tc.DepartmentId != null
                                select new
                                {
                                    Id = us.Id,
                                    FirstName = us.FirstName,
                                    LastName = us.LastName,
                                    ThirdName = us.ThirdName
                                }).Distinct().OrderBy(s=> s.LastName).ToList();

                return this.Json(teachers, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult GetDayEntryData(string loadingId, byte semester)
        {
            List<DayEntryViewModel> dayEntry;
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                dayEntry = (from del in ctx.DayEntryLoads
                    join sb in ctx.Subjects on del.SubjectId equals sb.Id
                    join spt in ctx.Specialties on del.SpecialtyId equals spt.Id
                    join spl in ctx.Specializes on del.SpecializeId equals spl.Id
                    join cs in ctx.Courses on del.CourseId equals cs.Id
                    join ds in ctx.DaySemesters on del.Id equals ds.DayEntryLoadId
                    where (del.LoadingListId == loadingId && ds.Semester == semester)
                    select new DayEntryViewModel()
                    {
                        #region Fields
                        DayEntryId = del.Id,
                        Faculty = del.FacultyName,
                        Specialty = spt.Code,
                        Specialization = spl.Cipher,
                        Course = cs.Literal,
                        EducationDegree = del.EducationDegree,
                        StudentsCount = del.QuantityOfStudents,
                        ForeignersCount = del.QuantityOfForeigners,
                        GroupsCipher = del.CipherOfGroups,
                        QuantityOfGroupsA = del.QuantityOfGroupsCritOne,
                        RealQuantityOfGroups = del.RealQuantityOfGroups,
                        QuantityOfGroupsB = del.QuantityOfGroupsCritTwo,
                        QuantityOfThreads = del.QuantityOfThreads,
                        ConflatedThreads = del.ConflatedThreads,
                        Notes = del.Note,
                        Subject = sb.Name,
                        QuantityOfCredits = del.CountOfCredits,
                        Hours = del.CountOfHours,
                        Language = del.Language,
                        QuantityOfWeeksFs = del.FS_CountOfWeeks,
                        QuantityOfWeeksSs = del.SS_CountOfWeeks,

                        DeS = new DayEntrySemester()
                        {
                            TotalHours = semester == (byte) SemesterType.First ? del.F_TotalHour : del.S_TotalHour,
                            Total = semester == (byte) SemesterType.First ? del.F_Total : del.S_Total,
                            Lectures = semester == (byte) SemesterType.First ? del.F_Lectures : del.S_Lectures,
                            Labs = semester == (byte) SemesterType.First ? del.F_Labs : del.S_Labs,
                            Practices = semester == (byte) SemesterType.First ? del.F_Practical : del.S_Practical,
                            IndependentWorks = semester == (byte) SemesterType.First ? del.F_IndividualWork : del.S_IndividualWork,
                            Courses = semester == (byte) SemesterType.First ? del.F_CourseProjects : del.S_CourseProjects,
                            Exam = semester == (byte) SemesterType.First ? del.F_Exams : del.S_Exams,
                            Evaluation = semester == (byte) SemesterType.First ? del.F_Evaluation : del.S_Evaluation
                        },

                        DaySemesterId = ds.Id,
                        Dd = new DayDistribution()
                        {
                            Semester = ds.Semester,
                            Lecture = ds.Lecture,
                            Practice = ds.Practice,
                            Lab = ds.Lab,
                            ConsultInSemester = ds.ConsultInSemester,
                            ConsultForExam = ds.ConsultForExam,
                            VerifyingOfTests = ds.VerifyingOfTests,
                            KR_KP = ds.KR_KP,
                            ControlEvaluation = ds.ControlEvaluation,
                            ControlExam = ds.ControlExam,
                            PracticePreparation = ds.PracticePreparation,
                            Dek = ds.Dek,
                            StateExam = ds.StateExam,
                            ManagedDiploma = ds.ManagedDiploma,
                            Other = ds.Other,
                            Total = ds.Total,
                            Active = ds.Active,
                            EnglishBonus = ds.EnglishBonus
                        },

                        CoeficientFs = del.FSemCoefficient,
                        CoeficientSs = del.SSemCoefficient,
                        DepartmentCipher = del.DepartmentCipher,
                        Projects = del.KR_KP_DR,
                        Practices = del.Practice,
                        QuantityOfMembers = del.QuantityOfDek
                        #endregion

                    }).ToList();
                return new JsonResult() { Data = dayEntry, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        [HttpGet]
        public JsonResult GetExtramuralEntryData(string loadingId, int semester)
        {
            List<ExtramuralEntryViewModel> extramuralEntry;
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                extramuralEntry = (from eel in ctx.ExtramuralEntryLoads
                                   join sb in ctx.Subjects on eel.SubjectId equals sb.Id
                                   join spt in ctx.Specialties on eel.SpecialtyId equals spt.Id
                                   join es in ctx.ExtramuralSemesters on eel.Id equals es.ExtramuralEntryLoadId
                                   where (eel.LoadingListId == loadingId && es.Semester == semester)
                                   select new ExtramuralEntryViewModel()
                                   {
                                       #region Fields
                                       ExtramuralEntryId = eel.Id,
                                       DepartmentCipher = eel.DepartmentCipher,
                                       Subject = sb.Name,
                                       Specialty = spt.Code,
                                       Extramural = eel.Extramural,
                                       Course = eel.Course,
                                       QuantityOfStudents = eel.QuantityOfStudents,
                                       QuantityOfGroups = eel.QuantityOfGroups,
                                       QuantityOfThreads = eel.QuantityOfThreads,
                                       NumOfThread = eel.NumOfThread,
                                       MajorSpecialty = eel.MajorSpecialty,
                                       CommonTime = eel.CommonTime,
                                       Credits = eel.Credits,

                                       EeS = new ExtraEntrySemester()
                                       {
                                           Lectures = semester == (byte)SemesterType.First ? eel.F_Lecture : eel.S_Lecture,
                                           Labs = semester == (byte)SemesterType.First ? eel.F_Lab : eel.S_Lab,
                                           Practices = semester == (byte)SemesterType.First ? eel.F_Practical : eel.S_Practical,
                                           IndependentWorks = semester == (byte)SemesterType.First ? eel.F_IndividualWork : eel.S_IndividualWork,
                                           Exam = semester == (byte)SemesterType.First ? eel.F_Exam : eel.S_Exam,
                                           Evaluation = semester == (byte)SemesterType.First ? eel.F_Evaluation : eel.S_Evaluation,
                                           Projects = semester == (byte)SemesterType.First ? eel.F_KR : eel.S_KR,
                                           Test = semester == (byte)SemesterType.First ? eel.F_Test : eel.S_Test,
                                           LimitOnProjects = semester == (byte)SemesterType.First ? eel.F_LimitOnProjects : eel.S_LimitOnProjects
                                       },

                                       ExtramuralSemesterId = es.Id,

                                       Ed = new ExtraDistribution()
                                       {
                                           Lectures = es.Lecture,
                                           Practices = es.Practice,
                                           Labs = es.Lab,
                                           SemesterConsults = es.ConsultInSemester,
                                           ExamConsults = es.ConsultForExam,
                                           WrittenWorks = es.WrittenWork,
                                           AnalyticalWorks = es.CalcWorks,
                                           Projects = es.CourseProjects,
                                           Evaluation = es.Evaluation,
                                           OralExams = es.OralExam,
                                           WrittenExams = es.WrittenExam,
                                           CheckingTests = es.VerifyingOfTest,
                                           DiplomManagement = es.ManagedDiploma,
                                           DekParticipation = es.Dek,
                                           CheckWriteWorks = es.VerifyingOfWrittenWorks,
                                           Protection = es.Protection,
                                           Total = es.Total,
                                           Active = es.Active,
                                           Semester = es.Semester,
                                           #endregion
                                       }

                                   }).ToList();
                return new JsonResult() { Data = extramuralEntry, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        //Extramural detail 
        [HttpGet]
        public JsonResult GetExtramuralDistributed(string ExtramuralEntryId, byte semester)
        {
            List<ExtramuralEntryViewModel> extramuralEntry;
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                extramuralEntry = (from eel in ctx.ExtramuralEntryLoads
                                   join sb in ctx.Subjects on eel.SubjectId equals sb.Id
                                   join spt in ctx.Specialties on eel.SpecialtyId equals spt.Id
                                   join es in ctx.ExtramuralSemesters on eel.Id equals es.ExtramuralEntryLoadId
                                   where (eel.Id == ExtramuralEntryId && es.Semester == semester)
                                   select new ExtramuralEntryViewModel()
                                   {
                                       #region Fields
                                       ExtramuralEntryId = eel.Id,
                                       DepartmentCipher = eel.DepartmentCipher,
                                       Subject = sb.Name,
                                       Specialty = spt.Code,
                                       Extramural = eel.Extramural,
                                       Course = eel.Course,
                                       QuantityOfStudents = eel.QuantityOfStudents,
                                       QuantityOfGroups = eel.QuantityOfGroups,
                                       QuantityOfThreads = eel.QuantityOfThreads,
                                       NumOfThread = eel.NumOfThread,
                                       MajorSpecialty = eel.MajorSpecialty,
                                       CommonTime = eel.CommonTime,
                                       Credits = eel.Credits,

                                       EeS = new ExtraEntrySemester()
                                       {
                                           Lectures = semester == (byte)SemesterType.First ? eel.F_Lecture : eel.S_Lecture,
                                           Labs = semester == (byte)SemesterType.First ? eel.F_Lab : eel.S_Lab,
                                           Practices = semester == (byte)SemesterType.First ? eel.F_Practical : eel.S_Practical,
                                           IndependentWorks = semester == (byte)SemesterType.First ? eel.F_IndividualWork : eel.S_IndividualWork,
                                           Exam = semester == (byte)SemesterType.First ? eel.F_Exam : eel.S_Exam,
                                           Evaluation = semester == (byte)SemesterType.First ? eel.F_Evaluation : eel.S_Evaluation,
                                           Projects = semester == (byte)SemesterType.First ? eel.F_KR : eel.S_KR,
                                           Test = semester == (byte)SemesterType.First ? eel.F_Test : eel.S_Test,
                                           LimitOnProjects = semester == (byte)SemesterType.First ? eel.F_LimitOnProjects : eel.S_LimitOnProjects
                                       },

                                       ExtramuralSemesterId = es.Id,

                                       Ed = new ExtraDistribution()
                                       {
                                           Lectures = es.Lecture,
                                           Practices = es.Practice, 
                                           Labs = es.Lab,
                                           SemesterConsults = es.ConsultInSemester,
                                           ExamConsults = es.ConsultForExam, 
                                           WrittenWorks = es.WrittenWork,
                                           AnalyticalWorks = es.CalcWorks, 
                                           Projects = es.CourseProjects, 
                                           Evaluation = es.Evaluation, 
                                           OralExams = es.OralExam, 
                                           WrittenExams = es.WrittenExam, 
                                           CheckingTests = es.VerifyingOfTest, 
                                           DiplomManagement = es.ManagedDiploma, 
                                           DekParticipation = es.Dek, 
                                           CheckWriteWorks = es.VerifyingOfWrittenWorks, 
                                           Protection = es.Protection, 
                                           Total = es.Total, 
                                           Active = es.Active, 
                                           Semester = es.Semester
                                           #endregion
                                       }

                                   }).ToList();
                var extramural = extramuralEntry.First();
                return new JsonResult() { Data = extramural, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        //Day detail
        [HttpGet]
        public JsonResult GetDetailWorkersDistributed(string DayEntryId, byte semester)
        {
            List<DayEntryViewModel> dayEntry;
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                dayEntry = (from del in ctx.DayEntryLoads
                            join sb in ctx.Subjects on del.SubjectId equals sb.Id
                            join spt in ctx.Specialties on del.SpecialtyId equals spt.Id
                            join spl in ctx.Specializes on del.SpecializeId equals spl.Id
                            join cs in ctx.Courses on del.CourseId equals cs.Id
                            join ds in ctx.DaySemesters on del.Id equals ds.DayEntryLoadId
                            where (del.Id == DayEntryId && ds.Semester == semester)
                            select new DayEntryViewModel()
                            {
                                #region Fields
                                DayEntryId = del.Id,
                                Faculty = del.FacultyName,
                                Specialty = spt.Code,
                                Specialization = spl.Cipher,
                                Course = cs.Literal,
                                EducationDegree = del.EducationDegree,
                                StudentsCount = del.QuantityOfStudents,
                                ForeignersCount = del.QuantityOfForeigners,
                                GroupsCipher = del.CipherOfGroups,
                                QuantityOfGroupsA = del.QuantityOfGroupsCritOne,
                                RealQuantityOfGroups = del.RealQuantityOfGroups,
                                QuantityOfGroupsB = del.QuantityOfGroupsCritTwo,
                                QuantityOfThreads = del.QuantityOfThreads,
                                ConflatedThreads = del.ConflatedThreads,
                                Notes = del.Note,
                                Subject = sb.Name,
                                QuantityOfCredits = del.CountOfCredits,
                                Hours = del.CountOfHours,
                                Language = del.Language,
                                QuantityOfWeeksFs = del.FS_CountOfWeeks,
                                QuantityOfWeeksSs = del.SS_CountOfWeeks,

                                DeS = new DayEntrySemester()
                                {
                                    TotalHours = semester == (byte)SemesterType.First ? del.F_TotalHour : del.S_TotalHour,
                                    Total = semester == (byte)SemesterType.First ? del.F_Total : del.S_Total,
                                    Lectures = semester == (byte)SemesterType.First ? del.F_Lectures : del.S_Lectures,
                                    Labs = semester == (byte)SemesterType.First ? del.F_Labs : del.S_Labs,
                                    Practices = semester == (byte)SemesterType.First ? del.F_Practical : del.S_Practical,
                                    IndependentWorks = semester == (byte)SemesterType.First ? del.F_IndividualWork : del.S_IndividualWork,
                                    Courses = semester == (byte)SemesterType.First ? del.F_CourseProjects : del.S_CourseProjects,
                                    Exam = semester == (byte)SemesterType.First ? del.F_Exams : del.S_Exams,
                                    Evaluation = semester == (byte)SemesterType.First ? del.F_Evaluation : del.S_Evaluation
                                },

                                DaySemesterId = ds.Id,
                                Dd = new DayDistribution()
                                {
                                    Semester = ds.Semester,
                                    Lecture = ds.Lecture,
                                    Practice = ds.Practice,
                                    Lab = ds.Lab,
                                    ConsultInSemester = ds.ConsultInSemester,
                                    ConsultForExam = ds.ConsultForExam,
                                    VerifyingOfTests = ds.VerifyingOfTests,
                                    KR_KP = ds.KR_KP,
                                    ControlEvaluation = ds.ControlEvaluation,
                                    ControlExam = ds.ControlExam,
                                    PracticePreparation = ds.PracticePreparation,
                                    Dek = ds.Dek,
                                    StateExam = ds.StateExam,
                                    ManagedDiploma = ds.ManagedDiploma,
                                    Other = ds.Other,
                                    Total = ds.Total,
                                    Active = ds.Active,
                                    EnglishBonus = ds.EnglishBonus
                                },

                                CoeficientFs = del.FSemCoefficient,
                                CoeficientSs = del.SSemCoefficient,
                                DepartmentCipher = del.DepartmentCipher,
                                Projects = del.KR_KP_DR,
                                Practices = del.Practice,
                                QuantityOfMembers = del.QuantityOfDek
                                #endregion


                            }).ToList();
                var days = dayEntry.First();
                return new JsonResult() { Data = days, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        [HttpGet]
        public JsonResult GetDayTeachLoad(string loadingId, byte semester)
        {
            List<DayTeachLoads> dayTeachLoad;
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                dayTeachLoad = (from del in ctx.DayEntryLoads
                                join sb in ctx.Subjects on del.SubjectId equals sb.Id
                                join spt in ctx.Specialties on del.SpecialtyId equals spt.Id
                                join spl in ctx.Specializes on del.SpecializeId equals spl.Id
                                join cs in ctx.Courses on del.CourseId equals cs.Id
                                join das in ctx.DaySemesters on del.Id equals das.DayEntryLoadId
                                join dt in ctx.DayTeachLoads on del.Id equals dt.DayEntryLoadId
                                join us in ctx.Users on dt.ApplicationUserId equals us.Id
                                where (del.LoadingListId == loadingId && dt.Semester == semester)
                                select new DayTeachLoads()
                                {
                                    #region Fields
                                    DayEntryLoadId = del.Id,
                                    DayTeachLoadId = dt.Id,
                                    Specialization = spl.Cipher,
                                    Active = dt.Active,
                                    LastName = us.LastName,
                                    FirstName = us.FirstName.Substring(0,1),
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
                                }).Distinct().OrderBy(s=> s.LastName).ToList();
                return new JsonResult() { Data = dayTeachLoad, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpGet]
        public JsonResult GetExtramuralchLoad(string loadingId, byte semester)
        {
            List<ExtramuralTeachLoads> extramuralTeachLoad;
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                extramuralTeachLoad = (from eel in ctx.ExtramuralEntryLoads
                                       join sb in ctx.Subjects on eel.SubjectId equals sb.Id
                                       join spt in ctx.Specialties on eel.SpecialtyId equals spt.Id
                                       join es in ctx.ExtramuralSemesters on eel.Id equals es.ExtramuralEntryLoadId
                                       join ext in ctx.ExtramuralTeachLoads on eel.Id equals ext.ExtramuralEntryLoadId
                                       join dep in ctx.Departments on eel.DepartmentId equals dep.Id
                                       join us in ctx.Users on ext.ApplicationUserId equals us.Id
                                where (eel.LoadingListId == loadingId && ext.Semester == semester)
                                select new ExtramuralTeachLoads()
                                {
                                    #region Fields
                                    ExtramuralEntryLoadId = eel.Id,
                                    ExtramuralTeachLoadsId = ext.Id,
                                    Active = ext.Active,
                                    LastName = us.LastName, 
                                    FirstName = us.FirstName.Substring(0, 1),
                                    ThirdName = us.ThirdName.Substring(0, 1),
                                    CourseLiteral = eel.Course,
                                    StudentsCount =  eel.QuantityOfStudents,
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
                                }).Distinct().OrderBy(s => s.LastName).ToList();
                return new JsonResult() { Data = extramuralTeachLoad, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        #endregion

        #region Posts
        //Edit day 
        public string EditDetailWorkersDistributed(DaySemester ds, string Id, string userId, string DayEntryId, byte semestr)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var newDaySemester = ctx.DaySemesters.Where(s => s.Id == Id).First();

                var dayTeachLoad = (from del in ctx.DayEntryLoads
                                    join sb in ctx.Subjects on del.SubjectId equals sb.Id
                                    join spt in ctx.Specialties on del.SpecialtyId equals spt.Id
                                    join spl in ctx.Specializes on del.SpecializeId equals spl.Id
                                    join cs in ctx.Courses on del.CourseId equals cs.Id
                                    join das in ctx.DaySemesters on del.Id equals das.DayEntryLoadId
                                    join dt in ctx.DayTeachLoads on del.Id equals dt.DayEntryLoadId
                                    where (del.Id == DayEntryId && das.Semester == semestr && dt.ApplicationUserId == userId)
                                    select new
                                    {
                                        DayEntryId = del.Id,
                                        DayTeachId = dt.ApplicationUserId,
                                        Semestr = das.Semester,
                                        SubjectId = del.SubjectId
                                    }).FirstOrDefault();
                if (dayTeachLoad == null)
                {
                    var nDayTl = (from del in ctx.DayEntryLoads
                                  join sb in ctx.Subjects on del.SubjectId equals sb.Id
                                  join spt in ctx.Specialties on del.SpecialtyId equals spt.Id
                                  join spl in ctx.Specializes on del.SpecializeId equals spl.Id
                                  join cs in ctx.Courses on del.CourseId equals cs.Id
                                  join das in ctx.DaySemesters on del.Id equals das.DayEntryLoadId
                                  where (del.Id == DayEntryId && das.Semester == semestr)
                                  select new
                                  {
                                      DayEntryId = del.Id,
                                      Semestr = das.Semester,
                                      SubjectId = del.SubjectId
                                  }).FirstOrDefault();

                    var crdayTeachLoad = new DayTeachLoad()
                    {
                        DayEntryLoadId = DayEntryId,
                        SubjectId = nDayTl.SubjectId,
                        ApplicationUserId = userId,
                        Semester = semestr,
                        //Active = ds.Active, -
                        ConsultForExam = ds.ConsultForExam,
                        ConsultInSemester = ds.ConsultInSemester,
                        Dek = ds.Dek,
                        Lab = ds.Lab,
                        Lecture = ds.Lecture,
                        ManagedDiploma = ds.ManagedDiploma,
                        Practice = ds.Practice,
                        //Total = ds.Total, -
                        Evaluation = ds.ControlEvaluation, //?
                        VerifyingOfTest = ds.VerifyingOfTests,
                        Total = ds.Lecture + ds.Practice + ds.Lab + ds.ConsultForExam +
                        ds.ConsultInSemester + ds.VerifyingOfTests + ds.ControlEvaluation + ds.ManagedDiploma + ds.Dek,
                        Active = ds.Lecture + ds.Practice + ds.Lab + ds.ConsultForExam
                    };
                    ctx.Entry(crdayTeachLoad).State = System.Data.Entity.EntityState.Added;


                    #region Fields Day Semester
                    newDaySemester.ConsultForExam -= ds.ConsultForExam;
                    newDaySemester.ConsultInSemester -= ds.ConsultInSemester;
                    newDaySemester.Dek -= ds.Dek;
                    newDaySemester.Lab -= ds.Lab;
                    newDaySemester.Lecture -= ds.Lecture;
                    newDaySemester.ManagedDiploma -= ds.ManagedDiploma;
                    newDaySemester.Practice -= ds.Practice;
                    newDaySemester.ControlEvaluation = ds.ControlEvaluation;
                    newDaySemester.VerifyingOfTests -= ds.VerifyingOfTests;
                    newDaySemester.Total = newDaySemester.Lecture + newDaySemester.Practice + newDaySemester.Lab + newDaySemester.ConsultForExam +
                        newDaySemester.ConsultInSemester + newDaySemester.VerifyingOfTests + newDaySemester.ControlEvaluation + newDaySemester.ControlExam
                        + newDaySemester.PracticePreparation + newDaySemester.StateExam + newDaySemester.ManagedDiploma + newDaySemester.Other;
                    newDaySemester.Active = newDaySemester.Lecture + newDaySemester.Practice + newDaySemester.Lab + newDaySemester.ConsultForExam;
                    #endregion

                    if (newDaySemester.Active < 0 || newDaySemester.ConsultForExam < 0 || newDaySemester.ConsultInSemester < 0
                        || newDaySemester.Dek < 0 || newDaySemester.Lab < 0 || newDaySemester.Lecture < 0
                        || newDaySemester.ManagedDiploma < 0 || newDaySemester.Practice < 0 || newDaySemester.Total < 0 || newDaySemester.VerifyingOfTests < 0)
                    {
                        throw new Exception("Значення не може бути менше 0!");
                    }
                    else
                    {
                        ctx.Entry(newDaySemester).State = System.Data.Entity.EntityState.Modified;
                    }
                    
                    ctx.SaveChanges();
                }
                else
                {
                    #region Fields Day Semester
                    newDaySemester.ConsultForExam -= ds.ConsultForExam;
                    newDaySemester.ConsultInSemester -= ds.ConsultInSemester;
                    newDaySemester.Dek -= ds.Dek;
                    newDaySemester.Lab -= ds.Lab;
                    newDaySemester.Lecture -= ds.Lecture;
                    newDaySemester.ManagedDiploma -= ds.ManagedDiploma;
                    newDaySemester.Practice -= ds.Practice;
                    newDaySemester.ControlEvaluation -= ds.ControlEvaluation;
                    newDaySemester.VerifyingOfTests -= ds.VerifyingOfTests;
                    newDaySemester.Total = newDaySemester.Lecture + newDaySemester.Practice + newDaySemester.Lab + newDaySemester.ConsultForExam +
                        newDaySemester.ConsultInSemester + newDaySemester.VerifyingOfTests + newDaySemester.ControlEvaluation + newDaySemester.ManagedDiploma + newDaySemester.Dek;
                    newDaySemester.Active = newDaySemester.Lecture + newDaySemester.Practice + newDaySemester.Lab + newDaySemester.ConsultForExam;
                    #endregion

                    if (newDaySemester.Active < 0 || newDaySemester.ConsultForExam < 0 || newDaySemester.ConsultInSemester < 0
                        || newDaySemester.Dek < 0 || newDaySemester.Lab < 0 || newDaySemester.Lecture < 0
                        || newDaySemester.ManagedDiploma < 0 || newDaySemester.Practice < 0 || newDaySemester.ControlEvaluation < 0 || newDaySemester.VerifyingOfTests < 0)
                    {
                        throw new Exception("Значення не може бути менше 0!");
                    }

                    ctx.Entry(newDaySemester).State = System.Data.Entity.EntityState.Modified;

                    var newdayTeachLoad = ctx.DayTeachLoads.Where(s => s.DayEntryLoadId == dayTeachLoad.DayEntryId
              && s.SubjectId == dayTeachLoad.SubjectId && s.Semester == dayTeachLoad.Semestr && s.ApplicationUserId == dayTeachLoad.DayTeachId).FirstOrDefault();

                    #region Fields Day Teach Load
                    newdayTeachLoad.ConsultForExam += ds.ConsultForExam;
                    newdayTeachLoad.ConsultInSemester += ds.ConsultInSemester;
                    newdayTeachLoad.Dek += ds.Dek;
                    newdayTeachLoad.Lab += ds.Lab;
                    newdayTeachLoad.Lecture += ds.Lecture;
                    newdayTeachLoad.ManagedDiploma += ds.ManagedDiploma;
                    newdayTeachLoad.Practice += ds.Practice;
                    newdayTeachLoad.Evaluation += ds.ControlEvaluation;
                    newdayTeachLoad.VerifyingOfTest += ds.VerifyingOfTests;
                    newdayTeachLoad.Total = newdayTeachLoad.Lecture + newdayTeachLoad.Practice + newdayTeachLoad.Lab + newdayTeachLoad.ConsultForExam +
                       newdayTeachLoad.ConsultInSemester + newdayTeachLoad.VerifyingOfTest + newdayTeachLoad.Evaluation + newdayTeachLoad.ManagedDiploma + newdayTeachLoad.Dek;

                    newdayTeachLoad.Active = newdayTeachLoad.Lecture + newdayTeachLoad.Practice + newdayTeachLoad.Lab + newdayTeachLoad.ConsultForExam;
                    #endregion

                    ctx.Entry(newdayTeachLoad).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
            return "Данные обновлены!";
        }

        //Edit Extramural
        public string EditExtramuralDistributed(ExtramuralSemester exs, string Id, string userId, string ExtramuralEntryId, byte semester)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var newExtramuralSemester = ctx.ExtramuralSemesters.Where(s => s.Id == Id).First();

                var extramuralTeachLoad = (from eel in ctx.ExtramuralEntryLoads
                                           join sb in ctx.Subjects on eel.SubjectId equals sb.Id
                                           join spt in ctx.Specialties on eel.SpecialtyId equals spt.Id
                                           join es in ctx.ExtramuralSemesters on eel.Id equals es.ExtramuralEntryLoadId
                                           join ext in ctx.ExtramuralTeachLoads on eel.Id equals ext.ExtramuralEntryLoadId
                                           join dep in ctx.Departments on eel.DepartmentId equals dep.Id
                                           where (eel.Id == ExtramuralEntryId && es.Semester == semester && ext.ApplicationUserId == userId)
                                           select new
                                           {
                                               ExtramuralEntryId = eel.Id,
                                               ExtramuralTeachId = ext.ApplicationUserId,
                                               Semester = es.Semester,
                                               SubjectId = eel.SubjectId
                                           }).FirstOrDefault();

                if(extramuralTeachLoad == null)
                {
                    var extTL = (from eel in ctx.ExtramuralEntryLoads
                                 join sb in ctx.Subjects on eel.SubjectId equals sb.Id
                                 join spt in ctx.Specialties on eel.SpecialtyId equals spt.Id
                                 join es in ctx.ExtramuralSemesters on eel.Id equals es.ExtramuralEntryLoadId
                                 join dep in ctx.Departments on eel.DepartmentId equals dep.Id
                                 where (eel.Id == ExtramuralEntryId && es.Semester == semester)
                                 select new
                                 {
                                     ExtramuralEntryId = eel.Id,
                                     Semester = es.Semester,
                                     SubjectId = eel.SubjectId
                                 }).FirstOrDefault();


                    var crExtramuralTeachLoad = new ExtramuralTeachLoad()
                    {
                        ExtramuralEntryLoadId = ExtramuralEntryId,
                        ApplicationUserId = userId,
                        SubjectId = extTL.SubjectId,
                        Semester = semester,
                        Lecture = exs.Lecture,
                        Practice = exs.Practice,
                        Lab = exs.Lab,
                        ConsultInSemester = exs.ConsultInSemester,
                        ConsultForExam = exs.ConsultForExam,
                        WrittenWork = exs.WrittenWork,
                        CalcWorks = exs.CalcWorks,
                        CourseProjects = exs.CourseProjects,
                        Evaluation = exs.Evaluation,
                        OralExam = exs.OralExam,
                        WrittenExam = exs.WrittenExam,
                        VerifyingOfTest = exs.VerifyingOfTest,
                        ManagedDiploma = exs.ManagedDiploma,
                        Dek = exs.Dek,
                        VerifyingOfWrittenWorks = exs.VerifyingOfWrittenWorks,
                        Protection = exs.Protection,
                        Total = exs.Lecture + exs.Practice + exs.Lab + exs.ConsultInSemester + exs.ConsultForExam +
                exs.WrittenWork + exs.Evaluation + exs.OralExam +
                exs.WrittenExam + exs.VerifyingOfTest + exs.ManagedDiploma + exs.Dek + exs.VerifyingOfWrittenWorks +
                exs.Protection,
                        Active = exs.Lecture + exs.Practice + exs.Lab + exs.ConsultForExam + exs.Evaluation + exs.OralExam
                + exs.Dek
                };
                    ctx.Entry(crExtramuralTeachLoad).State = System.Data.Entity.EntityState.Added;

                    #region Fields Extramural Semester
                    newExtramuralSemester.Lecture -= exs.Lecture;
                    newExtramuralSemester.Practice -= exs.Practice;
                    newExtramuralSemester.Lab -= exs.Lab;
                    newExtramuralSemester.ConsultInSemester -= exs.ConsultInSemester;
                    newExtramuralSemester.ConsultForExam -= exs.ConsultForExam;
                    newExtramuralSemester.WrittenWork -= exs.WrittenWork;
                    newExtramuralSemester.CalcWorks -= exs.CalcWorks;
                    newExtramuralSemester.CourseProjects -= exs.CourseProjects;
                    newExtramuralSemester.Evaluation -= exs.Evaluation;
                    newExtramuralSemester.OralExam -= exs.OralExam;
                    newExtramuralSemester.WrittenExam -= exs.WrittenExam;
                    newExtramuralSemester.VerifyingOfTest -= exs.VerifyingOfTest;
                    newExtramuralSemester.ManagedDiploma -= exs.ManagedDiploma;
                    newExtramuralSemester.Dek -= exs.Dek;
                    newExtramuralSemester.VerifyingOfWrittenWorks -= exs.VerifyingOfWrittenWorks;
                    newExtramuralSemester.Protection -= exs.Protection;
                    newExtramuralSemester.Total = newExtramuralSemester.Lecture + newExtramuralSemester.Practice + newExtramuralSemester.Lab + newExtramuralSemester.ConsultInSemester + newExtramuralSemester.ConsultForExam +
                newExtramuralSemester.WrittenWork + newExtramuralSemester.Evaluation + newExtramuralSemester.OralExam +
                newExtramuralSemester.WrittenExam + newExtramuralSemester.VerifyingOfTest + newExtramuralSemester.ManagedDiploma + newExtramuralSemester.Dek + newExtramuralSemester.VerifyingOfWrittenWorks +
                newExtramuralSemester.Protection;
                    newExtramuralSemester.Active = newExtramuralSemester.Lecture + newExtramuralSemester.Practice + newExtramuralSemester.Lab + newExtramuralSemester.ConsultForExam + newExtramuralSemester.Evaluation + newExtramuralSemester.OralExam
                + newExtramuralSemester.Dek;
                    #endregion

                    if (newExtramuralSemester.Lecture < 0 || newExtramuralSemester.Practice < 0 || newExtramuralSemester.Lab < 0
                        || newExtramuralSemester.ConsultInSemester < 0 || newExtramuralSemester.ConsultForExam < 0 || newExtramuralSemester.WrittenWork < 0
                        || newExtramuralSemester.CalcWorks < 0 || newExtramuralSemester.CourseProjects < 0 || newExtramuralSemester.Evaluation < 0
                        || newExtramuralSemester.OralExam < 0 || newExtramuralSemester.WrittenExam < 0 || newExtramuralSemester.VerifyingOfTest < 0
                        || newExtramuralSemester.ManagedDiploma < 0 || newExtramuralSemester.Dek < 0 || newExtramuralSemester.VerifyingOfWrittenWorks < 0
                        || newExtramuralSemester.Protection < 0 || newExtramuralSemester.Total < 0 || newExtramuralSemester.Active < 0)
                    {
                        throw new Exception("Значення не може бути менше 0!");
                    }

                    ctx.Entry(newExtramuralSemester).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }
                else
                {

                    var newExtramuralTeachLoad = ctx.ExtramuralTeachLoads.Where(s => s.ExtramuralEntryLoadId == extramuralTeachLoad.ExtramuralEntryId
                    && s.ApplicationUserId == extramuralTeachLoad.ExtramuralTeachId && s.Semester == extramuralTeachLoad.Semester && s.SubjectId == extramuralTeachLoad.SubjectId).FirstOrDefault();


                    #region Fields Extramural Semester
                    newExtramuralSemester.Lecture -= exs.Lecture;
                    newExtramuralSemester.Practice -= exs.Practice;
                    newExtramuralSemester.Lab -= exs.Lab;
                    newExtramuralSemester.ConsultInSemester -= exs.ConsultInSemester;
                    newExtramuralSemester.ConsultForExam -= exs.ConsultForExam;
                    newExtramuralSemester.WrittenWork -= exs.WrittenWork;
                    newExtramuralSemester.CalcWorks -= exs.CalcWorks;
                    newExtramuralSemester.CourseProjects -= exs.CourseProjects;
                    newExtramuralSemester.Evaluation -= exs.Evaluation;
                    newExtramuralSemester.OralExam -= exs.OralExam;
                    newExtramuralSemester.WrittenExam -= exs.WrittenExam;
                    newExtramuralSemester.VerifyingOfTest -= exs.VerifyingOfTest;
                    newExtramuralSemester.ManagedDiploma -= exs.ManagedDiploma;
                    newExtramuralSemester.Dek -= exs.Dek;
                    newExtramuralSemester.VerifyingOfWrittenWorks -= exs.VerifyingOfWrittenWorks;
                    newExtramuralSemester.Protection -= exs.Protection;
                    newExtramuralSemester.Total = newExtramuralSemester.Lecture + newExtramuralSemester.Practice + newExtramuralSemester.Lab + newExtramuralSemester.ConsultInSemester + newExtramuralSemester.ConsultForExam +
                newExtramuralSemester.WrittenWork + newExtramuralSemester.Evaluation + newExtramuralSemester.OralExam +
                newExtramuralSemester.WrittenExam + newExtramuralSemester.VerifyingOfTest + newExtramuralSemester.ManagedDiploma + newExtramuralSemester.Dek + newExtramuralSemester.VerifyingOfWrittenWorks +
                newExtramuralSemester.Protection;
                    newExtramuralSemester.Active = newExtramuralSemester.Lecture + newExtramuralSemester.Practice + newExtramuralSemester.Lab + newExtramuralSemester.ConsultForExam + newExtramuralSemester.Evaluation + newExtramuralSemester.OralExam
                + newExtramuralSemester.Dek;
                    #endregion

                    if (newExtramuralSemester.Lecture < 0 || newExtramuralSemester.Practice < 0 || newExtramuralSemester.Lab < 0
                        || newExtramuralSemester.ConsultInSemester < 0 || newExtramuralSemester.ConsultForExam < 0 || newExtramuralSemester.WrittenWork < 0
                        || newExtramuralSemester.CalcWorks < 0 || newExtramuralSemester.CourseProjects < 0 || newExtramuralSemester.Evaluation < 0
                        || newExtramuralSemester.OralExam < 0 || newExtramuralSemester.WrittenExam < 0 || newExtramuralSemester.VerifyingOfTest < 0
                        || newExtramuralSemester.ManagedDiploma < 0 || newExtramuralSemester.Dek < 0 || newExtramuralSemester.VerifyingOfWrittenWorks < 0
                        || newExtramuralSemester.Protection < 0 || newExtramuralSemester.Total < 0 || newExtramuralSemester.Active < 0)
                    {
                        throw new Exception("Значення не може бути менше 0!");
                    }

                    ctx.Entry(newExtramuralSemester).State = System.Data.Entity.EntityState.Modified;

                    #region Fields Extramural Teach Load
                    newExtramuralTeachLoad.SubjectId = extramuralTeachLoad.SubjectId;
                    newExtramuralTeachLoad.Lecture += exs.Lecture;
                    newExtramuralTeachLoad.Practice += exs.Practice;
                    newExtramuralTeachLoad.Lab += exs.Lab;
                    newExtramuralTeachLoad.ConsultInSemester += exs.ConsultInSemester;
                    newExtramuralTeachLoad.ConsultForExam += exs.ConsultForExam;
                    newExtramuralTeachLoad.WrittenWork += exs.WrittenWork;
                    newExtramuralTeachLoad.CalcWorks += exs.CalcWorks;
                    newExtramuralTeachLoad.CourseProjects += exs.CourseProjects;
                    newExtramuralTeachLoad.Evaluation += exs.Evaluation;
                    newExtramuralTeachLoad.OralExam += exs.OralExam;
                    newExtramuralTeachLoad.WrittenExam += exs.WrittenExam;
                    newExtramuralTeachLoad.VerifyingOfTest += exs.VerifyingOfTest;
                    newExtramuralTeachLoad.ManagedDiploma += exs.ManagedDiploma;
                    newExtramuralTeachLoad.Dek += exs.Dek;
                    newExtramuralTeachLoad.VerifyingOfWrittenWorks += exs.VerifyingOfWrittenWorks;
                    newExtramuralTeachLoad.Protection += exs.Protection;
                    newExtramuralTeachLoad.Total = newExtramuralTeachLoad.Lecture + newExtramuralTeachLoad.Practice + newExtramuralTeachLoad.Lab + newExtramuralTeachLoad.ConsultInSemester + newExtramuralTeachLoad.ConsultForExam +
                newExtramuralTeachLoad.WrittenWork + newExtramuralTeachLoad.Evaluation + newExtramuralTeachLoad.OralExam +
                newExtramuralTeachLoad.WrittenExam + newExtramuralTeachLoad.VerifyingOfTest + newExtramuralTeachLoad.ManagedDiploma + newExtramuralTeachLoad.Dek + newExtramuralTeachLoad.VerifyingOfWrittenWorks +
                newExtramuralTeachLoad.Protection;
                    newExtramuralTeachLoad.Active = newExtramuralTeachLoad.Lecture + newExtramuralTeachLoad.Practice + newExtramuralTeachLoad.Lab + newExtramuralTeachLoad.ConsultForExam + newExtramuralTeachLoad.Evaluation + newExtramuralTeachLoad.OralExam
                + newExtramuralTeachLoad.Dek;
                    #endregion

                    ctx.Entry(newExtramuralTeachLoad).State = System.Data.Entity.EntityState.Modified;

                    ctx.SaveChanges();
                }

            }
            return "Данные обновлены!";
        }
        #endregion
    }
}