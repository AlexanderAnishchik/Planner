using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Course
    {
        public Course()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public String Id { get; set; }
        public String Literal { get; set; }

        public virtual ICollection<DayEntryLoad> DayEntryLoads { get; set; }
    }
}
