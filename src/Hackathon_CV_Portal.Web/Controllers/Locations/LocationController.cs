using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Domain.Locations.Commands;
using Hackathon_CV_Portal.Domain.Users;
using Hackathon_CV_Portal.Web.Models.LocationModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon_CV_Portal.Web.Controllers.Locations
{
    public class LocationController : BaseController
    {
        private readonly ILocationService _locationService;

        public LocationController(SignInManager<ApplicationUser> signInManager, ILocationService locationService) : base(signInManager)
        {
            _locationService = locationService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var result = await _locationService.GetLocations();

            if (result == null)
                return RedirectToAction("NotFound", "Home");

            return View(result);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddLocation()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddLocation([FromForm] CreateLocationDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            LoadUserModel();

            var command = new CreateLocationCommand()
            {
                Country = model.Country,
                City = model.City
            };

            await _locationService.AddLocation(command);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            await _locationService.DeleteLocation(id);

            return RedirectToAction("Index");
        }
    }
}
