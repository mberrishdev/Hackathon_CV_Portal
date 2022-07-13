namespace Hackathon_CV_Portal.Domain.Users.Commands
{
    public class LogInUserCommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
