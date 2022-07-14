using System.ComponentModel.DataAnnotations;

namespace Hackathon_CV_Portal.Web.Models.VacancyModels
{
    public class CreateVacancyDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int Salary { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public DateTime DeadLine { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
