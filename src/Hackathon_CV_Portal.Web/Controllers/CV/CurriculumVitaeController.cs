using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Implementations.Cv.Models;
using Hackathon_CV_Portal.Domain.CVs.Commands;
using Hackathon_CV_Portal.Domain.Skills.Commands;
using Hackathon_CV_Portal.Domain.Users;
using Hackathon_CV_Portal.Web.Models.CvModels;
using Hackathon_CV_Portal.Web.Models.SkillsModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hackathon_CV_Portal.Web.Controllers.CV
{
    public class CurriculumVitaeController : BaseController
    {
        private readonly ICvService _cvService;

        public CurriculumVitaeController(ICvService cvService, SignInManager<ApplicationUser> signInManager) : base(signInManager)
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

        public async Task<IActionResult> Create()
        {
            CvVM result = null;
            LodaUserModel();

            if (UserModel != null)
            {
                result = await _cvService.GetCV(UserModel.UserId);
            }
            if (result == null)
                return RedirectToAction("Index", "NotFound");

            ViewBag.WorkingExperience = new SelectList(result.WorkingExperience, "Id", "Name");
            ViewBag.Education = new SelectList(result.Education, "Id", "Name");
            ViewBag.Skills = new SelectList(result.Skills, "Id", "Title");

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCvDTO model)
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

        public async Task<IActionResult> AddSkill()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSkill([FromForm] CreateSkillDTO model)
        {
            if (!ModelState.IsValid)
                return View();

            LodaUserModel();

            var command = new CreateSkillCommand()
            {
                Title = model.Title,
                UserId = UserModel.UserId,
            };
            await _cvService.AddSkillAsync(command);

            return RedirectToAction("Index");
        }

        //public async Task<IActionResult> AddEducation()
        //{

        //}

        //public async Task<IActionResult> AddWorkingExperience()
        //{

        //}
    }
}
