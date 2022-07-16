using Hackathon_CV_Portal.Application.Implementations.UserRoles.Models;

namespace Hackathon_CV_Portal.Application.Abstractions
{
    public interface IUserRolesService
    {
        Task<List<ManageUserRolesModel>> GetManageUserRoles(int id);
        Task UpdateUserRoleAsync(List<ManageUserRolesModel> model, int id);
    }
}
