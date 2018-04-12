using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class DayEntryLoad
    {
        public DayEntryLoad()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public String Id { get; set; }

        public string Language { get; set; }
        public string Note { get; set; }
        public string EducationDegree { get; set; }
        public string ConflatedThreads { get; set; }
        public double CountOfCredits { get; set; }
        public double CountOfHours { get; set; }
        public double HoursPerCredit { get; set; }
        public double FSemCoefficient { get; set; }
        public double SSemCoefficient { get; set; }
        public double F_TotalHour { get; set; }
        public double F_Total { get; set; }
        public double F_Lectures { get; set; }
        public double F_Labs { get; set; }
        public double F_Practical { get; set; }
        public double F_IndividualWork { get; set; }
        public string F_CourseProjects { get; set; }
        public string F_Exams { get; set; }
        public string F_Evaluation { get; set; }
        public double S_TotalHour { get; set; }
        public double S_Total { get; set; }
        public double S_Lectures { get; set; }
        public double S_Labs { get; set; }
        public double S_Practical { get; set; }
        public double S_IndividualWork { get; set; }
        public string S_CourseProjects { get; set; }
        public string S_Exams { get; set; }
        public string S_Evaluation { get; set; }
        public string DepartmentCipher { get; set; }
        public double FS_CountOfWeeks { get; set; }
        public double SS_CountOfWeeks { get; set; }
        public double QuantityOfStudents { get; set; }
        public double QuantityOfForeigners { get; set; }
        public string CipherOfGroups { get; set; }
        public double QuantityOfGroupsCritOne { get; set; }
        public double RealQuantityOfGroups { get; set; }
        public double QuantityOfGroupsCritTwo { get; set; }
        public double QuantityOfThreads { get; set; }
        public double CipherOfThreads { get; set; }
        public double KR_KP_DR { get; set; }
        public double Practice { get; set; }
        public double QuantityOfDek { get; set; }
        public string FacultyName { get; set; } //+

        public String LoadingListId { get; set; }
        [ForeignKey("LoadingListId")]
        public virtual LoadingList LoadingList { get; set; }

        public String DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        public String SpecialtyId { get; set; }
        [ForeignKey("SpecialtyId")]
        public virtual Specialty Specialty { get; set; }

        public String SpecializeId { get; set; }
        [ForeignKey("SpecializeId")]
        public virtual Specialize Specialize { get; set; }

        public String CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

        public String SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }

        public virtual ICollection<DaySemester> DaySemesters { get; set; }
    }
}
