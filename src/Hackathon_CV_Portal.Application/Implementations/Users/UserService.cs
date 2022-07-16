using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Implementations.Users.Models;
using Hackathon_CV_Portal.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Hackathon_CV_Portal.Application.Implementations.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UsersVM> ListUsers(string userName)
        {
            var users = _userManager.Users.ToList();

            if (!string.IsNullOrEmpty(userName))
                users = users.Where(x => x.UserName.Contains(userName)).ToList();

            var userModel = new List<ApplicationUserModel>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userModel.Add(new ApplicationUserModel()
                {
                    Id = user.Id,
                    IsBlocked = !user.LockoutEnabled,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList(),
                    CreateDate = user.CreateDate,
                });
            }
            return new UsersVM()
            {
                UserModels = userModel,
                UserCount = userModel.Count,
                ActiveUsers = 0
            };

        }

        public async Task BlockUser(int id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
                return;

            await _userManager.SetLockoutEnabledAsync(user, false);
        }

        public async Task UnBlockUser(int id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
                return;

            await _userManager.SetLockoutEnabledAsync(user, true);
        }
    }
}
