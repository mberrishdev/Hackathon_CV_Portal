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
        private readonly ICvService _cvService;
        private readonly ILogger<AccountService> _logger;
        private readonly IExternalLoginAuthInfoProvider _authInfoProvider;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IRoleService roleService,
            ICvService cvService,
            ILogger<AccountService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleService = roleService;
            _logger = logger;
            _cvService = cvService;
        }

        public async Task<SignInStatus> LoginAsync(LogInUserCommand command, HttpContext httpContext)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(command.UserName);

            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(command.UserName, command.Password, command.RememberMe, false);

                if (signInResult.IsLockedOut)
                    return SignInStatus.Blocked;

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

        public async Task<(SignInStatus Status, IdentityResult Result)> ExternalRegistration(ExternalCreateAppilicationUserCommand command)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                throw new ApplicationException("Error loading external login information during confirmation.");

            var user = new ApplicationUser
            {
                UserName = command.Email,
                Email = command.Email ?? info.Principal?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                CreateDate = DateTime.Now,
            };


            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, UserRole.User.ToString());
                result = await _userManager.AddLoginAsync(user, info);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

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


                    await command.HttpContext.SignInAsync(
                                        CookieAuthenticationDefaults.AuthenticationScheme,
                                        new ClaimsPrincipal(identity));

                    return (SignInStatus.Success, result);
                }


                _authInfoProvider.PopulateUser(info, user);
                return (SignInStatus.Failure, result);
            }

            return (SignInStatus.Failure, null);
        }
    }
}
