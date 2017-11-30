using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Specialty
    {
        public Specialty()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public String Id { get; set; }
        public String Code { get; set; }
        public String Description { get; set; }

        public virtual ICollection<DayEntryLoad> DayEntryLoads { get; set; }
    }
}
