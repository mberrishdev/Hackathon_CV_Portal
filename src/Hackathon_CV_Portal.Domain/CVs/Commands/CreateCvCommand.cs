using Hackathon_CV_Portal.Domain.Educations;
using Hackathon_CV_Portal.Domain.Skills;
using Hackathon_CV_Portal.Domain.WorkignExperiences;

namespace Hackathon_CV_Portal.Domain.CVs.Commands
{
    public class CreateCvCommand : UserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirtDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string AboutMe { get; set; }
        public string Image { get; set; }

        public ICollection<Education> Educations { get; set; }
        public ICollection<WorkingExperience> WorkingExperiences { get; set; }
        public ICollection<Skill> Skills { get; set; }
    }
}
