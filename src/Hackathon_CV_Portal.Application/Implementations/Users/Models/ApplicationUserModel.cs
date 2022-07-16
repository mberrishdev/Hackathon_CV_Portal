namespace Hackathon_CV_Portal.Application.Implementations.Users.Models
{
    public class ApplicationUserModel
    {
        public int Id { get; internal set; }
        public string UserName { get; internal set; }
        public string Email { get; internal set; }
        public List<string> Roles { get; internal set; }
        public DateTime CreateDate { get; internal set; }
        public bool IsBlocked { get; internal set; }
    }
}
