using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Domain.Vcancies;
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

        public async Task<IActionResult> Index(int page = 1, string searchCategory = null)
        {
            Expression<Func<Vacancy, bool>>? expression = null;
            if (!String.IsNullOrEmpty(searchCategory))
            {
                expression = x => x.Category.Name.Contains(searchCategory);
            }

            var result = await _vacancyService.ListVacancyQuery(page, expression);

            if (result == null)
                return RedirectToAction("Index", "NotFound");

            return View(result);
        }

    }
}
