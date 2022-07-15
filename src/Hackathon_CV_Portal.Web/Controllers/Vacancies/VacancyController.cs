using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Domain.Enums;
using Hackathon_CV_Portal.Domain.Vacancies.Commands;
using Hackathon_CV_Portal.Domain.Vcancies;
using Hackathon_CV_Portal.Web.Models.VacancyModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Hackathon_CV_Portal.Web.Controllers.Vacancies
{
    public class VacancyController : BaseController
    {
        private readonly IVacancyService _vacancyService;

        public VacancyController(IVacancyService vacancyService)
        {
            _vacancyService = vacancyService;
        }

        public async Task<IActionResult> Index(string searchCategory, string vacancyType, int page = 1)
        {
            Expression<Func<Vacancy, bool>>? expression = null;
            if (!String.IsNullOrEmpty(searchCategory))
            {
                expression = x => x.Category.Name.Contains(searchCategory);
            }

            if (!String.IsNullOrEmpty(vacancyType))
            {
                expression = x => x.Type == Enum.Parse<VacancyType>(vacancyType);
            }

            var result = await _vacancyService.ListVacancyQuery(page, expression);

            if (result == null)
                return RedirectToAction("Index", "NotFound");

            return View(result);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _vacancyService.GetVacancyById(id);
            return View(result);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] CreateVacancyDTO model)
        {
            if (!ModelState.IsValid)
                return View();

            LodaUserModel();

            var command = new CreateVacancyCommand()
            {
                Title = model.Title,
                Description = model.Description,
                DeadLine = model.DeadLine,
                CategoryId = model.CategoryId,
                Currency = model.Currency,
                //Salary = model.Salary,
                UserId = UserModel.UserId,
                UserName = UserModel.UserName
            };

            await _vacancyService.CreateVacancy(command);

            return RedirectToAction("Index");
        }
    }
}
