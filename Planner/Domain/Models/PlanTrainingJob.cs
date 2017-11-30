using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class PlanTrainingJob
    {
        public PlanTrainingJob()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public String Id { get; set; }
        public string EducationForm { get; set; }
        public int OrderNumber { get; set; }
        public string Subject { get; set; }
        public string DSD { get; set; }
        public int Course { get; set; }
        public int CountStudent { get; set; }
        public string GroupCode { get; set; }
        public int PlannedLectures { get; set; }
        public int DoneLectures { get; set; }
        public int PlannedPract { get; set; }
        public int DonePract { get; set; }
        public int PlannedLaboratory { get; set; }
        public int DoneLaboratory { get; set; }
        public int PlannedSeminar { get; set; }
        public int DoneSeminar { get; set; }
        public int PlannedIndividual { get; set; }
        public int DoneIndividual { get; set; }
        public int PlannedConsultation { get; set; }
        public int DoneConsultation { get; set; }
        public int PlannedExamConsultation { get; set; }
        public int DoneExamConsultation { get; set; }
        public int PlannedCheckControl { get; set; }
        public int DoneCheckControl { get; set; }
        public int PlannedCheckLectureControl { get; set; }
        public int DoneCheckLectureControl { get; set; }
        public int PlannedEAT { get; set; }
        public int DoneEAT { get; set; }
        public int PlannedCGS { get; set; }
        public int DoneCGS { get; set; }
        public int PlannedCoursework { get; set; }
        public int DoneCoursework { get; set; }
        public int PlannedOffsetting { get; set; }
        public int DoneOffsetting { get; set; }
        public int PlannedSemestrExam { get; set; }
        public int DoneSemestrExam { get; set; }
        public int PlannedTrainingPract { get; set; }
        public int DoneTrainingPract { get; set; }
        public int PlannedStateExam { get; set; }
        public int DoneStateExam { get; set; }
        public int PlannedDiploma { get; set; }
        public int DoneDiploma { get; set; }
        public int PlannedPostgraduates { get; set; }
        public int DonePostgraduates { get; set; }
        public int PlannedDEK { get; set; }
        public int DoneDEK { get; set; }
        public String ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
