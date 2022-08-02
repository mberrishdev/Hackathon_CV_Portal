using Hackathon_CV_Portal.Domain.Educations;
using Hackathon_CV_Portal.Domain.Skills;
using Hackathon_CV_Portal.Domain.WorkignExperiences;
using System.ComponentModel.DataAnnotations;

namespace Hackathon_CV_Portal.Web.Models.CvModels
{
    public class CreateCvDTO
    {
        [Required (ErrorMessage = "სახელი აუცილებელია")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "გვარი აუცილებელია")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "დაბადების თარიღი აუცილებელია")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "მობილურის ნომერი აუცილებელია"), MaxLength(15) ]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "მეილი აუცილებელია"), MaxLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "მისამართი აუცილებელია"), MaxLength(100)]
        public string Address { get; set; }

        [Required(ErrorMessage = "ინფორმაცია შენს შესახებ აუცილებელია"), MaxLength(250)]
        public string AboutMe { get; set; }

        public string? Image { get; set; }

    }
}
