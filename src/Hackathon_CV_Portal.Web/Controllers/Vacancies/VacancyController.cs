using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Implementations.Vacancies.Queries;
using Hackathon_CV_Portal.Domain.Enums;
using Hackathon_CV_Portal.Domain.FavouriteVacancies.Commands;
using Hackathon_CV_Portal.Domain.Qualifications.Commands;
using Hackathon_CV_Portal.Domain.Responsibilities.Commands;
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
        private readonly IQualificationService _qualificationService;
        private readonly IResponsibilityService _responsibilityService;

        public VacancyController(IVacancyService vacancyService, SignInManager<ApplicationUser> signInManager,
            ICategoryService categoryService, ILocationService locationService, IQualificationService qualificationService,
            IResponsibilityService responsibilityService) : base(signInManager)
        {
            _vacancyService = vacancyService;
            _categoryService = categoryService;
            _locationService = locationService;
            _qualificationService = qualificationService;
            _responsibilityService = responsibilityService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchKeyword, int categoryId, int locationId, string vacancyType, bool isFav, bool withFiL = true, int page = 1, int companyId = 0)
        {
            Expression<Func<Vacancy, bool>>? expression = null;

            expression = x =>
                !string.IsNullOrEmpty(searchKeyword)
                        ? companyId == 0
                            ? x.CompanyName.Contains(searchKeyword) || x.Description.Contains(searchKeyword) || x.Responsibilities.Any(y => y.ResponsibilityName.Contains(searchKeyword)) || x.Qualifications.Any(y => y.QualificationName.Contains(searchKeyword))
                            : x.UserId == companyId && (x.CompanyName.Contains(searchKeyword) || x.Description.Contains(searchKeyword) || x.Responsibilities.Any(y => y.ResponsibilityName.Contains(searchKeyword)) || x.Qualifications.Any(y => y.QualificationName.Contains(searchKeyword)))
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
                    : true
                && x.DeadLine <= DateTime.Now;

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

        #region CRUD
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var locations = await _locationService.GetLocations();
            ViewBag.Locations = new SelectList(locations, "Id", "CountryCity");

            ViewBag.Types = new SelectList(new List<VacancyTypeClass> { new VacancyTypeClass() { Id = 1, Type = "სრული განაკვეთ" }, new VacancyTypeClass() { Id = 2, Type = "ნახევარი განაკვეთ" } }, "Id", "Type");

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> Create([FromForm] CreateVacancyDTO model)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetCategories();
                ViewBag.Categories = new SelectList(categories, "Id", "Name");

                var locations = await _locationService.GetLocations();
                ViewBag.Locations = new SelectList(locations, "Id", "CountryCity");

                ViewBag.Types = new SelectList(new List<VacancyTypeClass> { new VacancyTypeClass() { Id = 1, Type = "სრული განაკვეთ" }, new VacancyTypeClass() { Id = 2, Type = "ნახევარი განაკვეთ" } }, "Id", "Type");
                return View(model);
            }


            if (model.SalaryRange == null)
                model.SalaryRange = "შეთანხმებით";

            LoadUserModel();
            //ToDo
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
                Email = model.Email,
                //Responsibility = model.Responsibility,
                //Qualifications = model.Qualifications,
                UserModel = UserModel
            };

            var id = await _vacancyService.CreateVacancy(command);

            return RedirectToAction("AddVacancyDetail", new { vacancyId = id });
        }

        [Authorize(Roles = "Company, Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _vacancyService.Delete(id);
            return RedirectToAction("Index", "Vacancy");
        }

        [Authorize(Roles = "Company")]
        public async Task<IActionResult> Update(int id)
        {
            LoadUserModel();

            var vacancy = await _vacancyService.GetVacancyById(id);

            if (vacancy == null)
                return RedirectToAction("NotFound", "Home");

            if (vacancy.UserId != UserModel.UserId)
                return RedirectToAction("AccessDenied", "Account");

            var categories = await _categoryService.GetCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var locations = await _locationService.GetLocations();
            ViewBag.Locations = new SelectList(locations, "Id", "CountryCity");

            ViewBag.Types = new SelectList(new List<VacancyTypeClass> { new VacancyTypeClass() { Id = 1, Type = "სრული განაკვეთ" }, new VacancyTypeClass() { Id = 2, Type = "ნახევარი განაკვეთ" } }, "Id", "Type");

            return View(vacancy);
        }

        [HttpPost]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> Update([FromForm] UpdateVacancyDTO model)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetCategories();
                ViewBag.Categories = new SelectList(categories, "Id", "Name");

                var locations = await _locationService.GetLocations();
                ViewBag.Locations = new SelectList(locations, "Id", "City");

                ViewBag.Types = new SelectList(new List<VacancyTypeClass> { new VacancyTypeClass() { Id = 1, Type = "სრული განაკვეთ" }, new VacancyTypeClass() { Id = 2, Type = "ნახევარი განაკვეთ" } }, "Id", "Type");
                return View();
            }


            if (model.SalaryRange == null)
                model.SalaryRange = "შეთანხმებით";

            LoadUserModel();

            LoadUserModel();

            var command = new UpdateVacancyCommand()
            {
                Id = model.Id,
                CategoryId = model.CategoryId,
                Type = (VacancyType)int.Parse(model.Type),
                CompanyName = model.CompanyName,
                LocationId = model.LocationId,
                Title = model.Title,
                SalaryRange = model.SalaryRange,
                DeadLine = model.DeadLine,
                Email = model.Email,
                Description = model.Description,
                UserModel = UserModel
            };

            await _vacancyService.UpdateVacancy(command);

            return RedirectToAction("Update", new { id = command.Id });
        }


        #endregion

        #region Favourite

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

        #endregion

        #region DetailCRUD
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> AddVacancyDetail(int vacancyId)
        {
            var qualifications = await _qualificationService.GetByVacancyId(vacancyId);
            var responsibilities = await _responsibilityService.GetByVacancyId(vacancyId);
            var model = new AddVacancyDetailModel()
            {
                VacancyId = vacancyId,
                Qualifications = qualifications,
                Responsibilities = responsibilities,
            };

            return View(model);
        }

        [Authorize(Roles = "Company")]
        public async Task<IActionResult> UpdateVacancyDetail(int id)
        {
            var qualifications = await _qualificationService.GetByVacancyId(id);
            var responsibilities = await _responsibilityService.GetByVacancyId(id);
            var model = new UpdateVacancyDetailModel()
            {
                VacancyId = id,
                Qualifications = qualifications,
                Responsibilities = responsibilities,
            };

            return View(model);
        }

        [Authorize(Roles = "Company")]
        [HttpPost]
        public async Task<IActionResult> AddQualification(string responsibilityName, int vacancyId)
        {
            if (!ModelState.IsValid)
                return View();

            LoadUserModel();

            var command = new AddQualificationCommand()
            {
                QualificationName = responsibilityName,
                VacancyId = vacancyId,
                UserModel = UserModel,
            };

            await _qualificationService.AddQualification(command);


            return Json(new { redirectToUrl = Url.Action("AddVacancyDetail", "Vacancy", new { vacancyId = vacancyId }) });
        }

        [Authorize(Roles = "Company")]
        //[HttpPost]
        public async Task<IActionResult> DeleteQualification(int qualificationId, int vacancyId)
        {
            if (qualificationId <= 0 || vacancyId <= 0)
                return RedirectToAction("UpdateVacancyDetail", new { id = vacancyId });

            LoadUserModel();

            var command = new DeleteQualificationCommand()
            {
                QualificationId = qualificationId,
                VacancyId = vacancyId,
                UserModel = UserModel,
            };

            await _qualificationService.DeleteQualification(command);


            return RedirectToAction("UpdateVacancyDetail", new { id = vacancyId });
        }

        [Authorize(Roles = "Company")]
        [HttpPost]
        public async Task<IActionResult> AddResponsibility(string responsibilityName, int vacancyId)
        {
            if (!ModelState.IsValid)
                return View();

            LoadUserModel();

            var command = new AddResponsibilityCommand()
            {
                ResponsibilityName = responsibilityName,
                VacancyId = vacancyId,
                UserModel = UserModel,
            };

            await _responsibilityService.AddResponsibility(command);

            return Json(new { redirectToUrl = Url.Action("AddVacancyDetail", "Vacancy", new { vacancyId = vacancyId }) });
        }

        [Authorize(Roles = "Company")]
        //[HttpPost]
        public async Task<IActionResult> DeleteResponsibility(int responsibilityId, int vacancyId)
        {
            if (responsibilityId <= 0 || vacancyId <= 0)
                return RedirectToAction("UpdateVacancyDetail", new { id = vacancyId });

            LoadUserModel();

            var command = new DeleteResponsibilityCommand()
            {
                ResponsibilityId = responsibilityId,
                VacancyId = vacancyId,
                UserModel = UserModel,
            };

            await _responsibilityService.DeleteResponsibility(command);

            return RedirectToAction("UpdateVacancyDetail", new { id = vacancyId });
        }
        #endregion
    }
}
