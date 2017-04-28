using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class DDataStorage
    {
        [Key]
        public String Id { get; set; }

        public int Semester { get; set; }
        public int N { get; set; }
        public string Subject { get; set; }
        public string Faculty { get; set; }
        public string Specialty { get; set; }
        public string Specialize { get; set; }
        public string Course { get; set; }
        public string EduDegree { get; set; }
        public double CountOfStud { get; set; }
        public string CipherOfGroup { get; set; }
        public double QuanOfGroupCritOne { get; set; }
        public double RealQuanGr { get; set; }
        public string QuanOfGroupCritTwo { get; set; }
        public double QuanOfThread { get; set; }
        public double TotalHour { get; set; }
        public double Total { get; set; }
        public double Lecture { get; set; }
        public double Practice { get; set; }
        public double Lab { get; set; }
        public double IndWork { get; set; }
        public string CourseProjects { get; set; }
        public string Exam { get; set; }
        public string Eval { get; set; }
        public double CLecture { get; set; }
        public double CPractice { get; set; }
        public double CLab { get; set; }
        public double CConsultInSem { get; set; }
        public double CConsultForExam { get; set; }
        public double CCheckOfTests { get; set; }
        public double CKR_KP { get; set; }
        public double CEval { get; set; }
        public double CExam { get; set; }
        public double CPracticePreparation { get; set; }
        public double CDek { get; set; }
        public double CStateExam { get; set; }
        public double CManagedDiploma { get; set; }
        public double COther { get; set; }
        public double CTotal { get; set; }
        public double CActive { get; set; }

        public String LoadingListId { get; set; }
        [ForeignKey("LoadingListId")]
        public virtual LoadingList LoadingList { get; set; }
    }
}
