using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Implementations.Vacancies.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon_CV_Portal.Web.Controllers.Vacancies
{
    public class VacancyController : BaseController
    {
        private readonly IVacancyService _vacancyService;

        public VacancyController(IVacancyService vacancyService)
        {
            _vacancyService = vacancyService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            List<VacancyModel> result = await _vacancyService.ListVacancyQuery(page);

            if (result == null)
                return RedirectToAction("Index", "NotFound");

            return View(result);
        }

    }
}
