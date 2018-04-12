using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Faculty
    {
        public Faculty()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public String Id { get; set; }
        public String Name { get; set; }

        // for *.xls
        public String ShortName { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}
