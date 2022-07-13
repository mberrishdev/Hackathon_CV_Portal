namespace Hackathon_CV_Portal.Domain.Users.Commands
{
    public class CreateAppilicationUserCommand
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
