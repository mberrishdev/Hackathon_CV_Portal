using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Domain.Enums;
using Hackathon_CV_Portal.Domain.Users.Commands;
using Hackathon_CV_Portal.Web.Models.UserAccountModel;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon_CV_Portal.Web.Controllers.Accounts
{
    public class UserAccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public UserAccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterDto model)
        {
            if (!ModelState.IsValid)
                return View();

            var createAppilicationUserCommand = new CreateAppilicationUserCommand()
            {
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password,
                ConfirmPassword = model.Password
            };

            var result = await _accountService.RegisterAsync(createAppilicationUserCommand, UserType.User);

            if (!result.Any())
                return RedirectToAction("Login");

            foreach (var error in result)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn([FromForm] LogInDto model)
        {

            if (!ModelState.IsValid)
                return View();

            var command = new LogInUserCommand()
            {
                UserName = model.UserName,
                Password = model.Password,
                RememberMe = model.RememberMe
            };

            var status = await _accountService.LoginAsync(command, HttpContext);
            if (status == SignInStatus.Success)
                return RedirectToAction("", "");


            ModelState.AddModelError("", "Username or password is incorrect");

            return View();
        }

        public async Task<IActionResult> LogOut(string returnUrl = null)
        {
            await _accountService.LogOutAsync(HttpContext);
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("LogOut");
            }
        }
    }
}
