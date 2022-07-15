using System.ComponentModel.DataAnnotations;

namespace Hackathon_CV_Portal.Web.Models.EducationModel
{
    public class CreateEducationDto
    {
        [Required]
        [Display(Name = "დაწყების თარიღი")]
        public DateTime StartDate { get; set; }
        [Display(Name = "დასრულების თარიღი (არ არის აუცილებელი)")]
        public DateTime? EndDate { get; set; }
        [Required, MaxLength(100)]
        [Display(Name = "სახელწოდება")]
        public string Name { get; set; }
        [Required, MaxLength(200)]
        [Display(Name = "აღწერა")]
        public string Description { get; set; }
        [Required, MaxLength(50)]
        [Display(Name = "უნივერსიტეტი ან კოლეჯი")]
        public string University { get; set; }
        [Required, MaxLength(50)]
        [Display(Name = "ქალაქი")]
        public string City { get; set; }
    }
}
