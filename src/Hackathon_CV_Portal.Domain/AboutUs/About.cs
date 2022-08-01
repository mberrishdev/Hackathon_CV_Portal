using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon_CV_Portal.Domain.AboutUs
{
    public class About
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
