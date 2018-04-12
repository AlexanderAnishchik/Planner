using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Specialize
    {
        public Specialize()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public String Id { get; set; }
        public String Cipher { get; set; }

        public virtual ICollection<DayEntryLoad> DayEntryLoads { get; set; }
    }
}
