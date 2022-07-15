using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Implementations.Cv.Models;
using Hackathon_CV_Portal.Application.Implementations.Cv.Queries;
using Hackathon_CV_Portal.Domain.CVs.Commands;
using Hackathon_CV_Portal.Domain.Users;
using Hackathon_CV_Portal.Web.Models.CvModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon_CV_Portal.Web.Controllers.CV
{
    public class CurriculumVitaeController : BaseController
    {
        private readonly ICvService _cvService;

        public CurriculumVitaeController(ICvService cvService, SignInManager<ApplicationUser> signInManager) : base(signInManager)
        {
            _cvService = cvService;
        }

        public async Task<IActionResult> Index(int cvId = 0)
        {
            CvVM result = null;

            var returnAcction = "Index";
            var returnController = "CurriculumVitae";

            if (!IsSignedId())
                return RedirectToAction("LogIn", "Account", new { returnAcction, returnController });

            LoadUserModel();

            var query = new GetCVQuery()
            {
                UserModel = UserModel,
                CvId = cvId,
            };

            if (UserModel != null)
            {
                result = await _cvService.GetCV(query);
            }

            if (result == null)
                return RedirectToAction("NotFound", "Home");

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] CreateCvDTO model)
        {
            if (!ModelState.IsValid)
                return View();

            LoadUserModel();

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
