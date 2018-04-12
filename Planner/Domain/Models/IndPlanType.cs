using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class IndPlanType
    {
        public IndPlanType()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public String Id { get; set; }

        public String Name { get; set; }

        public virtual ICollection<IndivPlanFields> IndivPlanFields { get; set; }
    }
}
