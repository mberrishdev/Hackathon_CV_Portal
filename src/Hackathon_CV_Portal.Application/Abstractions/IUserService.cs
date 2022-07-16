using Hackathon_CV_Portal.Application.Implementations.Users.Models;

namespace Hackathon_CV_Portal.Application.Abstractions
{
    public interface IUserService
    {
        Task<UsersVM> ListUsers(string userName);
        Task BlockUser(int id);
        Task UnBlockUser(int id);
    }
}
