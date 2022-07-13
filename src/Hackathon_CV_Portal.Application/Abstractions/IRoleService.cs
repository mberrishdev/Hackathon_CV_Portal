using Hackathon_CV_Portal.Domain.Users;

namespace Hackathon_CV_Portal.Application.Abstractions
{
    public interface IRoleService
    {
        public Task<List<ApplicationRole>> GetUserRoleAsync(int userId);
    }
}
