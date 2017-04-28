using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
   public class Department
    {
        public Department()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public String Id { get; set; }
        public String Name { get; set; }

        // field for *.xls(x)
        public double Code { get; set; } = 0;

        public String FacultyId { get; set; }
        [ForeignKey("FacultyId")]
        public virtual Faculty Faculty { get; set; }

        public virtual ICollection<DepartmentUser> DepartmentUsers { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }

        public virtual ICollection<DayEntryLoad> DayEntryLoads { get; set; }
        public virtual ICollection<ExtramuralEntryLoad> ExtramuralEntryLoads { get; set; }

        public virtual ICollection<LoadingList> LoadingList { get; set; }
    }
}
