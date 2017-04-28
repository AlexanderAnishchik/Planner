
namespace Load.Mapper.RowFormat
{
    public class ExtraFormatRow : FormatRow
    {
        public double DepartmentCode { get; set; }
        public string DepartmentCipher { get; set; }
        public string Subject { get; set; }
        public string SpecialtyCipher { get; set; }
        public string Extra { get; set; }
        public string Course { get; set; }
        public double StudentsCount { get; set; }
        public double QuantityOfGroups { get; set; }
        public double QuantityOfThreads { get; set; }
        public double ThreadNumber { get; set; }
        public string MajorSpecialty { get; set; }
        public double TotalHours { get; set; }
        public double Credits { get; set; }

        public ExtraEntrySemester First { get; set; } = new ExtraEntrySemester();
        public ExtraEntrySemester Second { get; set; } = new ExtraEntrySemester();
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
}
