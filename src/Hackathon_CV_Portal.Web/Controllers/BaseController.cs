using Hackathon_CV_Portal.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hackathon_CV_Portal.Web.Controllers
{
    public class BaseController : Controller
    {
        public UserModel UserModel { get; set; }

        public BaseController()
        {
            if (User != null)
            {
                UserModel = new()
                {
                    UserName = User.FindFirstValue(ClaimTypes.Name),
                    UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                };
            }
        }

        public void LodaUserModel()
        {
            UserModel = new()
            {
                UserName = User.FindFirstValue(ClaimTypes.Name),
                UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
            };
        }

    }
}


