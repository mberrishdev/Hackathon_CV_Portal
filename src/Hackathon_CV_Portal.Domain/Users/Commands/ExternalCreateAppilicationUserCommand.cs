using Microsoft.AspNetCore.Http;

namespace Hackathon_CV_Portal.Domain.Users.Commands
{
    public class ExternalCreateAppilicationUserCommand
    {
        public string Email { get; set; }
        public HttpContext HttpContext { get; set; }
    }
}
