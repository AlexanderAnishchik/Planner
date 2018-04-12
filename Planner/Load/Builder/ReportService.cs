using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using SpreadsheetLight;

namespace Load.Builder
{
    public enum ReportType : byte
    {
        None = 0,
        CommonDayFormatReport = 10,
        SemesterDayFormatReport = 11,
        CommonExtraFormatReport = 12,
        SemesterExtraFormatReport = 13,
        CommonDistribution = 14,
        TeacherDayLoading = 20,
        TeacherExtraLoading = 21,
        TeacherLoading = 22
    }

    internal class ReportBuildConfiguration
    {
        public IDataContext DataContext { get; set; }
        public string PathToSample { get; set; }
    }

    internal class EntryReportBuildConfiguration : ReportBuildConfiguration
    {
        public int StartFromA { get; set; }
        public int StartFromB { get; set; }
        public string SheetNameA { get; set; }
        public string SheetNameB { get; set; }
    }

    public class WrappedReport
    {
        public SLDocument Document { get; set; }
        public string Name { get; set; }
        public readonly string Schema = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    }

    public class ReportService
    {
        private static readonly Dictionary<ReportType, ReportBuildConfiguration> ReportBuildConfigurations;
        private static readonly Dictionary<ReportType, ReportBuilder> ReportBuilders;

        static ReportService()
        {
            string folder = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();

            ReportBuildConfigurations = new Dictionary<ReportType, ReportBuildConfiguration>
            {
                [ReportType.CommonDayFormatReport] = new EntryReportBuildConfiguration()
                {
                    PathToSample = Path.Combine(folder, "CommonDayFormatReport.xlsx"),
                    SheetNameA = "1 семестр",
                    SheetNameB = "2 семестр",
                    StartFromA = 17,
                    StartFromB = 5,
                },
                [ReportType.CommonExtraFormatReport] = new EntryReportBuildConfiguration()
                {
                    PathToSample = Path.Combine(folder, "CommonExtraFormatReport.xlsx"),
                    SheetNameA = "1 семестр",
                    SheetNameB = "2 семестр",
                    StartFromA = 23,
                    StartFromB = 7,
                },
                [ReportType.SemesterDayFormatReport] = new EntryReportBuildConfiguration()
                {
                    PathToSample = Path.Combine(folder, "CommonDayFormatReport.xlsx"),
                    SheetNameA = "1 семестр",
                    SheetNameB = "2 семестр",
                    StartFromA = 17,
                    StartFromB = 5
                },
                [ReportType.SemesterExtraFormatReport] = new EntryReportBuildConfiguration()
                {
                    PathToSample = Path.Combine(folder, "CommonExtraFormatReport.xlsx"),
                    SheetNameA = "1 семестр",
                    SheetNameB = "2 семестр",
                    StartFromA = 23,
                    StartFromB = 7
                },
                [ReportType.TeacherDayLoading] = new EntryReportBuildConfiguration()
                {
                    PathToSample = Path.Combine(folder, "DayTeachLoadReport.xlsx"),
                    SheetNameA = "1 семестр",
                    SheetNameB = "2 семестр",
                    StartFromA = 12,
                    StartFromB = 4
                },
                [ReportType.TeacherExtraLoading] = new EntryReportBuildConfiguration()
                {
                    PathToSample = Path.Combine(folder, "ExtramuralTeachLoadReport.xlsx"),
                    SheetNameA = "1 семестр",
                    SheetNameB = "2 семестр",
                    StartFromA = 12,
                    StartFromB = 4
                }
            };

            ReportBuilders = new Dictionary<ReportType, ReportBuilder>
            {
                [ReportType.CommonDayFormatReport] = new CommonDayFormatReportBuilder(),
                [ReportType.CommonExtraFormatReport] = new CommonExtraFormatReportBuilder(),
                [ReportType.SemesterDayFormatReport] = new SemesterDayFormatReportBuilder(),
                [ReportType.SemesterExtraFormatReport] = new SemesterExtraFormatReportBuilder(),
                [ReportType.TeacherDayLoading] = new DayTeachReportBuilder(),
                [ReportType.TeacherExtraLoading] = new ExtramuralTeachReportBuilder()
            };
        }

        public WrappedReport Construct(ReportType reportType, IDataContext dataContext)
        {
            if (reportType == ReportType.None || dataContext == null)
                throw new NullReferenceException("ReportService has no actual data!");

            var buildConfiguration = ReportBuildConfigurations[reportType];
            buildConfiguration.DataContext = dataContext;

            var builder = ReportBuilders[reportType];
            builder.SetBuildConfiguration(buildConfiguration);

            return builder.GetReport();
        }
    }
}
