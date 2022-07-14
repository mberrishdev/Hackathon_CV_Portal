using Hackathon_CV_Portal.Domain.CVs;
using System.ComponentModel.DataAnnotations;

namespace Hackathon_CV_Portal.Domain.WorkignExperiences
{
    public class WorkignExperience
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [Required, MaxLength(200)]
        public string Description { get; set; }
        [Required, MaxLength(50)]
        public string Company { get; set; }
        [Required, MaxLength(50)]
        public string City { get; set; }

        public CV CV { get; set; }
    }
}
