using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon_CV_Portal.Application.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleService(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<List<ApplicationRole>> GetUserRoleAsync(int userId)
        {

            var roles = await _userManager.Users.Include(u => u.UserRoles)
                                          .ThenInclude(ur => ur.Role)
                                          .AsNoTracking()
                                          .FirstOrDefault(x => x.Id == userId).UserRoles
                                          .Select(x => x.Role)
                                          .AsQueryable()
                                          .ToListAsync();

            return roles;
        }
    }
}
