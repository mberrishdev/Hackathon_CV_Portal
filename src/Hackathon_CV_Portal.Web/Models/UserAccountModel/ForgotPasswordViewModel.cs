using System.ComponentModel.DataAnnotations;

namespace Hackathon_CV_Portal.Web.Models.UserAccountModel
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
