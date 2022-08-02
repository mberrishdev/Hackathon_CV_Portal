using System.ComponentModel.DataAnnotations;

namespace Hackathon_CV_Portal.Web.Models.Contact
{
    public class ContactUsDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string GoogleCaptchaToken { get; set; }
    }
}
