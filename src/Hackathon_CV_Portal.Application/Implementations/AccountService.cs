using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Domain.Enums;
using Hackathon_CV_Portal.Domain.Users;
using Hackathon_CV_Portal.Domain.Users.Commands;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Hackathon_CV_Portal.Application.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRoleService _roleService;
        private readonly ILogger<AccountService> _logger;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IRoleService roleService,
            ILogger<AccountService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleService = roleService;
            _logger = logger;
        }

        public async Task<SignInStatus> LoginAsync(LogInUserCommand command, HttpContext httpContext)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(command.UserName);

            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(command.UserName, command.Password, command.RememberMe, false);

                if (signInResult.Succeeded)
                {
                    var userRoles = await _roleService.GetUserRoleAsync(user.Id);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    };

                    foreach (var userRole in userRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await httpContext.SignInAsync(
                                             CookieAuthenticationDefaults.AuthenticationScheme,
                                             new ClaimsPrincipal(identity),
                                             new AuthenticationProperties
                                             {
                                                 IsPersistent = command.RememberMe
                                             });

                    return SignInStatus.Success;
                }
            }

            return SignInStatus.Failure;
        }

        public async Task<IEnumerable<IdentityError>> RegisterAsync(CreateAppilicationUserCommand command, UserType userType)
        {
            var user = new ApplicationUser(command);

            string role;
            if (userType == UserType.User)
                role = UserRole.User.ToString();
            else
                role = UserRole.Company.ToString();

            var result = await _userManager.CreateAsync(user, command.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                await _userManager.AddToRoleAsync(user, role);
            }

            return result.Errors;
        }

        public async Task LogOutAsync(HttpContext httpContext)
        {
            await _signInManager.SignOutAsync();

            await httpContext.SignOutAsync(
                 CookieAuthenticationDefaults.AuthenticationScheme);

            _logger.LogInformation("User logged out.");
        }
    }
}
