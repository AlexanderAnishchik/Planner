using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class IndivPlanFields
    {
        public IndivPlanFields()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public String Id { get; set; }

        public String DisplayName { get; set; }

        public String SchemaName { get; set; }

        public String Suffix { get; set; }

        public String TabName { get; set; }

        public String TypeId { get; set; }
        [ForeignKey("TypeId")]
        public virtual IndPlanType IndPlanType { get; set; }
    }
}
