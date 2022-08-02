using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Implementations.AboutUs.Models;
using Hackathon_CV_Portal.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hackathon_CV_Portal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAboutService _aboutService;

        public HomeController(ILogger<HomeController> logger, IAboutService aboutService)
        {
            _aboutService = aboutService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var aboutUs = await _aboutService.GetAboutUs();

            AboutVM aboutUsVM = new AboutVM()
            {
                Content = aboutUs.Content
            };

            return View(aboutUsVM);
        }

        public IActionResult NotFound()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Success()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}