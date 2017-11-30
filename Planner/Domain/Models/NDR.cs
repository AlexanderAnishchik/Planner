using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class NDR
    {
        public NDR()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public String Id { get; set; }
        [Required]
        [Display(Name = "ПIБ викладача")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Тип")]
        public string Type { get; set; }
        [Required]
        [Display(Name = "Рiвень")]
        public string Level { get; set; }
        [Required]
        [Display(Name = "Назва/Напрям")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Етап")]
        public string Step { get; set; }
        [Required]
        [Display(Name = "Мiсце та дата проведення")]
        public string Place { get; set; }

        [Required]

        [Display(Name = "ПIБ студента")]
        public string StudentName { get; set; }


        [Display(Name = "Нагороди")]
        public string Awards { get; set; }
        public String UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}