using Microsoft.AspNetCore.Identity;

namespace Hackathon_CV_Portal.Domain.Users
{
    public class ApplicationUser : IdentityUser<int>
    {

        public int CvId { get; set; }
        public int VacansyId { get; set; }
    }


}
