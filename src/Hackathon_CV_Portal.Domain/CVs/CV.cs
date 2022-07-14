using Hackathon_CV_Portal.Domain.Educations;
using Hackathon_CV_Portal.Domain.Skills;
using Hackathon_CV_Portal.Domain.Users;
using Hackathon_CV_Portal.Domain.WorkignExperiences;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hackathon_CV_Portal.Domain.CVs
{
    public class CV
    {
        [Key]
        public int Id { get; private set; }

        [Required, Column(TypeName = "NVARCHAR"), MaxLength(50)]
        public string FirstName { get; private set; }
        [Required, Column(TypeName = "NVARCHAR"), MaxLength(50)]
        public string LastName { get; private set; }
        [Required]
        public DateTime BirtDate { get; private set; }
        public int Age { get { return DateTime.Now.Subtract(BirtDate).Days / 365; } }
        [Required, MaxLength(15)]
        public string PhoneNumber { get; private set; }
        [Required, MaxLength(50)]
        public string Email { get; private set; }
        [Required, MaxLength(100)]
        public string Address { get; private set; }
        [Required, Column(TypeName = "NVARCHAR"), MaxLength(500)]
        public string AboutMe { get; private set; }
        [Required, Column(TypeName = "varbinary")]
        public string Image { get; private set; }

        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<Education> Educations { get; set; }
        public ICollection<WorkignExperience> WorkignExperiences { get; set; }
        public ICollection<Skill> Skills { get; set; }
    }
}
