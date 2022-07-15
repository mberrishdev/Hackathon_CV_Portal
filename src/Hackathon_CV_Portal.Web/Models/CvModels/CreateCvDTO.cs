using Hackathon_CV_Portal.Domain.Educations;
using Hackathon_CV_Portal.Domain.Skills;
using Hackathon_CV_Portal.Domain.WorkignExperiences;
using System.ComponentModel.DataAnnotations;

namespace Hackathon_CV_Portal.Web.Models.CvModels
{
    public class CreateCvDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime BirtDate { get; set; }
        [Required, MaxLength(15)]
        public string PhoneNumber { get; set; }

        [Required, MaxLength(50)]
        public string Email { get; set; }

        [Required, MaxLength(100)]
        public string Address { get; set; }

        [Required, MaxLength(250)]
        public string AboutMe { get; set; }

        public List<Education> Educations { get; set; }
        public List<WorkingExperience> WorkingExperiences { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
