using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using Load.Mapper.SemesterFormat;
using Color = System.Drawing.Color;

namespace Load.Builder
{
    internal class SlDStyler
    {
        public SLStyle CommonStyle, BoldStyle, InputStyle, SummaryStyle;
        public SlDStyler()
        {           
            Initialize();
        }

        private void Initialize()
        {
            CommonStyle = new SLStyle();
            CommonStyle.Fill.SetPatternBackgroundColor(Color.White);
            CommonStyle.Font.FontName = "Times New Roman";
            CommonStyle.Font.FontSize = 16;
            CommonStyle.Font.FontColor = Color.Black;
            CommonStyle.Font.Bold = false;
            CommonStyle.Font.Italic = false;
            CommonStyle.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            CommonStyle.Alignment.Vertical = VerticalAlignmentValues.Center;
            CommonStyle.SetWrapText(true);

            BoldStyle = new SLStyle();
            BoldStyle.Fill.SetPatternBackgroundColor(Color.White);
            BoldStyle.Font.FontName = "Times New Roman";
            BoldStyle.Font.FontSize = 16;
            BoldStyle.Font.FontColor = Color.Black;
            BoldStyle.Font.Bold = true;
            BoldStyle.Font.Italic = false;
            BoldStyle.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            BoldStyle.Alignment.Vertical = VerticalAlignmentValues.Center;
            BoldStyle.SetWrapText(true);

            InputStyle = new SLStyle();
            InputStyle.Fill.SetPatternBackgroundColor(Color.Gainsboro);
            InputStyle.Font.FontName = "Times New Roman";
            InputStyle.Font.FontSize = 16;
            InputStyle.Font.FontColor = Color.Black;
            InputStyle.Font.Bold = false;
            InputStyle.Font.Italic = false;
            InputStyle.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            InputStyle.Alignment.Vertical = VerticalAlignmentValues.Center;
            InputStyle.SetWrapText(true);

            SummaryStyle = new SLStyle();
            SummaryStyle.Fill.SetPatternBackgroundColor(Color.Gainsboro);
            SummaryStyle.Font.FontName = "Times New Roman";
            SummaryStyle.Font.FontSize = 16;
            SummaryStyle.Font.FontColor = Color.Black;
            SummaryStyle.Font.Bold = true;
            SummaryStyle.Font.Italic = false;
            SummaryStyle.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            SummaryStyle.Alignment.Vertical = VerticalAlignmentValues.Center;
            SummaryStyle.SetWrapText(true);
        }
    }

    internal static class SlDExtention
    {
        public static void Fill(this SLDocument sl, string cellRef, bool data, SLStyle style)
        { 
            sl.SetCellValue(cellRef, data);
            sl.SetCellStyle(cellRef, style);
        }
        public static void Fill(this SLDocument sl, string cellRef, int data, SLStyle style)
        {
            sl.SetCellValue(cellRef, data);
            sl.SetCellStyle(cellRef, style);
        }
        public static void Fill(this SLDocument sl, string cellRef, double data, SLStyle style)
        {
            sl.SetCellValue(cellRef, data);
            sl.SetCellStyle(cellRef, style);
        }
        public static void Fill(this SLDocument sl, string cellRef, string data, SLStyle style)
        {
            sl.SetCellValue(cellRef, data);
            sl.SetCellStyle(cellRef, style);
        }
        public static void Fill(this SLDocument sl, string cellRef, DateTime data, SLStyle style)
        {
            sl.SetCellValue(cellRef, data);
            sl.SetCellStyle(cellRef, style);
        }
    }

    internal abstract class ReportBuilder
    {
        protected readonly ReportDataService DataService = new ReportDataService();
        protected readonly WrappedReport Report = new WrappedReport();
        protected readonly SlDStyler SlDStyler = new SlDStyler();

        protected dynamic BuildConfiguration;

        public void SetBuildConfiguration(ReportBuildConfiguration buildConfiguration)
            => BuildConfiguration = (EntryReportBuildConfiguration)buildConfiguration;

        protected abstract void InitializeData();
        protected abstract void BuildHeader();
        protected abstract void BuildBody();
        protected abstract void BuildSummary();

        protected virtual void Build()
        {
            InitializeData();
            BuildHeader();
            BuildBody();
            BuildSummary();
        }

        public WrappedReport GetReport()
        {
            Build();
            return Report;
        }
    }

    internal class CommonDayFormatReportBuilder : ReportBuilder
    {
        protected List<DayFormatData> DataFs;
        protected List<DayFormatData> DataSs;
        protected SemesterType SemesterType;

        protected override void InitializeData()
        {
            var eqParams = (EntryQueryContext)BuildConfiguration.DataContext;
            SemesterType = (SemesterType)eqParams.Semester;

            if (SemesterType == SemesterType.Both)
            {
                DataFs = DataService.GetDayFormatData(eqParams.Loading, (byte)SemesterType.First);
                DataSs = DataService.GetDayFormatData(eqParams.Loading, (byte)SemesterType.Second);
            }
            else if (SemesterType == SemesterType.First)
                DataFs = DataService.GetDayFormatData(eqParams.Loading, (byte)SemesterType.First);
            else if (SemesterType == SemesterType.Second)
                DataSs = DataService.GetDayFormatData(eqParams.Loading, (byte)SemesterType.Second);
        }

        protected override void BuildHeader()
        {
            int from = DataFs[0].Year;
            int to = from + 1;
            string semesterValue = SemesterType == SemesterType.Both
               ? string.Empty
               : SemesterType == SemesterType.First ? "1" : "2";
            semesterValue += " семестр";
            Report.Name = $"Отчёт по ДФ обучения за {from}-{to}, {semesterValue} {DateTime.Today.ToShortDateString()}.xlsx";
        }

        protected override void BuildBody()
        {
            Report.Document = new SLDocument(BuildConfiguration.PathToSample, Report.Name);

            BuildFirstSemester();
            BuildSecondSemester();            
        }

        protected void BuildFirstSemester()
        {
            int startA = BuildConfiguration.StartFromA;
            Report.Document.SelectWorksheet(BuildConfiguration.SheetNameA);
            int length = DataFs.Count;

            for (int i = 0; i < length; i++)
            {
                int n = i + 1;
                #region Fill

                Report.Document.Fill($"A{i + startA}", n, SlDStyler.BoldStyle);

                Report.Document.Fill($"D{i + startA}", DataFs[i].SubjectName, SlDStyler.CommonStyle);
                Report.Document.Fill($"E{i + startA}", DataFs[i].Faculty, SlDStyler.CommonStyle);
                Report.Document.Fill($"F{i + startA}", DataFs[i].Specialty, SlDStyler.CommonStyle);
                Report.Document.Fill($"G{i + startA}", DataFs[i].Specialization, SlDStyler.CommonStyle);
                Report.Document.Fill($"H{i + startA}", DataFs[i].Course, SlDStyler.CommonStyle);
                Report.Document.Fill($"I{i + startA}", DataFs[i].StudentsCount, SlDStyler.CommonStyle);
                Report.Document.Fill($"J{i + startA}", DataFs[i].GroupsCipher, SlDStyler.CommonStyle);
                Report.Document.Fill($"K{i + startA}", DataFs[i].QuantityOfThreads, SlDStyler.CommonStyle);

                Report.Document.Fill($"T{i + startA}", DataFs[i].InLectures, SlDStyler.InputStyle);
                Report.Document.Fill($"U{i + startA}", DataFs[i].InLabs, SlDStyler.InputStyle);
                Report.Document.Fill($"V{i + startA}", DataFs[i].InPractices, SlDStyler.InputStyle);
                Report.Document.Fill($"X{i + startA}", DataFs[i].InSeminars, SlDStyler.InputStyle); // [outdated]
                Report.Document.Fill($"Y{i + startA}", DataFs[i].InIndLessons, SlDStyler.InputStyle); // [outdated]
                Report.Document.Fill($"Z{i + startA}", DataFs[i].InPracticeWeeks, SlDStyler.InputStyle); // [outdated]
                Report.Document.Fill($"AA{i + startA}", DataFs[i].InIndependentWorks, SlDStyler.InputStyle);
                Report.Document.Fill($"AB{i + startA}", DataFs[i].InDiplomaProjects, SlDStyler.InputStyle); // [outdated]
                Report.Document.Fill($"AC{i + startA}", DataFs[i].InStateSertifications, SlDStyler.InputStyle); // [outdated]
                Report.Document.Fill($"AD{i + startA}", DataFs[i].InCourseProjects, SlDStyler.InputStyle);
                Report.Document.Fill($"AE{i + startA}", DataFs[i].InExams, SlDStyler.InputStyle);
                Report.Document.Fill($"AF{i + startA}", DataFs[i].InEvaluations, SlDStyler.InputStyle);

                Report.Document.Fill($"BK{i + startA}", DataFs[i].OutLectures, SlDStyler.CommonStyle);
                Report.Document.Fill($"BL{i + startA}", DataFs[i].OutPractices, SlDStyler.CommonStyle);
                Report.Document.Fill($"BM{i + startA}", DataFs[i].OutLabs, SlDStyler.CommonStyle);
                Report.Document.Fill($"BN{i + startA}", DataFs[i].OutSeminars, SlDStyler.CommonStyle); // [outdated]
                Report.Document.Fill($"BO{i + startA}", DataFs[i].OutIndLessons, SlDStyler.CommonStyle); // [outdated] 
                Report.Document.Fill($"BP{i + startA}", DataFs[i].OutConsultInSemesters, SlDStyler.CommonStyle);
                Report.Document.Fill($"BQ{i + startA}", DataFs[i].OutConsultForExams, SlDStyler.CommonStyle);
                Report.Document.Fill($"BR{i + startA}", DataFs[i].OutVerifyingOfTestsA, SlDStyler.CommonStyle);
                Report.Document.Fill($"BS{i + startA}", DataFs[i].OutVerifyingOfTestsB, SlDStyler.CommonStyle); // [outdated]

                Report.Document.Fill($"BT{i + startA}", DataFs[i].OutAnalitycalWorks, SlDStyler.CommonStyle); // [outdated]
                Report.Document.Fill($"BU{i + startA}", DataFs[i].OutCalculatedWorks, SlDStyler.CommonStyle); // [outdated]
                Report.Document.Fill($"BV{i + startA}", DataFs[i].OutKrKp, SlDStyler.CommonStyle);

                Report.Document.Fill($"BW{i + startA}", DataFs[i].OutControlEvaluations, SlDStyler.CommonStyle);
                Report.Document.Fill($"BX{i + startA}", DataFs[i].OutControlExams, SlDStyler.CommonStyle);
                Report.Document.Fill($"BY{i + startA}", DataFs[i].OutPracticePreparations, SlDStyler.CommonStyle);
                Report.Document.Fill($"BZ{i + startA}", DataFs[i].OutStateExams, SlDStyler.CommonStyle);
                Report.Document.Fill($"CA{i + startA}", DataFs[i].OutManagedDiplomas, SlDStyler.CommonStyle);
                Report.Document.Fill($"CB{i + startA}", DataFs[i].OutOther, SlDStyler.CommonStyle);
                Report.Document.Fill($"CC{i + startA}", DataFs[i].OutTotal, SlDStyler.SummaryStyle);
                Report.Document.Fill($"CD{i + startA}", DataFs[i].OutActive, SlDStyler.SummaryStyle);

                #endregion
            }
        }
        protected void BuildSecondSemester()
        {
            Report.Document.SelectWorksheet(BuildConfiguration.SheetNameB);
            int startB = BuildConfiguration.StartFromB;
            int length = DataSs.Count;

            for (int i = 0; i < length; i++)
            {
                int n = i + 1;
                Report.Document.Fill($"A{i + startB}", n, SlDStyler.BoldStyle);

                Report.Document.Fill($"D{i + startB}", DataSs[i].SubjectName, SlDStyler.CommonStyle);
                Report.Document.Fill($"T{i + startB}", DataSs[i].Faculty, SlDStyler.CommonStyle);
                Report.Document.Fill($"U{i + startB}", DataSs[i].Specialty, SlDStyler.CommonStyle);
                Report.Document.Fill($"V{i + startB}", DataSs[i].Specialization, SlDStyler.CommonStyle);
                Report.Document.Fill($"W{i + startB}", DataSs[i].Course, SlDStyler.CommonStyle);
                Report.Document.Fill($"X{i + startB}", DataSs[i].StudentsCount, SlDStyler.CommonStyle);
                Report.Document.Fill($"Y{i + startB}", DataSs[i].GroupsCipher, SlDStyler.CommonStyle);
                Report.Document.Fill($"Z{i + startB}", DataSs[i].RealQuantityOfGroups, SlDStyler.CommonStyle);
                Report.Document.Fill($"AA{i + startB}", DataSs[i].QuantityOfThreads, SlDStyler.CommonStyle);

                Report.Document.Fill($"AB{i + startB}", DataSs[i].InLectures, SlDStyler.InputStyle);
                Report.Document.Fill($"AC{i + startB}", DataSs[i].InLabs, SlDStyler.InputStyle);
                Report.Document.Fill($"AD{i + startB}", DataSs[i].InPractices, SlDStyler.InputStyle);
                Report.Document.Fill($"AF{i + startB}", DataSs[i].InSeminars, SlDStyler.InputStyle); // [outdated]
                Report.Document.Fill($"AG{i + startB}", DataSs[i].InIndLessons, SlDStyler.InputStyle); // [outdated]
                Report.Document.Fill($"AH{i + startB}", DataSs[i].InPracticeWeeks, SlDStyler.InputStyle); // [outdated]
                Report.Document.Fill($"AI{i + startB}", DataSs[i].InIndependentWorks, SlDStyler.InputStyle);
                Report.Document.Fill($"AJ{i + startB}", DataSs[i].InDiplomaProjects, SlDStyler.InputStyle); // [outdated]
                Report.Document.Fill($"AK{i + startB}", DataSs[i].InStateSertifications, SlDStyler.InputStyle); // [outdated]
                Report.Document.Fill($"AL{i + startB}", DataSs[i].InCourseProjects, SlDStyler.InputStyle);
                Report.Document.Fill($"AN{i + startB}", DataSs[i].InExams, SlDStyler.InputStyle);
                Report.Document.Fill($"AN{i + startB}", DataSs[i].InEvaluations, SlDStyler.InputStyle);

                Report.Document.Fill($"CC{i + startB}", DataSs[i].OutLectures, SlDStyler.CommonStyle);
                Report.Document.Fill($"CD{i + startB}", DataSs[i].OutPractices, SlDStyler.CommonStyle);
                Report.Document.Fill($"CE{i + startB}", DataSs[i].OutLabs, SlDStyler.CommonStyle);
                Report.Document.Fill($"CF{i + startB}", DataSs[i].OutSeminars, SlDStyler.CommonStyle); // [outdated]
                Report.Document.Fill($"CG{i + startB}", DataSs[i].OutIndLessons, SlDStyler.CommonStyle); // [outdated] 
                Report.Document.Fill($"CH{i + startB}", DataSs[i].OutConsultInSemesters, SlDStyler.CommonStyle);
                Report.Document.Fill($"CI{i + startB}", DataSs[i].OutConsultForExams, SlDStyler.CommonStyle);
                Report.Document.Fill($"CJ{i + startB}", DataSs[i].OutVerifyingOfTestsA, SlDStyler.CommonStyle);
                Report.Document.Fill($"CK{i + startB}", DataSs[i].OutVerifyingOfTestsB, SlDStyler.CommonStyle); // [outdated]

                Report.Document.Fill($"CL{i + startB}", DataSs[i].OutAnalitycalWorks, SlDStyler.CommonStyle); // [outdated]
                Report.Document.Fill($"CM{i + startB}", DataSs[i].OutCalculatedWorks, SlDStyler.CommonStyle); // [outdated]
                Report.Document.Fill($"CN{i + startB}", DataSs[i].OutKrKp, SlDStyler.CommonStyle);

                Report.Document.Fill($"CP{i + startB}", DataSs[i].OutControlEvaluations, SlDStyler.CommonStyle);
                Report.Document.Fill($"CQ{i + startB}", DataSs[i].OutControlExams, SlDStyler.CommonStyle);
                Report.Document.Fill($"CR{i + startB}", DataSs[i].OutPracticePreparations, SlDStyler.CommonStyle);
                Report.Document.Fill($"CS{i + startB}", DataSs[i].OutStateExams, SlDStyler.CommonStyle);
                Report.Document.Fill($"CT{i + startB}", DataSs[i].OutManagedDiplomas, SlDStyler.CommonStyle);
                Report.Document.Fill($"CU{i + startB}", DataSs[i].OutOther, SlDStyler.CommonStyle);
                Report.Document.Fill($"CV{i + startB}", DataSs[i].OutTotal, SlDStyler.SummaryStyle);
                Report.Document.Fill($"CW{i + startB}", DataSs[i].OutActive, SlDStyler.SummaryStyle);
            }
        }

        protected override void BuildSummary()
        {
            BuildFirstSummary();
            BuildSecondSummary();
        }

        protected void BuildFirstSummary() {}
        protected void BuildSecondSummary() {}
    }

    internal class SemesterDayFormatReportBuilder : CommonDayFormatReportBuilder
    {   
        protected override void BuildBody()
        {
            Report.Document = new SLDocument(BuildConfiguration.PathToSample, Report.Name);
            if(SemesterType == SemesterType.First) BuildFirstSemester();
            if(SemesterType == SemesterType.Second) BuildSecondSemester();
        }

        protected override void BuildSummary()
        {
            if(SemesterType == SemesterType.First) BuildFirstSummary();
            if(SemesterType == SemesterType.Second) BuildSecondSummary();
        }
    }

    internal class CommonExtraFormatReportBuilder : ReportBuilder
    {
        protected List<ExtraFormatData> DataFs;
        protected List<ExtraFormatData> DataSs;
        protected SemesterType SemesterType;

        protected override void InitializeData()
        {
            var eqParams = (EntryQueryContext)BuildConfiguration.DataContext;
            SemesterType = (SemesterType)eqParams.Semester;

            if (SemesterType == SemesterType.Both)
            {
                DataFs = DataService.GetExtraFormatData(eqParams.Loading, (byte)SemesterType.First);
                DataSs = DataService.GetExtraFormatData(eqParams.Loading, (byte)SemesterType.Second);
            }
            else if (SemesterType == SemesterType.First)
                DataFs = DataService.GetExtraFormatData(eqParams.Loading, (byte)SemesterType.First);
            else if (SemesterType == SemesterType.Second)
                DataSs = DataService.GetExtraFormatData(eqParams.Loading, (byte)SemesterType.Second);
        }

        protected override void BuildHeader()
        {
            int from = DataFs[0].Year;
            int to = from++;
            string semesterValue = SemesterType == SemesterType.Both
                ? string.Empty
                : SemesterType == SemesterType.First ? "1" : "2";
            semesterValue += " семестр";

            Report.Name = $"Отчёт по ЗФ обучения за {from}-{to},{semesterValue} {DateTime.Today.ToShortDateString()} .xlsx";
        }

        protected override void BuildBody()
        {
            Report.Document = new SLDocument(BuildConfiguration.PathToSample, Report.Name);
            BuildFirstSemester();
            BuildSecondSemester();
        }

        protected void BuildFirstSemester()
        {
            int startA = BuildConfiguration.StartFromA;
            int length = DataFs.Count;
            Report.Document.SelectWorksheet(BuildConfiguration.SheetNameA);

            for (int i = 0; i < length; i++)
            {
                int n = i + 1;
                Report.Document.Fill($"B{i + startA}", n, SlDStyler.BoldStyle);

                Report.Document.Fill($"C{i + startA}", DataFs[i].Subject, SlDStyler.CommonStyle);
                Report.Document.Fill($"D{i + startA}", DataFs[i].Specialty, SlDStyler.CommonStyle);
                Report.Document.Fill($"F{i + startA}", DataFs[i].Course, SlDStyler.CommonStyle);
                Report.Document.Fill($"G{i + startA}", DataFs[i].QuantityOfStudents, SlDStyler.CommonStyle);
                Report.Document.Fill($"H{i + startA}", DataFs[i].GroupsCipher, SlDStyler.CommonStyle);
                Report.Document.Fill($"I{i + startA}", DataFs[i].QuantityOfGroups, SlDStyler.CommonStyle);
                Report.Document.Fill($"J{i + startA}", DataFs[i].QuantityOfThreads, SlDStyler.CommonStyle);

                Report.Document.Fill($"M{i + startA}", DataFs[i].InLectures, SlDStyler.InputStyle);
                Report.Document.Fill($"N{i + startA}", DataFs[i].InPractices, SlDStyler.InputStyle);
                Report.Document.Fill($"O{i + startA}", DataFs[i].InLabs, SlDStyler.InputStyle);
                Report.Document.Fill($"P{i + startA}", DataFs[i].InSeminar, SlDStyler.InputStyle); // [outdated]
                Report.Document.Fill($"Q{i + startA}", DataFs[i].InIndLessons, SlDStyler.InputStyle); // [outdated]
                Report.Document.Fill($"R{i + startA}", DataFs[i].InPracticeWeeks, SlDStyler.InputStyle); // [outdated]
                Report.Document.Fill($"S{i + startA}", DataFs[i].InIndependentWorks, SlDStyler.InputStyle);
                Report.Document.Fill($"T{i + startA}", DataFs[i].InDiplomaProjects, SlDStyler.InputStyle);
                Report.Document.Fill($"AN{i + startA}", DataFs[i].InEvaluation, SlDStyler.InputStyle);
                Report.Document.Fill($"AO{i + startA}", DataFs[i].InProjects, SlDStyler.InputStyle);
                Report.Document.Fill($"AP{i + startA}", DataFs[i].InExam, SlDStyler.InputStyle);

                Report.Document.Fill($"AQ{i + startA}", DataFs[i].OutLectures, SlDStyler.CommonStyle);
                Report.Document.Fill($"AR{i + startA}", DataFs[i].OutPractices, SlDStyler.CommonStyle);
                Report.Document.Fill($"AS{i + startA}", DataFs[i].OutLabs, SlDStyler.CommonStyle);
                Report.Document.Fill($"AT{i + startA}", DataFs[i].OutSeminar, SlDStyler.CommonStyle); // [outdated]
                Report.Document.Fill($"AU{i + startA}", DataFs[i].OutIndLessons, SlDStyler.CommonStyle); // [outdated]
                Report.Document.Fill($"AV{i + startA}", DataFs[i].OutSemesterConsults, SlDStyler.CommonStyle);
                Report.Document.Fill($"AW{i + startA}", DataFs[i].OutExamConsults, SlDStyler.CommonStyle);
                Report.Document.Fill($"AX{i + startA}", DataFs[i].OutVerifyingOfTestsA, SlDStyler.CommonStyle); // [audit]
                Report.Document.Fill($"AY{i + startA}", DataFs[i].OutVerifyingOfTestsB, SlDStyler.CommonStyle);

                Report.Document.Fill($"AZ{i + startA}", DataFs[i].OutWrittenWorks, SlDStyler.CommonStyle);
                Report.Document.Fill($"BA{i + startA}", DataFs[i].OutAnalitycalWorks, SlDStyler.CommonStyle);
                Report.Document.Fill($"BB{i + startA}", DataFs[i].OutProjects, SlDStyler.CommonStyle);

                Report.Document.Fill($"BC{i + startA}", DataFs[i].OutEvaluation, SlDStyler.CommonStyle);
                Report.Document.Fill($"BD{i + startA}", DataFs[i].OutExam, SlDStyler.CommonStyle);
                Report.Document.Fill($"BE{i + startA}", DataFs[i].OutPracticePreparation, SlDStyler.CommonStyle);
                Report.Document.Fill($"BF{i + startA}", DataFs[i].OutStateExam, SlDStyler.CommonStyle);
                Report.Document.Fill($"BG{i + startA}", DataFs[i].OutDiplomManagement, SlDStyler.CommonStyle);
                Report.Document.Fill($"BH{i + startA}", DataFs[i].OutAspirants, SlDStyler.CommonStyle);
                Report.Document.Fill($"BI{i + startA}", DataFs[i].OutTotal, SlDStyler.SummaryStyle);
                Report.Document.Fill($"BJ{i + startA}", DataFs[i].OutActive, SlDStyler.SummaryStyle);

            }
        }
        protected void BuildSecondSemester()
        {
            int startB = BuildConfiguration.StartFromB;
            int length = DataSs.Count;
            Report.Document.SelectWorksheet(BuildConfiguration.SheetNameB);

            for (int i = 0; i < length; i++)
            {
                int n = i ++;
                Report.Document.Fill($"C{i + startB}", n, SlDStyler.BoldStyle);

                Report.Document.Fill($"D{i + startB}", DataSs[i].Subject, SlDStyler.CommonStyle);
                Report.Document.Fill($"E{i + startB}", DataSs[i].Specialty, SlDStyler.CommonStyle);
                Report.Document.Fill($"G{i + startB}", DataSs[i].Course, SlDStyler.CommonStyle);
                Report.Document.Fill($"H{i + startB}", DataSs[i].QuantityOfStudents, SlDStyler.CommonStyle);
                Report.Document.Fill($"I{i + startB}", DataSs[i].GroupsCipher, SlDStyler.CommonStyle);
                Report.Document.Fill($"J{i + startB}", DataSs[i].QuantityOfGroups, SlDStyler.CommonStyle);
                Report.Document.Fill($"K{i + startB}", DataSs[i].QuantityOfThreads, SlDStyler.CommonStyle);

                Report.Document.Fill($"X{i + startB}", DataSs[i].InLectures, SlDStyler.InputStyle);
                Report.Document.Fill($"Y{i + startB}", DataSs[i].InPractices, SlDStyler.InputStyle);
                Report.Document.Fill($"Z{i + startB}", DataSs[i].InLabs, SlDStyler.InputStyle);
                Report.Document.Fill($"AA{i + startB}", DataSs[i].InSeminar, SlDStyler.InputStyle); // [outdated]
                Report.Document.Fill($"AB{i + startB}", DataSs[i].InIndLessons, SlDStyler.InputStyle); // [outdated]
                Report.Document.Fill($"AC{i + startB}", DataSs[i].InPracticeWeeks, SlDStyler.InputStyle); // [outdated]
                Report.Document.Fill($"AD{i + startB}", DataSs[i].InIndependentWorks, SlDStyler.InputStyle);
                Report.Document.Fill($"AE{i + startB}", DataSs[i].InExam, SlDStyler.InputStyle);
                Report.Document.Fill($"AF{i + startB}", DataSs[i].InEvaluation, SlDStyler.InputStyle);
                Report.Document.Fill($"AG{i + startB}", DataSs[i].InProjects, SlDStyler.InputStyle);
                Report.Document.Fill($"AH{i + startB}", DataSs[i].InTest, SlDStyler.InputStyle);

                Report.Document.Fill($"BJ{i + startB}", DataSs[i].OutLectures, SlDStyler.CommonStyle);
                Report.Document.Fill($"BK{i + startB}", DataSs[i].OutPractices, SlDStyler.CommonStyle);
                Report.Document.Fill($"BL{i + startB}", DataSs[i].OutLabs, SlDStyler.CommonStyle);
                Report.Document.Fill($"BM{i + startB}", DataSs[i].OutSeminar, SlDStyler.CommonStyle); // [outdated]
                Report.Document.Fill($"BN{i + startB}", DataSs[i].OutIndLessons, SlDStyler.CommonStyle); // [outdated]
                Report.Document.Fill($"BO{i + startB}", DataSs[i].OutSemesterConsults, SlDStyler.CommonStyle);
                Report.Document.Fill($"BP{i + startB}", DataSs[i].OutExamConsults, SlDStyler.CommonStyle);
                Report.Document.Fill($"BQ{i + startB}", DataSs[i].OutVerifyingOfTestsA, SlDStyler.CommonStyle); // [audit]
                Report.Document.Fill($"BR{i + startB}", DataSs[i].OutVerifyingOfTestsB, SlDStyler.CommonStyle);

                Report.Document.Fill($"BS{i + startB}", DataSs[i].OutWrittenWorks, SlDStyler.CommonStyle);
                Report.Document.Fill($"BT{i + startB}", DataSs[i].OutAnalitycalWorks, SlDStyler.CommonStyle);
                Report.Document.Fill($"BU{i + startB}", DataSs[i].OutProjects, SlDStyler.CommonStyle);

                Report.Document.Fill($"BV{i + startB}", DataSs[i].OutEvaluation, SlDStyler.CommonStyle);
                Report.Document.Fill($"BW{i + startB}", DataSs[i].OutOralExams, SlDStyler.CommonStyle);
                Report.Document.Fill($"BX{i + startB}", DataSs[i].OutWrittenExams, SlDStyler.CommonStyle);
                Report.Document.Fill($"BY{i + startB}", DataSs[i].OutExam, SlDStyler.CommonStyle);

                Report.Document.Fill($"BZ{i + startB}", DataSs[i].OutDiplomManagement, SlDStyler.CommonStyle);
                Report.Document.Fill($"CA{i + startB}", DataSs[i].OutDekParticipation, SlDStyler.CommonStyle);
                Report.Document.Fill($"CB{i + startB}", DataSs[i].OutCheckWriteWorks, SlDStyler.CommonStyle);
                Report.Document.Fill($"CC{i + startB}", DataSs[i].OutAspirants, SlDStyler.CommonStyle);

                Report.Document.Fill($"CD{i + startB}", DataSs[i].OutTotal, SlDStyler.SummaryStyle);
                Report.Document.Fill($"CE{i + startB}", DataSs[i].OutActive, SlDStyler.SummaryStyle);
            }
        }

        protected override void BuildSummary()
        {
            BuildFirstSummary();
            BuildSecondSummary();
        }

        protected void BuildFirstSummary() { }
        protected void BuildSecondSummary() { }
    }

    internal class SemesterExtraFormatReportBuilder : CommonExtraFormatReportBuilder
    {
        protected override void BuildBody()
        {
            Report.Document = new SLDocument(BuildConfiguration.PathToSample, Report.Name);
            if (SemesterType == SemesterType.First) BuildFirstSemester();
            if (SemesterType == SemesterType.Second) BuildSecondSemester();
        }

        protected override void BuildSummary()
        {
            if (SemesterType == SemesterType.First) BuildFirstSummary();
            if (SemesterType == SemesterType.Second) BuildSecondSummary();
        }
    }




    internal class DayTeachReportBuilder : ReportBuilder
    {
        protected List<DayTeachLoadsFormatData> DataTLFs;
        protected List<DayTeachLoadsFormatData> DataTLSs;
        protected SemesterType SemesterType;

        protected override void InitializeData()
        {
            var eqParams = (EntryQueryContext)BuildConfiguration.DataContext;
            SemesterType = (SemesterType)eqParams.Semester;

            if (SemesterType == SemesterType.Both)
            {
                DataTLFs = DataService.GetDayTeachLoadFormatData(eqParams.Loading, (byte)SemesterType.First);
                DataTLSs = DataService.GetDayTeachLoadFormatData(eqParams.Loading, (byte)SemesterType.Second);
            }
            else if (SemesterType == SemesterType.First)
                DataTLFs = DataService.GetDayTeachLoadFormatData(eqParams.Loading, (byte)SemesterType.First);
            else if (SemesterType == SemesterType.Second)
                DataTLSs = DataService.GetDayTeachLoadFormatData(eqParams.Loading, (byte)SemesterType.Second);
        }

        protected override void BuildHeader()
        {
            int from = DataTLFs[0].Year;
            int to = from + 1;
            string semesterValue = SemesterType == SemesterType.Both
               ? string.Empty
               : SemesterType == SemesterType.First ? "1" : "2";
            semesterValue += " семестр";
            Report.Name = $"Отчёт по нагрузке преподавателей за {from}-{to}, {semesterValue} {DateTime.Today.ToShortDateString()}.xlsx";
        }

        protected override void BuildBody()
        {
            Report.Document = new SLDocument(BuildConfiguration.PathToSample, Report.Name);

            BuildFirstSemester();
            BuildSecondSemester();
        }

        protected void BuildFirstSemester()
        {
            int startA = BuildConfiguration.StartFromA;
            Report.Document.SelectWorksheet(BuildConfiguration.SheetNameA);
            int length = DataTLFs.Count;

            for (int i = 0; i < length; i++)
            {
                int n = i + 1;
                #region Fill

                Report.Document.Fill($"A{i + startA}", n, SlDStyler.BoldStyle);

                Report.Document.Fill($"B{i + startA}", $"{DataTLFs[i].LastName} {DataTLFs[i].FirstName}. {DataTLFs[i].ThirdName}." , SlDStyler.CommonStyle);
                Report.Document.Fill($"C{i + startA}", DataTLFs[i].SubjectName, SlDStyler.CommonStyle);
                Report.Document.Fill($"D{i + startA}", DataTLFs[i].Specialty, SlDStyler.CommonStyle);
                Report.Document.Fill($"E{i + startA}", DataTLFs[i].Specialization, SlDStyler.CommonStyle);
                Report.Document.Fill($"F{i + startA}", DataTLFs[i].CourseLiteral, SlDStyler.CommonStyle);
                Report.Document.Fill($"G{i + startA}", DataTLFs[i].EducationDegree, SlDStyler.CommonStyle);
                Report.Document.Fill($"H{i + startA}", DataTLFs[i].StudentsCount, SlDStyler.CommonStyle);
                Report.Document.Fill($"I{i + startA}", DataTLFs[i].GroupsCipher, SlDStyler.CommonStyle);

                Report.Document.Fill($"L{i + startA}", DataTLFs[i].Lecture, SlDStyler.CommonStyle);
                Report.Document.Fill($"M{i + startA}", DataTLFs[i].Practice, SlDStyler.CommonStyle);
                Report.Document.Fill($"N{i + startA}", DataTLFs[i].Lab, SlDStyler.CommonStyle);
                Report.Document.Fill($"R{i + startA}", DataTLFs[i].ConsultInSemester, SlDStyler.CommonStyle);
                Report.Document.Fill($"Q{i + startA}", DataTLFs[i].ConsultForExam, SlDStyler.CommonStyle);
                Report.Document.Fill($"S{i + startA}", DataTLFs[i].VerifyingOfTest, SlDStyler.CommonStyle);
                Report.Document.Fill($"T{i + startA}", DataTLFs[i].CourseProjects, SlDStyler.CommonStyle);
                Report.Document.Fill($"X{i + startA}", DataTLFs[i].Dek, SlDStyler.CommonStyle);
                Report.Document.Fill($"Z{i + startA}", DataTLFs[i].ManagedDiploma, SlDStyler.CommonStyle);
                Report.Document.Fill($"AB{i + startA}", DataTLFs[i].Total, SlDStyler.CommonStyle);
                Report.Document.Fill($"AC{i + startA}", DataTLFs[i].Active, SlDStyler.CommonStyle);
                #endregion
            }
        }
        protected void BuildSecondSemester()
        {
            Report.Document.SelectWorksheet(BuildConfiguration.SheetNameB);
            int startB = BuildConfiguration.StartFromB;
            int length = DataTLSs.Count;

            for (int i = 0; i < length; i++)
            {
                int n = i + 1;
                Report.Document.Fill($"A{i + startB}", n, SlDStyler.BoldStyle);

                Report.Document.Fill($"B{i + startB}", $"{DataTLSs[i].LastName} {DataTLSs[i].FirstName}. {DataTLSs[i].ThirdName}.", SlDStyler.CommonStyle);
                Report.Document.Fill($"C{i + startB}", DataTLSs[i].SubjectName, SlDStyler.CommonStyle);
                Report.Document.Fill($"D{i + startB}", DataTLSs[i].Specialty, SlDStyler.CommonStyle);
                Report.Document.Fill($"E{i + startB}", DataTLSs[i].Specialization, SlDStyler.CommonStyle);
                Report.Document.Fill($"F{i + startB}", DataTLSs[i].CourseLiteral, SlDStyler.CommonStyle);
                Report.Document.Fill($"G{i + startB}", DataTLSs[i].EducationDegree, SlDStyler.CommonStyle);
                Report.Document.Fill($"H{i + startB}", DataTLSs[i].StudentsCount, SlDStyler.CommonStyle);
                Report.Document.Fill($"I{i + startB}", DataTLSs[i].GroupsCipher, SlDStyler.CommonStyle);

                Report.Document.Fill($"L{i + startB}", DataTLSs[i].Lecture, SlDStyler.CommonStyle);
                Report.Document.Fill($"M{i + startB}", DataTLSs[i].Practice, SlDStyler.CommonStyle);
                Report.Document.Fill($"N{i + startB}", DataTLSs[i].Lab, SlDStyler.CommonStyle);
                Report.Document.Fill($"R{i + startB}", DataTLSs[i].ConsultInSemester, SlDStyler.CommonStyle);
                Report.Document.Fill($"Q{i + startB}", DataTLSs[i].ConsultForExam, SlDStyler.CommonStyle);
                Report.Document.Fill($"S{i + startB}", DataTLSs[i].VerifyingOfTest, SlDStyler.CommonStyle);
                Report.Document.Fill($"T{i + startB}", DataTLSs[i].CourseProjects, SlDStyler.CommonStyle);
                Report.Document.Fill($"X{i + startB}", DataTLSs[i].Dek, SlDStyler.CommonStyle);
                Report.Document.Fill($"Z{i + startB}", DataTLSs[i].ManagedDiploma, SlDStyler.CommonStyle);
                Report.Document.Fill($"AB{i + startB}", DataTLSs[i].Total, SlDStyler.CommonStyle);
                Report.Document.Fill($"AC{i + startB}", DataTLSs[i].Active, SlDStyler.CommonStyle);
            }
        }

        protected override void BuildSummary()
        {
            BuildFirstSummary();
            BuildSecondSummary();
        }

        protected void BuildFirstSummary() { }
        protected void BuildSecondSummary() { }
    }



    internal class ExtramuralTeachReportBuilder : ReportBuilder
    {
        protected List<ExtramuralTeachLoadsFormatData> DataTLFs;
        protected List<ExtramuralTeachLoadsFormatData> DataTLSs;
        protected SemesterType SemesterType;

        protected override void InitializeData()
        {
            var eqParams = (EntryQueryContext)BuildConfiguration.DataContext;
            SemesterType = (SemesterType)eqParams.Semester;

            if (SemesterType == SemesterType.Both)
            {
                DataTLFs = DataService.GetExtramuralTeachLoadsFormatData(eqParams.Loading, (byte)SemesterType.First);
                DataTLSs = DataService.GetExtramuralTeachLoadsFormatData(eqParams.Loading, (byte)SemesterType.Second);
            }
            else if (SemesterType == SemesterType.First)
                DataTLFs = DataService.GetExtramuralTeachLoadsFormatData(eqParams.Loading, (byte)SemesterType.First);
            else if (SemesterType == SemesterType.Second)
                DataTLSs = DataService.GetExtramuralTeachLoadsFormatData(eqParams.Loading, (byte)SemesterType.Second);
        }

        protected override void BuildHeader()
        {
            int from = DataTLFs[0].Year;
            int to = from + 1;
            string semesterValue = SemesterType == SemesterType.Both
               ? string.Empty
               : SemesterType == SemesterType.First ? "1" : "2";
            semesterValue += " семестр";
            Report.Name = $"Отчёт нагрузке преподавателей за {from}-{to}, {semesterValue} {DateTime.Today.ToShortDateString()}.xlsx";
        }

        protected override void BuildBody()
        {
            Report.Document = new SLDocument(BuildConfiguration.PathToSample, Report.Name);

            BuildFirstSemester();
            BuildSecondSemester();
        }

        protected void BuildFirstSemester()
        {
            int startA = BuildConfiguration.StartFromA;
            Report.Document.SelectWorksheet(BuildConfiguration.SheetNameA);
            int length = DataTLFs.Count;

            for (int i = 0; i < length; i++)
            {
                int n = i + 1;
                #region Fill

                Report.Document.Fill($"A{i + startA}", n, SlDStyler.BoldStyle);

                Report.Document.Fill($"B{i + startA}", $"{DataTLFs[i].LastName} {DataTLFs[i].FirstName}. {DataTLFs[i].ThirdName}.", SlDStyler.CommonStyle);
                Report.Document.Fill($"C{i + startA}", DataTLFs[i].SubjectName, SlDStyler.CommonStyle);
                Report.Document.Fill($"D{i + startA}", DataTLFs[i].CourseLiteral, SlDStyler.CommonStyle);
                Report.Document.Fill($"E{i + startA}", DataTLFs[i].StudentsCount, SlDStyler.CommonStyle);
                Report.Document.Fill($"F{i + startA}", DataTLFs[i].Specialty, SlDStyler.CommonStyle);

                Report.Document.Fill($"H{i + startA}", DataTLFs[i].WrittenWork, SlDStyler.CommonStyle);
                Report.Document.Fill($"I{i + startA}", DataTLFs[i].OralExam, SlDStyler.CommonStyle);
                Report.Document.Fill($"J{i + startA}", DataTLFs[i].WrittenExam, SlDStyler.CommonStyle);
                Report.Document.Fill($"L{i + startA}", DataTLFs[i].Lecture, SlDStyler.CommonStyle);
                Report.Document.Fill($"M{i + startA}", DataTLFs[i].Practice, SlDStyler.CommonStyle);
                Report.Document.Fill($"N{i + startA}", DataTLFs[i].Lab, SlDStyler.CommonStyle);
                Report.Document.Fill($"R{i + startA}", DataTLFs[i].ConsultInSemester, SlDStyler.CommonStyle);
                Report.Document.Fill($"Q{i + startA}", DataTLFs[i].ConsultForExam, SlDStyler.CommonStyle);
                Report.Document.Fill($"S{i + startA}", DataTLFs[i].VerifyingOfTest, SlDStyler.CommonStyle);
                Report.Document.Fill($"T{i + startA}", DataTLFs[i].CourseProjects, SlDStyler.CommonStyle);
                Report.Document.Fill($"U{i + startA}", DataTLFs[i].Evaluation, SlDStyler.CommonStyle);
                Report.Document.Fill($"X{i + startA}", DataTLFs[i].Dek, SlDStyler.CommonStyle);
                Report.Document.Fill($"Y{i + startA}", DataTLFs[i].VerifyingOfWrittenWorks, SlDStyler.CommonStyle);
                Report.Document.Fill($"Z{i + startA}", DataTLFs[i].ManagedDiploma, SlDStyler.CommonStyle);
                Report.Document.Fill($"AB{i + startA}", DataTLFs[i].Total, SlDStyler.CommonStyle);
                Report.Document.Fill($"AC{i + startA}", DataTLFs[i].Active, SlDStyler.CommonStyle);
                #endregion
            }
        }
        protected void BuildSecondSemester()
        {
            Report.Document.SelectWorksheet(BuildConfiguration.SheetNameB);
            int startB = BuildConfiguration.StartFromB;
            int length = DataTLSs.Count;

            for (int i = 0; i < length; i++)
            {
                int n = i + 1;
                Report.Document.Fill($"A{i + startB}", n, SlDStyler.BoldStyle);

                Report.Document.Fill($"B{i + startB}", $"{DataTLSs[i].LastName} {DataTLSs[i].FirstName}. {DataTLSs[i].ThirdName}.", SlDStyler.CommonStyle);
                Report.Document.Fill($"C{i + startB}", DataTLSs[i].SubjectName, SlDStyler.CommonStyle);
                Report.Document.Fill($"D{i + startB}", DataTLSs[i].CourseLiteral, SlDStyler.CommonStyle);
                Report.Document.Fill($"E{i + startB}", DataTLSs[i].StudentsCount, SlDStyler.CommonStyle);
                Report.Document.Fill($"F{i + startB}", DataTLSs[i].Specialty, SlDStyler.CommonStyle);

                Report.Document.Fill($"H{i + startB}", DataTLSs[i].WrittenWork, SlDStyler.CommonStyle);
                Report.Document.Fill($"I{i + startB}", DataTLSs[i].OralExam, SlDStyler.CommonStyle);
                Report.Document.Fill($"J{i + startB}", DataTLSs[i].WrittenExam, SlDStyler.CommonStyle);
                Report.Document.Fill($"L{i + startB}", DataTLSs[i].Lecture, SlDStyler.CommonStyle);
                Report.Document.Fill($"M{i + startB}", DataTLSs[i].Practice, SlDStyler.CommonStyle);
                Report.Document.Fill($"N{i + startB}", DataTLSs[i].Lab, SlDStyler.CommonStyle);
                Report.Document.Fill($"R{i + startB}", DataTLSs[i].ConsultInSemester, SlDStyler.CommonStyle);
                Report.Document.Fill($"Q{i + startB}", DataTLSs[i].ConsultForExam, SlDStyler.CommonStyle);
                Report.Document.Fill($"S{i + startB}", DataTLSs[i].VerifyingOfTest, SlDStyler.CommonStyle);
                Report.Document.Fill($"T{i + startB}", DataTLSs[i].CourseProjects, SlDStyler.CommonStyle);
                Report.Document.Fill($"U{i + startB}", DataTLSs[i].Evaluation, SlDStyler.CommonStyle);
                Report.Document.Fill($"X{i + startB}", DataTLSs[i].Dek, SlDStyler.CommonStyle);
                Report.Document.Fill($"Y{i + startB}", DataTLSs[i].VerifyingOfWrittenWorks, SlDStyler.CommonStyle);
                Report.Document.Fill($"Z{i + startB}", DataTLSs[i].ManagedDiploma, SlDStyler.CommonStyle);
                Report.Document.Fill($"AB{i + startB}", DataTLSs[i].Total, SlDStyler.CommonStyle);
                Report.Document.Fill($"AC{i + startB}", DataTLSs[i].Active, SlDStyler.CommonStyle);
            }
        }

        protected override void BuildSummary()
        {
            BuildFirstSummary();
            BuildSecondSummary();
        }

        protected void BuildFirstSummary() { }
        protected void BuildSecondSummary() { }
    }
}
