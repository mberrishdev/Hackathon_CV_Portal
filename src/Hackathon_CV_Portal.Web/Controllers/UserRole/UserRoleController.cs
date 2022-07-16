using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Implementations.UserRoles.Models;
using Hackathon_CV_Portal.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon_CV_Portal.Web.Controllers.UserRole
{
    public class UserRoleController : BaseController
    {
        private readonly IUserRolesService _userRolesService;
        public UserRoleController(SignInManager<ApplicationUser> signInManager, IUserRolesService userRolesService) : base(signInManager)
        {
            _userRolesService = userRolesService;
        }


        public async Task<IActionResult> Manage(int id)
        {
            var returnAcction = "Manage";
            var returnController = "UserRole";

            if (!IsSignedId())
                return RedirectToAction("LogIn", "Account", new { returnAcction, returnController, id });

            if (!IsInRole(Domain.Enums.UserRole.Admin.ToString()))
                return RedirectToAction("AccessDenied", "Account");

            LoadUserModel();

            ViewBag.userId = id.ToString();

            List<ManageUserRolesModel> mnanageUserRoles = await _userRolesService.GetManageUserRoles(id);

            ViewBag.UserId = id;

            return View(mnanageUserRoles);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageUserRolesModel> model, int id)
        {
            await _userRolesService.UpdateUserRoleAsync(model, id);
            return RedirectToAction("Index", "User");
        }
    }
}
