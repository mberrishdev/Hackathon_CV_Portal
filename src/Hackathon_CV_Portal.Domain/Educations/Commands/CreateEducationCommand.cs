using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon_CV_Portal.Domain.Educations.Commands
{
    public class CreateEducationCommand
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string University { get; set; }
        public string City { get; set; }
        public int UserId { get; set; }
    }
}
