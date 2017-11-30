using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Domain.Models
{
    public class PlanMethodicalWork
    {
        public PlanMethodicalWork()
        {
            Id = Guid.NewGuid().ToString();
        }
        [HiddenInput(DisplayValue = false)]
        [Key]
        public String Id { get; set; }

        public int OrderNumber { get; set; }
        public string Content { get; set; }
        public string Result { get; set; }
        public int DurationTime { get; set; }
        public int PlannedVolume { get; set; }
        public int ActualVolume { get; set; }
        public String ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
