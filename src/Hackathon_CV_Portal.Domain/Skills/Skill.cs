using Hackathon_CV_Portal.Domain.CVs;
using System.ComponentModel.DataAnnotations;

namespace Hackathon_CV_Portal.Domain.Skills
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Title { get; set; }

        public CurriculumVitae CV { get; set; }
    }
}
