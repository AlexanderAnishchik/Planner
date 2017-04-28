using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class PlanChange
    {
        public PlanChange()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public String Id { get; set; }

        public int Semester { get; set; }

        public string TypesfJobs { get; set; }
        public string Changes { get; set; }
        public int PlannedVolume { get; set; }
        public int ActualVolume { get; set; }
        public string Base { get; set; }
        public string Signature { get; set; }
        public String ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
