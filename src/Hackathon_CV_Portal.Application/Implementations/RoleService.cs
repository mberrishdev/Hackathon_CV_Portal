using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Domain.Users;
using Hackathon_CV_Portal.Persistence.Context;
using Microsoft.AspNetCore.Identity;

namespace Hackathon_CV_Portal.Application.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CvPortalDbContext _context;

        public RoleService(UserManager<ApplicationUser> userManager, CvPortalDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public Task<List<string>> GetUserRoleAsync(int userId)
        {
            var userRoles = _context.UserRoles.Where(x => x.UserId == userId).ToList();

            var result = new List<string>();

            foreach (var userRole in userRoles)
            {
                var role = _context.Roles.FirstOrDefault(x => x.Id == userRole.RoleId);
                result.Add(role.Name);
            }

            return Task.FromResult(result);
        }
    }
}
