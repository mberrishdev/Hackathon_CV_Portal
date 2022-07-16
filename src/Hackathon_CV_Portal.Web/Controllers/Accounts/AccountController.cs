using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Domain.Enums;
using Hackathon_CV_Portal.Domain.Users;
using Hackathon_CV_Portal.Domain.Users.Commands;
using Hackathon_CV_Portal.Web.Models.UserAccountModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon_CV_Portal.Web.Controllers.Accounts
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly ICvService _cvService;

        public AccountController(IAccountService accountService, SignInManager<ApplicationUser> signInManager, ICvService cvService) : base(signInManager)
        {
            _accountService = accountService;
            _cvService = _cvService;
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult LogIn(string returnAcction, string returnController, string routeId)
        {
            ViewBag.ReturnAcction = returnAcction;
            ViewBag.returnController = returnController;
            ViewBag.routeId = routeId;
            ModelState["returnAcction"].Errors.Clear();
            ModelState["returnController"].Errors.Clear();
            ModelState["routeId"].Errors.Clear();

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
                ConfirmPassword = model.ConfirmPassword
            };

            UserType userType = UserType.User;
            if (model.IsCompany)
                userType = UserType.Company;

            var result = await _accountService.RegisterAsync(createAppilicationUserCommand, userType);

            if (!result.Any())
                return RedirectToAction("Login");

            foreach (var error in result)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn([FromForm] LogInDto model, string returnAcction, string returnController, string routeId)
        {
            ModelState["returnAcction"].Errors.Clear();
            ModelState["returnController"].Errors.Clear();
            ModelState["routeId"].Errors.Clear();

            if (!ModelState.IsValid && !ModelState.Any(x => x.Key == "returnAcction") && !ModelState.Any(x => x.Key == "returnController")
                && !ModelState.Any(x => x.Key == "routeId"))
                return View();

            var command = new LogInUserCommand()
            {
                UserName = model.UserName,
                Password = model.Password,
                RememberMe = model.RememberMe
            };

            var status = await _accountService.LoginAsync(command, HttpContext);
            if (status == SignInStatus.Blocked)
            {
                ModelState.AddModelError("", "თქვენი ანგარიში დაბლოკილია, სცადეთ მოგვიანებით");
                return View();
            }
            if (status == SignInStatus.Success)
            {
                if (!string.IsNullOrEmpty(returnAcction) && !string.IsNullOrEmpty(returnController) && !string.IsNullOrEmpty(routeId))
                    return RedirectToAction(returnAcction, returnController, new { Id = int.Parse(routeId) });
                if (!string.IsNullOrEmpty(returnAcction) && !string.IsNullOrEmpty(returnController))
                    return RedirectToAction(returnAcction, returnController);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "მომხარებელი ან პაროლი არასწორია");


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
