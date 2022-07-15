using System.ComponentModel.DataAnnotations;

namespace Hackathon_CV_Portal.Web.Models.WorkingExperienceModel
{
    public class CreateWorkingExperienceDto
    {
        [Required]
        [Display(Name = "სამსახურის დაწყების დრო")]
        public DateTime StartDate { get; set; }
        [Display(Name = "სამსახურის დასრულების დრო")]
        public DateTime? EndDate { get; set; }
        [Required, MaxLength(100)]
        [Display(Name = "სახელწოდება")]
        public string Name { get; set; }
        [Required, MaxLength(200)]
        [Display(Name = "აღწერა")]
        public string Description { get; set; }
        [Required, MaxLength(50)]
        [Display(Name = "კომპანიის სახელწოდება")]
        public string Company { get; set; }
        [Required, MaxLength(50)]
        [Display(Name = "ქალაქი")]
        public string City { get; set; }
    }
}
