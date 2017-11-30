using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using Domain.Models;
using Load.Mapper.RowFormat;
using Load.Mapper.SemesterFormat;

namespace Load.Services
{
    public class DataPreparer
    {
        #region Data

        private readonly EntryDictionaryService _entryDictionaryService;

        #region In

        private readonly List<DayFormatRow> _dayFormatRows;
        private readonly List<ExtraFormatRow> _extraFormatRows;

        private readonly List<DayFormatSemester> _firstDayFormatSemesters;
        private readonly List<DayFormatSemester> _secondDayFormatSemesters;
        private readonly List<ExtraFormatSemester> _firstExtraFormatSemesters;
        private readonly List<ExtraFormatSemester> _secondExtraFormatSemesters;

        #endregion

        #region Out

        private List<DayEntryLoad> _dayEntryLoad;
        private List<ExtramuralEntryLoad> _extramuralEntryLoad;
        private List<DaySemester> _daySemestersData;
        private List<ExtramuralSemester> _extramuralSemestersData;

        #endregion

        #endregion

        public DataPreparer(List<DayFormatRow> dayFormatRows,
                            List<ExtraFormatRow> extraFormatRows,
                            List<DayFormatSemester> fdfs,
                            List<DayFormatSemester> sdfs,
                            List<ExtraFormatSemester> fefs,
                            List<ExtraFormatSemester> sefs)
        {
            _dayFormatRows = dayFormatRows;
            _extraFormatRows = extraFormatRows;
            _firstDayFormatSemesters = fdfs;
            _secondDayFormatSemesters = sdfs;
            _firstExtraFormatSemesters = fefs;
            _secondExtraFormatSemesters = sefs;

            _entryDictionaryService =
                new EntryDictionaryService(new UniqueDataResolver(_dayFormatRows, _extraFormatRows));
        }

        public void MergeEntryLoad(LoadingList list)
        {
            PrepareEntryData(list, out _dayEntryLoad, out _extramuralEntryLoad);
            PrepareSemesterData(out _daySemestersData, out _extramuralSemestersData);

            using (var db = new ApplicationDbContext())
            {
                foreach (var day in _dayEntryLoad)
                {
                    db.Entry(day).State = EntityState.Added;

                    db.Subjects.Attach(day.Subject);
                    db.Specialties.Attach(day.Specialty);
                    db.Specializes.Attach(day.Specialize);
                    db.Departments.Attach(day.Department);
                    db.Courses.Attach(day.Course);
                }

                foreach (var extra in _extramuralEntryLoad)
                {
                    db.Entry(extra).State = EntityState.Added;

                    db.Subjects.Attach(extra.Subject);
                    db.Specialties.Attach(extra.Specialty);
                    db.Departments.Attach(extra.Department);
                }

                foreach (var ds in _daySemestersData)
                    db.Entry(ds).State = EntityState.Added;

                foreach (var es in _extramuralSemestersData)
                    db.Entry(es).State = EntityState.Added;

                db.SaveChanges();
            }
        }

        private void PrepareEntryData(LoadingList list, out List<DayEntryLoad> dayEntry, out List<ExtramuralEntryLoad> extramuralEntry)
        {
            if (!_entryDictionaryService.IsInitialized())
                _entryDictionaryService.InitialPush();
            else
            {
                if (_entryDictionaryService.FindNew())
                    _entryDictionaryService.Update();
            }

            dayEntry = MapToDayEntryLoad(_dayFormatRows, list);
            extramuralEntry = MapToExtraLoad(_extraFormatRows, list);
        }

        private List<DayEntryLoad> MapToDayEntryLoad(List<DayFormatRow> dayRow, LoadingList list)
        {
            _dayEntryLoad = new List<DayEntryLoad>(dayRow.Count);

            foreach (var dr in dayRow)
            {
                var entry = new DayEntryLoad();

                entry.LoadingList = list;
                entry.Language = dr.Language;
                entry.Note = dr.Notes;

                Subject subject;
                if (_entryDictionaryService.Subjects.TryGetValue(dr.Subject, out subject))
                    entry.Subject = subject;
                else
                    Debug.WriteLine($" Subject: {dr.Subject}");

                entry.CountOfCredits = dr.QuantityOfCredits;
                entry.CountOfHours = dr.Hours;
                entry.HoursPerCredit = 0; // mock
                entry.FSemCoefficient = dr.First.Coefficient;
                entry.SSemCoefficient = dr.Second.Coefficient;
                //
                entry.F_TotalHour = dr.First.TotalHours;
                entry.F_Total = dr.First.Total;
                entry.F_Lectures = dr.First.Lectures;
                entry.F_Labs = dr.First.Labs;
                entry.F_Practical = dr.First.Practices;
                entry.F_IndividualWork = dr.First.IndependentWorks;
                entry.F_CourseProjects = dr.First.Courses;
                entry.F_Exams = dr.First.Exam;
                entry.F_Evaluation = dr.First.Evaluation;
                //
                entry.S_TotalHour = dr.Second.TotalHours;
                entry.S_Total = dr.Second.Total;
                entry.S_Lectures = dr.Second.Lectures;
                entry.S_Labs = dr.Second.Labs;
                entry.S_Practical = dr.Second.Practices;
                entry.S_IndividualWork = dr.Second.IndependentWorks;
                entry.S_CourseProjects = dr.Second.Courses;
                entry.S_Exams = dr.Second.Exam;
                entry.S_Evaluation = dr.Second.Evaluation;
                //
                entry.DepartmentCipher = dr.DepartmentCipher;

                /*
                 * Внимание, костыль!
                 * Проблема в том, что во входном файле имеется нагрузка лишь для кафедры ис - 401
                 * в связи с чем было созданое поле <Code> Code </Code> в сущности
                 * <Code> Department </Code>
                 */
                entry.Department = _entryDictionaryService.Departments[401];
                entry.FacultyName = dr.Faculty;

                Specialty specialty;
                if (_entryDictionaryService.Specialties.TryGetValue(dr.Specialty, out specialty))
                    entry.Specialty = specialty;
                else
                    Debug.WriteLine($" Specialty: {dr.Specialty}");

                Specialize specialize;
                if (_entryDictionaryService.Specializes.TryGetValue(dr.Specialize, out specialize))
                    entry.Specialize = specialize;
                else
                    Debug.WriteLine($" Specialize: {dr.Specialize}");

                Course course;
                if (_entryDictionaryService.Courses.TryGetValue(dr.Course, out course))
                    entry.Course = course;
                else
                    Debug.WriteLine($" Course: {dr.Specialty}");
                //
                entry.FS_CountOfWeeks = dr.QuantityOfWeeksFs;
                entry.SS_CountOfWeeks = dr.QuantityOfWeeksSs;
                entry.QuantityOfStudents = dr.StudentsCount;
                entry.QuantityOfForeigners = dr.ForeignersCount;
                entry.CipherOfGroups = dr.GroupsCipher;
                entry.QuantityOfGroupsCritOne = dr.QuantityOfGroupsA;
                entry.RealQuantityOfGroups = dr.RealQuantityOfGroups;
                entry.QuantityOfGroupsCritTwo = dr.QuantityOfGroupsB;
                entry.QuantityOfThreads = dr.QuantityOfThreads;
                entry.CipherOfThreads = 0; //mock
                entry.KR_KP_DR = dr.Projects;
                entry.QuantityOfDek = dr.QuantityOfMembers;
                entry.EducationDegree = dr.Degree;
                entry.ConflatedThreads = dr.ConflatedThreads;
                entry.Practice = dr.Practices;

                _dayEntryLoad.Add(entry);
            }

            return _dayEntryLoad;
        }
        private List<ExtramuralEntryLoad> MapToExtraLoad(List<ExtraFormatRow> extraRow, LoadingList list)
        {
            _extramuralEntryLoad = new List<ExtramuralEntryLoad>(extraRow.Count);

            foreach (var er in extraRow)
            {
                var entry = new ExtramuralEntryLoad();

                entry.LoadingList = list;

                Department department;
                if (_entryDictionaryService.Departments.TryGetValue(er.DepartmentCode, out department))
                    entry.Department = department;
                else
                    Debug.WriteLine($" Department: {er.DepartmentCode}");

                Subject subject;
                if (_entryDictionaryService.Subjects.TryGetValue(er.Subject, out subject))
                    entry.Subject = subject;
                else
                    Debug.WriteLine($" Subject: {er.Subject}");

                Specialty specialty;
                if (_entryDictionaryService.Specialties.TryGetValue(er.SpecialtyCipher, out specialty))
                    entry.Specialty = specialty;
                else
                    Debug.WriteLine($" Specialty: {er.SpecialtyCipher}");


                entry.Extramural = er.Extra;

                Course course;
                if (_entryDictionaryService.Courses.TryGetValue(er.Course, out course))
                    entry.Course = Convert.ToDouble(course.Literal);

                entry.QuantityOfStudents = er.StudentsCount;
                entry.QuantityOfThreads = er.QuantityOfThreads;
                entry.QuantityOfGroups = er.QuantityOfGroups;
                entry.NumOfThread = er.ThreadNumber;
                entry.MajorSpecialty = er.MajorSpecialty;
                entry.CommonTime = er.TotalHours;
                entry.Credits = er.Credits;

                entry.F_Lecture = er.First.Lectures;
                entry.F_Practical = er.First.Practices;
                entry.F_Lab = er.First.Labs;
                entry.F_IndividualWork = er.First.IndependentWorks;
                entry.F_Exam = er.First.Exam;
                entry.F_Evaluation = er.First.Evaluation;
                entry.F_KR = er.First.Projects;
                entry.F_Test = er.First.Test;
                entry.F_LimitOnProjects = er.First.LimitOnProjects;

                entry.S_Lecture = er.Second.Lectures;
                entry.S_Practical = er.Second.Practices;
                entry.S_Lab = er.Second.Labs;
                entry.S_IndividualWork = er.Second.IndependentWorks;
                entry.S_Exam = er.Second.Exam;
                entry.S_Evaluation = er.Second.Evaluation;
                entry.S_KR = er.Second.Projects;
                entry.S_Test = er.Second.Test;
                entry.S_LimitOnProjects = er.Second.LimitOnProjects;

                
                _extramuralEntryLoad.Add(entry);
            }

            return _extramuralEntryLoad;
        }

        private void PrepareSemesterData(out List<DaySemester> daySemester, out List<ExtramuralSemester> extramuralSemester)
        {
            List<DaySemester> firstDaySemester = MapToDaySemester(_firstDayFormatSemesters, 1);
            List<DaySemester> secondDaySemester = MapToDaySemester(_secondDayFormatSemesters, 2);

            List<ExtramuralSemester> firstExtramuralSemester = MapToExtramuralSemester(_firstExtraFormatSemesters, 1);
            List<ExtramuralSemester> secondExtramuralSemester = MapToExtramuralSemester(_secondExtraFormatSemesters, 2);

            int dayLength = _dayEntryLoad.Count;
            int extraLength = _extramuralEntryLoad.Count;

            for (int i = 0; i < dayLength; i++)
                firstDaySemester[i].DayEntryLoad = secondDaySemester[i].DayEntryLoad = _dayEntryLoad[i];
            for (int i = 0; i < extraLength; i++)
                firstExtramuralSemester[i].ExtramuralEntryLoad = secondExtramuralSemester[i].ExtramuralEntryLoad = _extramuralEntryLoad[i];

            daySemester = firstDaySemester.Concat(secondDaySemester).ToList();
            extramuralSemester = firstExtramuralSemester.Concat(secondExtramuralSemester).ToList();
        }

        private List<DaySemester> MapToDaySemester(List<DayFormatSemester> formatSemester, byte semester)
        {
            List<DaySemester> daySemesters =
                new List<DaySemester>(formatSemester.Count);

            foreach (var fr in formatSemester)
            {
                var ds = new DaySemester
                {
                    Lecture = fr.Lectures,
                    Practice = fr.Practices,
                    Lab = fr.Labs,
                    ConsultInSemester = fr.SemesterConsults,
                    ConsultForExam = fr.ExamConsults,
                    VerifyingOfTests = fr.CheckingTests,
                    KR_KP = fr.Projects,
                    ControlEvaluation = fr.Evaluation,
                    ControlExam = fr.Exam,
                    PracticePreparation = fr.PracticePreparation,
                    Dek = fr.ExamParticipation,
                    StateExam = fr.StateExam,
                    ManagedDiploma = fr.DiplomsManagement,
                    Other = fr.Other,
                    Total = fr.Total,
                    Active = fr.Active,
                    EnglishBonus = fr.EnglishBonus,
                    Semester = semester
                };
                daySemesters.Add(ds);
            }

            return daySemesters;
        }
        private List<ExtramuralSemester> MapToExtramuralSemester(List<ExtraFormatSemester> formatSemester, byte semester)
        {
            List<ExtramuralSemester> extramuralSemesters =
                new List<ExtramuralSemester>(formatSemester.Count);

            foreach (var fr in formatSemester)
            {
                var es = new ExtramuralSemester
                {
                    Lecture = fr.Lectures,
                    Practice = fr.Practices,
                    Lab = fr.Labs,
                    ConsultInSemester = fr.SemesterConsults,
                    ConsultForExam = fr.ExamConsults,
                    WrittenWork = fr.WrittenWorks,
                    CalcWorks = fr.AnalyticalWorks,
                    CourseProjects = fr.Projects,
                    Evaluation = fr.Evaluation,
                    OralExam = fr.OralExams,
                    WrittenExam = fr.WrittenExams,
                    VerifyingOfTest = fr.CheckingTests,
                    ManagedDiploma = fr.DiplomManagement,
                    Dek = fr.DekParticipation,
                    VerifyingOfWrittenWorks = fr.CheckWriteWorks,
                    Protection = fr.Protection,
                    Total = fr.Total,
                    Active = fr.Active,
                    Semester = semester
                };

                extramuralSemesters.Add(es);
            }

            return extramuralSemesters;
        }

    }
}
