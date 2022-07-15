using System.ComponentModel.DataAnnotations;

namespace Hackathon_CV_Portal.Web.Models.SkillsModel
{
    public class CreateSkillDTO
    {
        [Required]
        public string Title { get; set; }
    }
}
