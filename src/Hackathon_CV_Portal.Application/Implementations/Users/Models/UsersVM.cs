namespace Hackathon_CV_Portal.Application.Implementations.Users.Models
{
    public class UsersVM
    {
        public List<ApplicationUserModel> UserModels { get; set; }
        public int UserCount { get; set; }
        public int ActiveUsers { get; set; }
    }
}
