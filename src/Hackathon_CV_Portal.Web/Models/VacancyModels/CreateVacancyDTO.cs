using System.ComponentModel.DataAnnotations;

namespace Hackathon_CV_Portal.Web.Models.VacancyModels
{
    public class CreateVacancyDTO
    {
        [Required]
        [Display(Name = "კატეგორია")]
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "ტიპი")]
        public int Type { get; set; }
        [Required]
        [Display(Name = "კომპანიის სახელი")]
        public string CompanyName { get; set; }
        [Required]
        [Display(Name = "მისამართი")]
        public string Location { get; set; }
        [Required]
        [Display(Name = "სათაური")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "სახელფასო მოლოდინი")]
        public string SalaryRange { get; set; }
        [Required]
        [Display(Name = "ვაკანსიის გამოგზავნის ბოლო ვადა")]
        public DateTime DeadLine { get; set; }
        [Required]
        [Display(Name = "აღწერა")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "მოვალეობები")]
        public string Responsibility { get; set; }
        [Required]
        [Display(Name = "მოთხოვნები")]
        public string Qualifications { get; set; }
    }
}
