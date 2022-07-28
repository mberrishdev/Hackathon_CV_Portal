using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Implementations.Vacancies.Queries;
using Hackathon_CV_Portal.Domain.Enums;
using Hackathon_CV_Portal.Domain.FavouriteVacancies.Commands;
using Hackathon_CV_Portal.Domain.Users;
using Hackathon_CV_Portal.Domain.Vacancies.Commands;
using Hackathon_CV_Portal.Domain.Vcancies;
using Hackathon_CV_Portal.Web.Models.VacancyModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace Hackathon_CV_Portal.Web.Controllers.Vacancies
{
    [Authorize]
    public class VacancyController : BaseController
    {
        private readonly IVacancyService _vacancyService;
        private readonly ICategoryService _categoryService;
        private readonly ILocationService _locationService;

        public VacancyController(IVacancyService vacancyService, SignInManager<ApplicationUser> signInManager,
            ICategoryService categoryService, ILocationService locationService) : base(signInManager)
        {
            _vacancyService = vacancyService;
            _categoryService = categoryService;
            _locationService = locationService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchKeyword, int categoryId, int locationId, string vacancyType, bool isFav, bool withFiL = true, int page = 1, int companyId = 0)
        {
            Expression<Func<Vacancy, bool>>? expression = null;

            expression = x =>
                !string.IsNullOrEmpty(searchKeyword)
                        ? companyId == 0
                            ? x.CompanyName.Contains(searchKeyword) || x.Description.Contains(searchKeyword) || x.Responsibility.Contains(searchKeyword) || x.Qualifications.Contains(searchKeyword)
                            : x.UserId == companyId && (x.CompanyName.Contains(searchKeyword) || x.Description.Contains(searchKeyword) || x.Responsibility.Contains(searchKeyword) || x.Qualifications.Contains(searchKeyword))
                        : true
                && categoryId > 0
                        ? companyId == 0
                            ? x.Category.Id == categoryId
                            : x.Category.Id == categoryId && x.UserId == companyId
                        : true
                && locationId > 0
                        ? locationId == 0
                            ? x.Location.Id == locationId
                            : x.Location.Id == locationId && x.UserId == companyId
                        : true
                && !String.IsNullOrEmpty(vacancyType)
                    ? companyId == 0
                        ? x.Type == Enum.Parse<VacancyType>(vacancyType)
                        : x.Category.Id == categoryId && x.UserId == companyId
                    : true
                && categoryId <= 0 && String.IsNullOrEmpty(vacancyType) && companyId != 0
                    ? x.UserId == companyId
                    : true;

            LoadUserModel();

            if (UserModel?.UserId != companyId && companyId != 0)
                return RedirectToAction("AccessDenied", "Account");

            var query = new ListVacancyQuery()
            {
                Page = page,
                Expression = expression,
                UserModel = UserModel,
                WithFav = isFav,
            };

            var result = await _vacancyService.ListVacancyQuery(query);

            if (result == null)
                return RedirectToAction("Index", "NotFound");

            result.WithFil = withFiL;

            var categories = await _categoryService.GetCategories();
            result.Categories = categories;

            var locations = await _locationService.GetLocations();
            result.Locations = locations;

            return View(result);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _vacancyService.GetVacancyById(id);
            if (result == null)
                return RedirectToAction("NotFound", "Home");

            return View(result);
        }


        [Authorize(Roles = "Company, Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _vacancyService.Delete(id);
            return RedirectToAction("Index", "Vacancy");
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddFavourite(int id)
        {
            LoadUserModel();
            var command = new AddFavouriteCommand()
            {
                VacasnyId = id,
                UserModel = UserModel
            };

            await _vacancyService.AddFavourite(command);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> RemoveFavourite(int id)
        {
            LoadUserModel();
            var command = new RemoveFavouriteCommand()
            {
                VacasnyId = id,
                UserModel = UserModel
            };

            await _vacancyService.RemoveFavourite(command);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Company")]
        public async Task<IActionResult> Add()
        {
            var categories = await _categoryService.GetCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var locations = await _locationService.GetLocations();
            ViewBag.Locations = new SelectList(locations, "Id", "City");

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> Add([FromForm] CreateVacancyDTO model)
        {
            var returnAcction = "Add";
            var returnController = "Vacancy";

            if (!IsSignedId())
                return RedirectToAction("LogIn", "Account", new { returnAcction, returnController });

            if (!IsInRole(Hackathon_CV_Portal.Domain.Enums.UserRole.Company.ToString()))
                return RedirectToAction("AccessDenied", "Account");

            if (!ModelState.IsValid)
                return View();

            LoadUserModel();

            var command = new CreateVacancyCommand()
            {
                CategoryId = model.CategoryId,
                Type = (VacancyType)model.Type,
                CompanyName = model.CompanyName,
                LocationId = model.LocationId,
                Title = model.Title,
                SalaryRange = model.SalaryRange,
                DeadLine = model.DeadLine,
                Description = model.Description,
                Responsibility = model.Responsibility,
                Qualifications = model.Qualifications,
                UserModel = UserModel
            };

            await _vacancyService.CreateVacancy(command);

            return RedirectToAction("Index");
        }
    }
}
