using System.ComponentModel.DataAnnotations;

namespace Hackathon_CV_Portal.Web.Models.UserAccountModel
{
    public class LogInDto
    {
        [Required]
        [Display(Name = "მომხარებლის სახელი")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "პაროლო")]
        public string Password { get; set; }

        [Display(Name = "დავიმახსოვრო?")]
        public bool RememberMe { get; set; }
    }
}
