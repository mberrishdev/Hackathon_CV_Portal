using System.ComponentModel.DataAnnotations;

namespace Hackathon_CV_Portal.Web.Models.VacancyModels
{
    public class CreateVacancyDTO
    {
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int Type { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string SalaryRange { get; set; }
        [Required]
        public DateTime DeadLine { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Responsibility { get; set; }
        [Required]
        public string Qualifications { get; set; }
    }
}
