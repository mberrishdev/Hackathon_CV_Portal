using Hackathon_CV_Portal.Domain;
using Hackathon_CV_Portal.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hackathon_CV_Portal.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly SignInManager<ApplicationUser> _signInManager;
        public UserModel UserModel { get; set; }

        public BaseController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;

            if (User == null || User.FindFirstValue(ClaimTypes.Name) == null || User.FindFirstValue(ClaimTypes.NameIdentifier) == null)
            {
                UserModel = null;
            }
            else
            {
                UserModel = new()
                {
                    UserName = User.FindFirstValue(ClaimTypes.Name),
                    UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                };
            }
        }

        protected void LoadUserModel()
        {
            if (User == null || User.FindFirstValue(ClaimTypes.Name) == null || User.FindFirstValue(ClaimTypes.NameIdentifier) == null)
            {
                UserModel = null;
            }
            else
            {
                UserModel = new()
                {
                    UserName = User.FindFirstValue(ClaimTypes.Name),
                    UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                };
            }

        }

        protected bool IsSignedId()
        {
            return _signInManager.IsSignedIn(User);
        }
    }
}


