
namespace Load.Mapper.SemesterFormat
{
    public class ExtraFormatSemester : FormatSemester
    {
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
        // for II semester
        public double CheckingTests { get; set; }
        public double DiplomManagement { get; set; }
        public double DekParticipation { get; set; }
        public double CheckWriteWorks { get; set; }
        public double Protection { get; set; }
    }
}
