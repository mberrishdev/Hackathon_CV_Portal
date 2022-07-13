using Hackathon_CV_Portal.Domain.Enums;
using Hackathon_CV_Portal.Domain.Users.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Hackathon_CV_Portal.Application.Abstractions
{
    public interface IAccountService
    {
        Task<IEnumerable<IdentityError>> RegisterAsync(CreateAppilicationUserCommand command, UserType userType);
        Task<SignInStatus> LoginAsync(LogInUserCommand command, HttpContext httpContext);
        Task LogOutAsync(HttpContext httpContext);
    }
}
