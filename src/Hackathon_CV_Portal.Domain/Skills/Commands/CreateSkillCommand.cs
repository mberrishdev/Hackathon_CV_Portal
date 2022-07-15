using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon_CV_Portal.Domain.Skills.Commands
{
    public class CreateSkillCommand
    {
        public string Title { get; set; }
        public int UserId { get; set; }
    }
}
