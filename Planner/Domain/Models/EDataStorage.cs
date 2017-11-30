using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class EDataStorage
    {
        public EDataStorage()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public String Id { get; set; }

        public int Semester { get; set; }
        public int N { get; set; }
        public string Subject { get; set; }
        public string Specialty { get; set; }
        public string Extramural { get; set; }
        public double Course { get; set; }
        public double QuanOfStud { get; set; }
        public double QuanOfThread { get; set; }
        public double CommonTime { get; set; }
        public double Lecture { get; set; }
        public double Practice { get; set; }
        public double Lab { get; set; }
        public double IndWork { get; set; }
        public string Exam { get; set; }
        public string Eval { get; set; }
        public double Test { get; set; }
        public double NormKR_KP { get; set; }
        public double CLecture { get; set; }
        public double CPractice { get; set; }
        public double CLab { get; set; }
        public double CConsultInSem { get; set; }
        public double CConsultForExam { get; set; }
        public double CVerifyingTest { get; set; }
        public double CCourseProject { get; set; }
        public double CEval { get; set; }
        public double COralExam { get; set; }
        public double CManagedDiploma { get; set; }
        public double CDek { get; set; }
        public double CVerifyingOfWrWork { get; set; }
        public double CProtection { get; set; }
        public double CTotal { get; set; }
        public double CActive { get; set; }

        public String LoadingListId { get; set; }
        [ForeignKey("LoadingListId")]
        public virtual LoadingList LoadingList { get; set; }
    }
}
