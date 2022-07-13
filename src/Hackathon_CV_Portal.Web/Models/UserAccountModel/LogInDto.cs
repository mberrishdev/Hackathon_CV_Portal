using System.ComponentModel.DataAnnotations;

namespace Hackathon_CV_Portal.Web.Models.UserAccountModel
{
    public class LogInDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
