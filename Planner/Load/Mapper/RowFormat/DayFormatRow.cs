
namespace Load.Mapper.RowFormat
{
    public class DayFormatRow : FormatRow
    {
        // 45
        #region CommonPart

        // 20
        public string Faculty { get; set; }
        public string Specialty { get; set; }
        public string Specialize { get; set; }
        public string Course { get; set; }
        public string Degree { get; set; }
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

        #endregion

        public DayEntrySemester First { get; set; } = new DayEntrySemester();
        public DayEntrySemester Second { get; set; } = new DayEntrySemester();

        #region Other
        // 5
        public string DepartmentCipher { get; set; }
        public double DepartmentCode { get; set; }
        public double Projects { get; set; }
        public double Practices { get; set; }
        public double QuantityOfMembers { get; set; }

        #endregion
    }

    public class DayEntrySemester
    {
        // 10
        public double TotalHours { get; set; }
        public double Total { get; set; }
        public double Lectures { get; set; }
        public double Labs { get; set; }
        public double Practices { get; set; }
        public double IndependentWorks { get; set; }
        public string Courses { get; set; }
        public string Exam { get; set; }
        public string Evaluation { get; set; }
        public double Coefficient { get; set; }
    }
}
