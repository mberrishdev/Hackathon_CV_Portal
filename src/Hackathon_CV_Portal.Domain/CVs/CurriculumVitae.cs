using Hackathon_CV_Portal.Domain.CVs.Commands;
using Hackathon_CV_Portal.Domain.Educations;
using Hackathon_CV_Portal.Domain.Skills;
using Hackathon_CV_Portal.Domain.Users;
using Hackathon_CV_Portal.Domain.WorkignExperiences;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hackathon_CV_Portal.Domain.CVs
{
    public class CurriculumVitae
    {
        [Key]
        public int Id { get; set; }

        [Required, Column(TypeName = "NVARCHAR"), MaxLength(50)]
        public string FirstName { get; set; }
        [Required, Column(TypeName = "NVARCHAR"), MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public DateTime BirtDate { get; set; }
        public int Age { get { return DateTime.Now.Subtract(BirtDate).Days / 365; } }
        [Required, MaxLength(15)]
        public string PhoneNumber { get; set; }
        [Required, MaxLength(50)]
        public string Email { get; set; }
        [Required, MaxLength(100)]
        public string Address { get; set; }
        [Required, Column(TypeName = "NVARCHAR"), MaxLength(500)]
        public string AboutMe { get; set; }
        [Required, Column(TypeName = "varbinary")]
        public string Image { get; set; }

        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<Education> Educations { get; set; }
        public ICollection<WorkingExperience> WorkingExperience { get; set; }
        public ICollection<Skill> Skills { get; set; }


        public CurriculumVitae()
        {
        }

        public CurriculumVitae(CreateCvCommand command)
        {
            FirstName = command.FirstName;
            LastName = command.LastName;
            PhoneNumber = command.PhoneNumber;
            BirtDate = command.BirtDate;
            Email = command.Email;
            Address = command.Address;
            AboutMe = command.AboutMe;
            Educations = command.Educations;
            WorkingExperience = command.WorkingExperiences;
            Skills = command.Skills;
            UserId = command.UserId;
        }
    }
}
