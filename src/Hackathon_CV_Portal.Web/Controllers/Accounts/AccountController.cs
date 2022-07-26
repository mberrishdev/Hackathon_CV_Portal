using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Domain.Enums;
using Hackathon_CV_Portal.Domain.Users;
using Hackathon_CV_Portal.Domain.Users.Commands;
using Hackathon_CV_Portal.Web.Infrastracture.Extensions;
using Hackathon_CV_Portal.Web.Models.UserAccountModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hackathon_CV_Portal.Web.Controllers.Accounts
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRoleService _roleService;
        private readonly IEmailSender _emailSender;
        private readonly IAccountService _accountService;
        private readonly ICvService _cvService;

        public AccountController(IAccountService accountService,
            SignInManager<ApplicationUser> signInManager,
            ICvService cvService,
            UserManager<ApplicationUser> userManager,
            IRoleService roleService,
            IEmailSender emailSender) : base(signInManager)
        {
            _accountService = accountService;
            _userManager = userManager;
            _cvService = cvService;
            _emailSender = emailSender;
            _roleService = roleService;
            _emailSender = emailSender;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        public IActionResult AccessDenied()
        {
            return View();
        }

        #region LogIn
        [HttpGet]
        [AllowAnonymous]
        public IActionResult LogIn(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn([FromForm] LogInDto model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            var command = new LogInUserCommand()
            {
                UserName = model.UserName,
                Password = model.Password,
                RememberMe = model.RememberMe
            };

            var status = await _accountService.LoginAsync(command, HttpContext);
            if (status == SignInStatus.Blocked)
            {
                return RedirectToAction(nameof(Lockout));
            }
            if (status == SignInStatus.Success)
            {
                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError("", "მომხარებელი ან პაროლი არასწორია");


            return View();
        }

        #endregion

        #region ExternalLogIn
        [HttpPost]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string? returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string? returnUrl = null, string? remoteError = null)
        {
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToAction(nameof(LogIn));
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(LogIn));
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["LoginProvider"] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLogin", new ExternalLoginViewModel { Email = email });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            var command = new ExternalCreateAppilicationUserCommand()
            {
                Email = model.Email,
                HttpContext = HttpContext
            };

            var (Status, Result) = await _accountService.ExternalRegistration(command);

            if (Status == SignInStatus.Success)
                return RedirectToLocal(returnUrl);
            else if (Status == SignInStatus.Failure)
                AddErrors(Result);

            ViewData["ReturnUrl"] = returnUrl;
            return View(nameof(ExternalLogin), model);
        }
        #endregion

        #region Register

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] RegisterDto model, string? returnUrl = null)
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

        #endregion

        public async Task<IActionResult> LogOut(string? returnUrl = null)
        {
            await _accountService.LogOutAsync(HttpContext);
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
            await _emailSender.SendEmailAsync(model.Email, "პაროლის აღდგენა",
               $"დააჭირეთ პაროლის აღსადგენად: <a href='{callbackUrl}'>ლინკი</a>");

            return RedirectToAction(nameof(ForgotPasswordConfirmation));

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string? code = null)
        {
            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }

            var model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            AddErrors(result);
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        #region Helpers
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        #endregion
    }
}
