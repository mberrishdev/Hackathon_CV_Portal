using Hackathon_CV_Portal.Domain.Educations;
using Hackathon_CV_Portal.Domain.Skills;
using Hackathon_CV_Portal.Domain.WorkignExperiences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon_CV_Portal.Application.Implementations.Cv.Models
{
    public class CvVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string AboutMe { get; set; }
        public List<WorkingExperience> WorkingExperience { get; set; }
        public List<Education> Education { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
