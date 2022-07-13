using Microsoft.AspNetCore.Identity;

namespace Hackathon_CV_Portal.Domain.Users
{
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
