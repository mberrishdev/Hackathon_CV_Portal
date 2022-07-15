using Hackathon_CV_Portal.Domain.Educations;
using Hackathon_CV_Portal.Domain.Skills;
using Hackathon_CV_Portal.Domain.WorkignExperiences;
using System.ComponentModel.DataAnnotations;

namespace Hackathon_CV_Portal.Web.Models.CvModels
{
    public class CreateCvDTO
    {
        [Required]
        public string FirstName { get; private set; }
        [Required]
        public string LastName { get; private set; }

        [Required]
        public DateTime BirtDate { get; private set; }
        [Required, MaxLength(15)]
        public string PhoneNumber { get; private set; }

        [Required, MaxLength(50)]
        public string Email { get; private set; }

        [Required, MaxLength(100)]
        public string Address { get; private set; }

        [Required, MaxLength(250)]
        public string AboutMe { get; private set; }

        public List<Education> Educations { get; set; }
        public List<WorkingExperience> WorkingExperiences { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
