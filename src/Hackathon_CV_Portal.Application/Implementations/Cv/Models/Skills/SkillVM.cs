using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon_CV_Portal.Application.Implementations.Cv.Models.Skills
{
    public class SkillVM
    {
        [Display(Name = "შეიყვანეს უნარის სახელწოდება")]
        public string Title { get; set; }
    }
}
