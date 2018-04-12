using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Rate
    {
        public Rate()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public String Id { get; set; }
        public Double Value { get; set; }
        public virtual ICollection<DepartmentUser> DepartmentUsers { get; set; }
        //public virtual ICollection<TeachersRate> TeachersRates { get; set; }
    }
}
