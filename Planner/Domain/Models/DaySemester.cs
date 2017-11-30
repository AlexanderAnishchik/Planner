using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class DaySemester
    {
        public DaySemester()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public String Id { get; set; }

        public byte Semester { get; set; }
        public double Lecture { get; set; }
        public double Practice { get; set; }
        public double Lab { get; set; }
        public double ConsultInSemester { get; set; }
        public double ConsultForExam { get; set; }
        public double VerifyingOfTests { get; set; }
        public double KR_KP { get; set; }
        public double ControlEvaluation { get; set; }
        public double ControlExam { get; set; }
        public double PracticePreparation { get; set; }
        public double Dek { get; set; }
        public double StateExam { get; set; }
        public double ManagedDiploma { get; set; }
        public double Other { get; set; }
        public double Total { get; set; }
        public double Active { get; set; }
        public double EnglishBonus { get; set; }

        public String DayEntryLoadId { get; set; }
        [ForeignKey("DayEntryLoadId")]
        public virtual DayEntryLoad DayEntryLoad { get; set; }
    }
}
