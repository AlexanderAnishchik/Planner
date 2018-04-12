using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Load.Mapper.RowFormat;
using Load.Mapper.SemesterFormat;
using Load.Services.Types;

namespace Load.Services
{
    public enum Semester : byte { First = 1, Second = 2 }

    public class ProcessDataService
    {
        private readonly DayFormatCalculationRules _dR;
        private readonly ExtraFormatCalculationRules _eR;

        private readonly DayFormatRow[] _dayFormatRows;
        private readonly ExtraFormatRow[] _extraFormatRows;

        #region Processed Load

        private readonly List<DayFormatSemester> _firstDaySemesterLoad;
        public List<DayFormatSemester> FirstDaySemesterLoad => _firstDaySemesterLoad;

        private readonly List<DayFormatSemester> _secondDaySemesterLoad;
        public List<DayFormatSemester> SecondDaySemesterLoad => _secondDaySemesterLoad;

        private readonly List<ExtraFormatSemester> _firstExtraSemesterLoad;
        public List<ExtraFormatSemester> FirstExtraSemesterLoad => _firstExtraSemesterLoad;

        private readonly List<ExtraFormatSemester> _secondExtraSemesterLoad;
        public List<ExtraFormatSemester> SecondExtraSemesterLoad => _secondExtraSemesterLoad;

        public List<DayFormatSemester> GetFirstDaySemesterLoad()
        {
            return _firstDaySemesterLoad;
        }
        public List<DayFormatSemester> GetSecondDaySemesterLoad()
        {
            return _secondDaySemesterLoad;
        }
        public List<ExtraFormatSemester> GetFirstExtraSemesterLoad()
        {
            return _firstExtraSemesterLoad;
        }
        public List<ExtraFormatSemester> GetSecondExtraSemesterLoad()
        {
            return _secondExtraSemesterLoad;
        }

        #endregion

        public ProcessDataService()
        {
            _dR = new DayFormatCalculationRules();
            _eR = new ExtraFormatCalculationRules();            
        }

        public ProcessDataService(DayFormatRow[] dayFormatRows, ExtraFormatRow[] extraFormatRows)
        {
            if (dayFormatRows == null)
                throw new Exception("ProcessDataService: dayFormatRows is null!");
            else
            {
                _dayFormatRows = dayFormatRows;
                int size = _dayFormatRows.GetLength(0);

                _firstDaySemesterLoad = new List<DayFormatSemester>(size);
                _secondDaySemesterLoad = new List<DayFormatSemester>(size);
            }

            if (extraFormatRows == null)
                throw new Exception("ProcessDataService: extraFormatRows is null!");
            else
            {
                _extraFormatRows = extraFormatRows;
                int size = _extraFormatRows.GetLength(0);
                _firstExtraSemesterLoad = new List<ExtraFormatSemester>(size);
                _secondExtraSemesterLoad = new List<ExtraFormatSemester>(size);
            }

            _dR = new DayFormatCalculationRules();
            _eR = new ExtraFormatCalculationRules();
        }

        #region API

        #region Build
        public DayFormatSemester BuilDayFormatSemester(ref DayFormatRow dfr, Semester sem)
        {
            DayFormatSemester dfs = new DayFormatSemester();

            if (sem == Semester.First)
            {
                #region Processing

                dfs.Lectures =
                dfr.First.Lectures * dfr.QuantityOfThreads;

                dfs.Practices =
                    dfr.First.Practices * dfr.QuantityOfGroupsA;

                dfs.Labs =
                    dfr.First.Labs * dfr.QuantityOfGroupsB;

                dfs.SemesterConsults =
                    _dR.GetSemesterConsults(dfr.QuantityOfGroupsA, dfr.Notes, dfr.First.Total);

                dfs.ExamConsults =
                    _dR.GetExamConsultsFs(dfr.First.Exam, dfr.QuantityOfThreads, dfr.QuantityOfGroupsA);

                dfs.CheckingTests =
                    _dR.GetCheckingTests(dfr.StudentsCount, dfr.Notes, dfr.QuantityOfCredits, dfr.Hours, dfr.First.TotalHours, dfr.First.Total);

                dfs.Projects =
                    _dR.GetProjects(dfr.First.Courses, dfr.StudentsCount, dfr.Projects);

                dfs.Evaluation =
                    _dR.GetEvaluation(dfr.First.Evaluation, dfr.QuantityOfGroupsA);

                dfs.Exam =
                    _dR.GetExam(dfr.First.Exam, dfr.StudentsCount);

                dfs.PracticePreparation =
                    _dR.GetPracticePreparation(dfr.First.TotalHours, dfr.First.Total, dfr.Notes, dfr.StudentsCount, dfr.Practices);

                dfs.ExamParticipation =
                    _dR.GetExamParticipation(dfr.First.Courses, dfr.First.Exam, dfr.QuantityOfMembers, dfr.StudentsCount);

                dfs.StateExam =
                    _dR.GetStateExam(dfr.First.Exam, dfr.StudentsCount);

                dfs.DiplomsManagement =
                    _dR.GetDimplomsManagement(dfr.First.Courses, dfr.StudentsCount, dfr.Projects);

                dfs.Other = 0;
                dfs.Total = _dR.GetTotal(dfs);
                dfs.Active = _dR.GetActive(dfs);
                dfs.EnglishBonus = _dR.GetBonus(dfr.Language, dfs.Active);

                #endregion
            }
            else if (sem == Semester.Second)
            {
                #region Processing

                dfs.Lectures =
                dfr.Second.Lectures * dfr.QuantityOfThreads;

                dfs.Practices =
                    dfr.Second.Practices * dfr.QuantityOfGroupsA;

                dfs.Labs =
                    dfr.Second.Labs * dfr.QuantityOfGroupsB;

                dfs.SemesterConsults =
                    _dR.GetSemesterConsults(dfr.QuantityOfGroupsA, dfr.Notes, dfr.Second.Total);

                dfs.ExamConsults =
                    _dR.GetExamConsultsSs(dfr.Second.Exam, dfr.QuantityOfGroupsA);

                dfs.CheckingTests =
                    _dR.GetCheckingTests(dfr.StudentsCount, dfr.Notes, dfr.QuantityOfCredits, dfr.Hours, dfr.Second.TotalHours, dfr.Second.Total);

                dfs.Projects =
                    _dR.GetProjects(dfr.Second.Courses, dfr.StudentsCount, dfr.Projects);

                dfs.Evaluation =
                    _dR.GetEvaluation(dfr.Second.Evaluation, dfr.QuantityOfGroupsA);

                dfs.Exam =
                    _dR.GetExam(dfr.Second.Exam, dfr.StudentsCount);

                dfs.PracticePreparation =
                    _dR.GetPracticePreparation(dfr.Second.TotalHours, dfr.Second.Total, dfr.Notes, dfr.StudentsCount, dfr.Practices);

                dfs.ExamParticipation =
                    _dR.GetExamParticipation(dfr.Second.Courses, dfr.Second.Exam, dfr.QuantityOfMembers, dfr.StudentsCount);

                dfs.StateExam =
                    _dR.GetStateExam(dfr.Second.Exam, dfr.StudentsCount);

                dfs.DiplomsManagement =
                    _dR.GetDimplomsManagement(dfr.Second.Courses, dfr.StudentsCount, dfr.Projects);

                dfs.Other = 0;
                dfs.Total = _dR.GetTotal(dfs);
                dfs.Active = _dR.GetActive(dfs);
                dfs.EnglishBonus = _dR.GetBonus(dfr.Language, dfs.Active);

                #endregion
            }

            return dfs;
        }
        public ExtraFormatSemester BuildExtraFormatSemester(ref ExtraFormatRow efr, Semester sem)
        {
            ExtraFormatSemester efs = new ExtraFormatSemester();

            if (sem == Semester.First)
            {
                #region Processing

                efs.Lectures =
                efr.First.Lectures;

                efs.Practices =
                    efr.QuantityOfGroups * efr.First.Practices;

                efs.Labs =
                    efr.QuantityOfGroups * efr.First.Labs;

                efs.SemesterConsults =
                    efr.QuantityOfGroups * 2;

                efs.ExamConsults =
                    _eR.GetExamConsultsFs(efr.First.Exam, efr.Second.Exam, efr.QuantityOfThreads);

                efs.WrittenWorks = efs.AnalyticalWorks = 0;

                efs.Projects =
                    _eR.GetProjects(efr.First.Projects, efr.StudentsCount, efr.First.LimitOnProjects);

                efs.Evaluation =
                    _eR.GetEvaluation(efr.First.Evaluation, efr.QuantityOfGroups);

                efs.OralExams = efs.WrittenExams = 0;

                efs.Total = _eR.GetTotalFs(efs);
                efs.Active = _eR.GetActiveFs(efs);

                // these fields are not being used for calculation for the first semester of extramural format
                efs.CheckingTests = efs.DiplomManagement =
                efs.DekParticipation = efs.CheckWriteWorks =
                efs.Protection = 0.0;

                #endregion   
            }
            else if (sem == Semester.Second)
            {
                #region Processing

                efs.Lectures =
                efr.Second.Lectures;

                efs.Practices =
                    efr.QuantityOfGroups * efr.Second.Practices;

                efs.Labs =
                    efr.QuantityOfGroups * efr.Second.Labs;

                efs.SemesterConsults = efs.WrittenWorks = efs.AnalyticalWorks = 0;

                efs.ExamConsults =
                    _eR.GetExamConsultsSs(efr.Second.Exam, efr.Second.LimitOnProjects, efr.QuantityOfGroups);

                efs.Projects =
                    _eR.GetProjects(efr.Second.Projects, efr.StudentsCount, efr.Second.LimitOnProjects);

                efs.Evaluation =
                    _eR.GetEvaluation(efr.Second.Evaluation, efr.QuantityOfGroups);

                efs.OralExams =
                    _eR.GetOralExams(efr.First.Exam, efr.Second.Exam, efr.StudentsCount);

                efs.DekParticipation =
                    _eR.GetExamParticipation(efr.Second.Exam, efr.QuantityOfThreads, efr.Second.Projects, efr.StudentsCount);

                efs.CheckWriteWorks =
                    _eR.GetWrittenWorks(efr.Second.Exam, efr.StudentsCount);

                efs.Protection = efs.DiplomManagement = 0;

                efs.CheckingTests =
                    _eR.GetCheckingTests(efr.Second.Projects, efr.StudentsCount);

                efs.Total = _eR.GetTotalSs(efs);
                efs.Active = _eR.GetActiveSs(efs);

                #endregion
            }

            return efs;
        }

        #endregion

        #region Process

        private void ProcessFirstDFs()
        {
            int size = _dayFormatRows.GetLength(0);

            DayFormatSemester dfs;
            for (int i = 0; i < size; i++)
            {
                dfs = BuilDayFormatSemester(ref _dayFormatRows[i], Semester.First);
                _firstDaySemesterLoad.Add(dfs);
            }
            
        }
        private void ProcessSecondDFs()
        {
            int size = _dayFormatRows.GetLength(0);

            DayFormatSemester dfs;
            for (int i = 0; i < size; i++)
            {
                dfs = BuilDayFormatSemester(ref _dayFormatRows[i], Semester.Second);
                _secondDaySemesterLoad.Add(dfs);
            }
        }
        private void ProcessFirstEFs()
        {
            int size = _extraFormatRows.GetLength(0);

            ExtraFormatSemester efs;
            for (int i = 0; i < size; i++)
            {
                efs = BuildExtraFormatSemester(ref _extraFormatRows[i], Semester.First);
                _firstExtraSemesterLoad.Add(efs);
            }
        }
        private void ProcessSecondEFs()
        {
            int size = _extraFormatRows.GetLength(0);

            ExtraFormatSemester efs;
            for (int i = 0; i < size; i++)
            {
                efs = BuildExtraFormatSemester(ref _extraFormatRows[i], Semester.Second);
                _secondExtraSemesterLoad.Add(efs);
            }
        }

        private void ProcessDfs()
        {
            ProcessFirstDFs();
            ProcessSecondDFs();
        }
        private void ProcessEfs()
        {
            ProcessFirstEFs();
            ProcessSecondEFs();
        }
        public void Process()
        {
            ProcessDfs();
            ProcessEfs();
        }


        public async Task<Tuple<List<DayFormatRow>, List<ExtraFormatRow>, List<DayFormatSemester>, List<DayFormatSemester>, List<ExtraFormatSemester>, List<ExtraFormatSemester>>> GetProcessedLoadingAsync(Tuple<List<DayFormatRow>, List<ExtraFormatRow>> mappedData)
        {
            if(mappedData == null)
                throw new NullReferenceException("MappedData is null");

            var firstDaySemesterLoading = GetDaySemLoadingBySemester(mappedData.Item1, Semester.First);
            var firstExtraSemesterLoading = GetExtraSemLoadingBySemester(mappedData.Item2, Semester.First);
            var secondDaySemesterLoading = GetDaySemLoadingBySemester(mappedData.Item1, Semester.Second);
            var secondExtraSemesterLoading = GetExtraSemLoadingBySemester(mappedData.Item2, Semester.Second);

            var fdsl = await firstDaySemesterLoading;
            var fesl = await firstExtraSemesterLoading;
            var sdsl = await secondDaySemesterLoading;
            var sesl = await secondExtraSemesterLoading;

            return Tuple.Create(mappedData.Item1, mappedData.Item2, fdsl, sdsl, fesl, sesl);
        }
        private Task<List<DayFormatSemester>> GetDaySemLoadingBySemester(List<DayFormatRow> dayFormatRowsList, Semester semester)
        {
            return Task.Run(() =>
            {                
                DayFormatRow[] dayFormatRowsArray = dayFormatRowsList.ToArray();
                int size = dayFormatRowsArray.GetLength(0);
                List<DayFormatSemester> dayFormatSemesters = new List<DayFormatSemester>(size);

                for (int i = 0; i < size; i++)
                    dayFormatSemesters.Add(BuilDayFormatSemester(ref dayFormatRowsArray[i], semester));

                return dayFormatSemesters;
            });
        }
        private Task<List<ExtraFormatSemester>> GetExtraSemLoadingBySemester(List<ExtraFormatRow> extraFormatRowsList, Semester semester)
        {
            return Task.Run(() =>
            {
                ExtraFormatRow[] extraFormatRows = extraFormatRowsList.ToArray();
                int size = extraFormatRows.GetLength(0);
                List<ExtraFormatSemester> extraFormatSemesters = new List<ExtraFormatSemester>(size);

                for (int i = 0; i < size; i++)
                    extraFormatSemesters.Add(BuildExtraFormatSemester(ref extraFormatRows[i], semester));

                return extraFormatSemesters;
            });
        }
        #endregion

        #endregion

    }
}
