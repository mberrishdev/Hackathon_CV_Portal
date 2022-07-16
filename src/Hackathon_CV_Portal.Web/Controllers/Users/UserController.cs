using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon_CV_Portal.Web.Controllers.Users
{
    //[Authorize(Roles = "Admin")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(SignInManager<ApplicationUser> signInManager, IUserService userService) : base(signInManager)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index(string userName)
        {
            var users = await _userService.ListUsers(userName);

            if (users == null)
                return RedirectToAction("NotFound", "Home");

            return View(users);
        }

        public async Task<IActionResult> Block(int id)
        {
            await _userService.BlockUser(id);

            return Redirect("Index");
        }

        public async Task<IActionResult> UnBlock(int id)
        {
            await _userService.UnBlockUser(id);

            return Redirect("Index");
        }


    }
}
