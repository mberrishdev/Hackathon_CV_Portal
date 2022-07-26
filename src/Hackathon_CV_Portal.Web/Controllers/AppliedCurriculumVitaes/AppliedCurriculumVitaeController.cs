using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Exceptions;
using Hackathon_CV_Portal.Application.Implementations.AppliedCurriculumVitaes.Models;
using Hackathon_CV_Portal.Application.Implementations.AppliedCurriculumVitaes.Queries;
using Hackathon_CV_Portal.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon_CV_Portal.Web.Controllers.AppliedCurriculumVitaes
{
    [Authorize]
    public class AppliedCurriculumVitaeController : BaseController
    {
        private readonly IAppliedCurriculumVitaeService _appliedCurriculumVitaeService;

        public AppliedCurriculumVitaeController(IAppliedCurriculumVitaeService appliedCurriculumVitaeService, SignInManager<ApplicationUser> signInManager) : base(signInManager)
        {
            _appliedCurriculumVitaeService = appliedCurriculumVitaeService;
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Index(int vacancyId = 0)
        {
            if (vacancyId == 0)
                return RedirectToAction("Index", "NotFound");

            LoadUserModel();

            var query = new GetCurriculumVitaeByVacancyIdQuery()
            {
                VacancyId = vacancyId,
                UserModel = UserModel,
            };

            AppliedCurriculumVitaesVM result = null;
            try
            {
                result = await _appliedCurriculumVitaeService.GetCurriculumVitaeByVacancyId(new GetCurriculumVitaeByVacancyIdQuery()
                {
                    VacancyId = vacancyId,
                    UserModel = UserModel,
                });
            }
            catch (NotFoundExcpetion)
            {
                return RedirectToAction("Index", "NotFound");
            }
            catch (AccessDeniedException)
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            return View(result);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Apply(int id)
        {
            LoadUserModel();

            await _appliedCurriculumVitaeService.ApplyVacancy(id, UserModel);
            return View();
        }

    }
}
