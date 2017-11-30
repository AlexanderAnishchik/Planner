using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class ExtramuralEntryLoad
    {
        public ExtramuralEntryLoad()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public String Id { get; set; }

        public string DepartmentCipher { get; set; }
        public string Extramural { get; set; }
        public double Course { get; set; }
        public double QuantityOfStudents { get; set; }
        public double QuantityOfGroups { get; set; }
        public double QuantityOfThreads { get; set; }
        public double NumOfThread { get; set; }
        public string MajorSpecialty { get; set; }
        public double CommonTime { get; set; }
        public double Credits { get; set; }
        public double F_Lecture { get; set; }
        public double F_Practical { get; set; }
        public double F_Lab { get; set; }
        public double F_IndividualWork { get; set; }
        public string F_Exam { get; set; }
        public string F_Evaluation { get; set; }
        public string F_KR { get; set; }
        public double F_Test { get; set; }
        public double F_LimitOnProjects { get; set; } //+
        public double S_Lecture { get; set; }
        public double S_Practical { get; set; }
        public double S_Lab { get; set; }
        public double S_IndividualWork { get; set; }
        public string S_Exam { get; set; }
        public string S_Evaluation { get; set; }
        public string S_KR { get; set; }
        public double S_Test { get; set; }
        public double S_LimitOnProjects { get; set; } //+

        public String LoadingListId { get; set; }
        [ForeignKey("LoadingListId")]
        public virtual LoadingList LoadingList { get; set; }

        public String DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        public String SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }

        public String SpecialtyId { get; set; }
        [ForeignKey("SpecialtyId")]
        public virtual Specialty Specialty { get; set; }

        public virtual ICollection<ExtramuralSemester> ExtramuralSemesters { get; set; }
    }
}
