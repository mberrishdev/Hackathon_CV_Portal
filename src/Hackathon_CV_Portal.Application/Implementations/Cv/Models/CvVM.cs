using Hackathon_CV_Portal.Domain.Educations;
using Hackathon_CV_Portal.Domain.Skills;
using Hackathon_CV_Portal.Domain.WorkignExperiences;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon_CV_Portal.Application.Implementations.Cv.Models
{
    public class CvVM
    {
        public int Id { get; set; }
        [Display(Name = "სახელი")]
        public string FirstName { get; set; }
        [Display(Name = "გვარი")]
        public string LastName { get; set; }
        [Display(Name = "დაბადების თარიღი")]
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        [Display(Name = "მობილურის ნომერი")]
        public string PhoneNumber { get; set; }
        [Display(Name = "საკონტაქტო მეილი")]
        public string Email { get; set; }
        [Display(Name = "მისამართი")]
        public string Address { get; set; }
        [Display(Name = "ზოგადი ინფორმაცია თქვენს შესახებ")]
        public string AboutMe { get; set; }
        [Display(Name = "სამუშაო გამოცდილება")]
        public List<WorkingExperience> WorkingExperience { get; set; }
        [Display(Name = "განათლება")]
        public List<Education> Education { get; set; }
        [Display(Name = "უნარები")]
        public List<Skill> Skills { get; set; }
        public string Image { get; set; }
        public int UserId { get; set; }
    }
}
