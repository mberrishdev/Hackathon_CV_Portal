using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon_CV_Portal.Application.Implementations.Cv.Models
{
    internal class CvModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; } 
        public string Email { get; set; }   
        public string Address { get; set; }   
        public string AboutMe { get; set; }   

    }
}
