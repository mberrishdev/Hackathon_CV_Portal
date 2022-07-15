using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon_CV_Portal.Application.Implementations.Cv.Models.WorkingExperienceVM
{
    public class WorkingExperienceModel
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public string City { get; set; }
    }
}
