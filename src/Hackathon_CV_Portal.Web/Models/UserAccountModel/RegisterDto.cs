using System.ComponentModel.DataAnnotations;

namespace Hackathon_CV_Portal.Web.Models.UserAccountModel
{
    public class RegisterDto
    {
        [Required]
        [Display(Name = "მომხარებლის სახელი")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "მაილი")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "პაროლი")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "გაიმეორეთ პაროლი")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "დავრეგისტრირდე როგორც კომპანია")]
        public bool IsCompany { get; set; }
    }
}
