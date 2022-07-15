using Microsoft.AspNetCore.Mvc;
using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Implementations.Cv.Models;
using Hackathon_CV_Portal.Web.Models.CvModels;
using Hackathon_CV_Portal.Domain.CVs.Commands;

namespace Hackathon_CV_Portal.Web.Controllers.CV
{
    public class CurriculumVitaeController : BaseController
    {
        private readonly ICvService _cvService;

        public CurriculumVitaeController(ICvService cvService)
        {
            _cvService = cvService;
        }

        public async Task<IActionResult> Index()
        {
            CvVM result = null;
            LodaUserModel();

            if (UserModel != null)
            {
                result = await _cvService.GetCV(UserModel.UserId);
            }
            if (result == null)
                return RedirectToAction("Index", "NotFound");

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] CreateCvDTO model)
        {
            if (!ModelState.IsValid)
                return View();

            LodaUserModel();

            var command = new CreateCvCommand()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirtDate = model.BirtDate,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                Address = model.Address,
                AboutMe = model.AboutMe,
                Educations = model.Educations,
                WorkingExperiences = model.WorkingExperiences,
                Skills = model.Skills,
                UserId = UserModel.UserId
            };

            await _cvService.CreateCv(command);

            return RedirectToAction("Index");
        }
    }
}
