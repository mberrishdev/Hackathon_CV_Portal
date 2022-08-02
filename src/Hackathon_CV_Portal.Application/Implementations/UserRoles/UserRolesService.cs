using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Implementations.UserRoles.Models;
using Hackathon_CV_Portal.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Hackathon_CV_Portal.Application.Implementations.UserRoles
{
    public class UserRolesService : IUserRolesService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserRolesService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<List<ManageUserRolesModel>> GetManageUserRoles(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var userRoles = new List<string>(await _userManager.GetRolesAsync(user));


            var model = new List<ManageUserRolesModel>();
            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new ManageUserRolesModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (userRoles.Contains(role.ToString()))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                model.Add(userRolesViewModel);
            }

            return model;
        }

        public async Task UpdateUserRoleAsync(List<ManageUserRolesModel> model, int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return;

            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
        }
    }
}
