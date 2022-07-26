using Hackathon_CV_Portal.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Hackathon_CV_Portal.Application.Abstractions
{
    public interface IExternalLoginAuthInfoProvider
    {
        void PopulateUser(ExternalLoginInfo info, ApplicationUser user);
    }
}
