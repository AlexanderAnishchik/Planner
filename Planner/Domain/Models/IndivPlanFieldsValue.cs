using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Domain.Models
{
    public class IndivPlanFieldsValue
    {
        public IndivPlanFieldsValue()
        {
            Id = Guid.NewGuid().ToString();
        }
        [HiddenInput(DisplayValue = false)]
        [Key]
        public String Id { get; set; }
        public string SchemaName { get; set; }
        /// <summary>
        /// Фактический обьем
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// Плановый обьем обьем
        /// </summary>
        public string PlannedValue { get; set; }
        public String ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
