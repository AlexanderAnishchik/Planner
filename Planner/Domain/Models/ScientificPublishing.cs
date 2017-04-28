using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ScientificPublishing
    {
        public ScientificPublishing()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public String Id { get; set; }

        public String UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Год. Поле обов'язкове.")]
		[Range(1930,2100,ErrorMessage = "Дата виходить за границю.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Видання монографій. Поле обов'язкове.")]
        [Range(0, int.MaxValue, ErrorMessage = "Видання монографій. Значення повинно бути цiлим додатнiм числом.")]
        public int Monographs { get; set; }

        [Required(ErrorMessage = "У вітчизняний виданнях. Поле обов'язкове.")]
        [Range(0, int.MaxValue, ErrorMessage = "У вітчизняний виданнях. Значення повинно бути цiлим додатнiм числом.")]
        public int MonographsNationalPublications { get; set; }

        [Required(ErrorMessage = "У зарубіжних виданнях. Поле обов'язкове.")]
        [Range(0, int.MaxValue, ErrorMessage = "У зарубіжних виданнях. Значення повинно бути цiлим додатнiм числом.")]
        public int MonographsForeignJournals { get; set; }

        [Required(ErrorMessage = "Всього публікацій. Поле обов'язкове.")]
        [Range(0, int.MaxValue, ErrorMessage = "Всього публікацій. Значення повинно бути цiлим додатнiм числом.")]
        public int AllPublications { get; set; }

        [Required(ErrorMessage = "Наукові публікаціі в Scopus. Поле обов'язкове.")]
        [Range(0, int.MaxValue, ErrorMessage = "Наукові публікаціі в Scopus. Значення повинно бути цiлим додатнiм числом.")]
        public int ScientificPublicationsInScopus { get; set; }

        [Required(ErrorMessage = "Публікацій (статі, тези), у виданнях з міжнародних науково-метричних баз даних. Поле обов'язкове.")]
        [Range(0, int.MaxValue, ErrorMessage = "Публікацій (статі, тези), у виданнях з міжнародних науково-метричних баз даних. Значення повинно бути цiлим додатнiм числом.")]
        public int ArticlesThesesInNmbd { get; set; }

        [Required(ErrorMessage = "Наукові публікації у зарубіжних виданнях. Поле обов'язкове.")]
        [Range(0, int.MaxValue, ErrorMessage = "Наукові публікації у зарубіжних виданнях. Значення повинно бути цiлим додатнiм числом.")]
        public int ScientificPublicationsInForeignJournals { get; set; }

        [Required(ErrorMessage = "Статті у фахових видання. Поле обов'язкове.")]
        [Range(0, int.MaxValue, ErrorMessage = "Статті у фахових видання. Значення повинно бути цiлим додатнiм числом.")]
        public int ArticlesInProfessionalPublications { get; set; }

        [Required(ErrorMessage = "Публікація наукових статей іноземною мовою. Поле обов'язкове.")]
        [Range(0, int.MaxValue, ErrorMessage = "Публікація наукових статей іноземною мовою. Значення повинно бути цiлим додатнiм числом.")]
        public int ScientificArticlesInForeignLanguages { get; set; }

        [Required(ErrorMessage = "Тези доповідей. Поле обов'язкове.")]
        [Range(0, int.MaxValue, ErrorMessage = "Тези доповідей. Значення повинно бути цiлим додатнiм числом.")]
        public int Abstracts { get; set; }
    }
}
