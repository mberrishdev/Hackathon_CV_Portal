using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Implementations.AboutUs.Models;
using Hackathon_CV_Portal.Domain.AboutUs.Commands;
using Hackathon_CV_Portal.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon_CV_Portal.Web.Controllers
{
    [Authorize]
    public class AboutController : BaseController
    {
        private readonly IAboutService _aboutService;

        public AboutController(SignInManager<ApplicationUser> signInManager, IAboutService aboutService) : base(signInManager)
        {
            _aboutService = aboutService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var aboutUs = await _aboutService.GetAboutUs();

            AboutVM aboutUsVM = new AboutVM()
            {
                Content = aboutUs.Content
            };

            return View(aboutUsVM);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit()
        {
            var aboutUs = await _aboutService.GetAboutUs();

            AboutVM aboutUsVM = new AboutVM()
            {
                Content = aboutUs.Content
            };

            return View(aboutUsVM);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string Content)
        {
            CreateAboutUsCommand command = new CreateAboutUsCommand()
            {
                Content = Content
            };

            await _aboutService.UpdateAboutUs(command);

            return Json(new { redirectToUrl = Url.Action("Index", "About") });
        }
    }
}
