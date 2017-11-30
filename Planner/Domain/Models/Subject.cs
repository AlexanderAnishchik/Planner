using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Subject
    {
        public Subject()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }
        public String Name { get; set; }

        public virtual ICollection<DayEntryLoad> DayEntryLoads { get; set; }
        public virtual ICollection<ExtramuralEntryLoad> ExtramuralEntryLoads { get; set; }
    }
}
