using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Implementations.Cv.Models;
using Hackathon_CV_Portal.Application.Implementations.Cv.Models.EducationVM;
using Hackathon_CV_Portal.Application.Implementations.Cv.Models.SkillsVM;
using Hackathon_CV_Portal.Application.Implementations.Cv.Models.WorkingExperienceVM;
using Hackathon_CV_Portal.Domain.CVs.Commands;
using Hackathon_CV_Portal.Domain.Educations.Commands;
using Hackathon_CV_Portal.Domain.Skills.Commands;
using Hackathon_CV_Portal.Domain.Users;
using Hackathon_CV_Portal.Domain.WorkignExperiences.Commands;
using Hackathon_CV_Portal.Web.Models.CvModels;
using Hackathon_CV_Portal.Web.Models.EducationModel;
using Hackathon_CV_Portal.Web.Models.SkillsModel;
using Hackathon_CV_Portal.Web.Models.WorkingExperienceModel;
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

        public async Task<IActionResult> Create()
        {
            CvVM result = null;
            LoadUserModel();

            if (UserModel != null)
            {
                result = await _cvService.GetCV(UserModel.UserId);
            }
            if (result == null)
                return RedirectToAction("Index", "NotFound");

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCvDTO model)
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

        // Skill

        public IActionResult Skill()
        {
            LoadUserModel();

            var skillModels = _cvService.GetCV(UserModel.UserId).Result.Skills;

            SkillVM skillVm = new SkillVM()
            {
                SkillModels = skillModels.Select(n => new SkillModel()
                {
                    Id = n.Id,
                    Title = n.Title,
                }).ToList()
            };

            return View(skillVm);
        }
    

        public IActionResult AddSkill()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSkill([FromForm] CreateSkillDTO model)
        {
            if (!ModelState.IsValid)
                return View();

            LoadUserModel();

            var command = new CreateSkillCommand()
            {
                Title = model.Title,
                UserId = UserModel.UserId,
            };
            await _cvService.AddSkillAsync(command);

            return RedirectToAction("Skill");
        }

        public async Task<IActionResult> DeleteSkill(int id)
        {
            LoadUserModel();

            var cv = await _cvService.GetCV(UserModel.UserId);

            if (cv != null)
            {
                if (cv.UserId == UserModel.UserId)
                {
                    await _cvService.DeleteSkill(id);
                }
            }

            return RedirectToAction("Skill");
        }

        // - Skill
       

        // Education
        public IActionResult AddEducation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEducation([FromForm] CreateEducationDto model)
        {

            if (!ModelState.IsValid)
                return View();

            LoadUserModel();

            var command = new CreateEducationCommand()
            {
                Name = model.Name,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                University = model.University,
                City = model.City,
                UserId = UserModel.UserId,
            };
            await _cvService.AddEducationAsync(command);

            return RedirectToAction("Education");
        }

        public async Task<IActionResult> DeleteEducation(int id)
        {
            LoadUserModel();

            var cv = await _cvService.GetCV(UserModel.UserId);

            if (cv != null)
            {
                if (cv.UserId == UserModel.UserId)
                {
                    await _cvService.DeleteEducation(id);
                }
            }

            return RedirectToAction("Education");
        }

        public async Task<IActionResult> Education()
        {
            LoadUserModel();

            var educationModels = _cvService.GetCV(UserModel.UserId).Result.Education;

            EducationVm educationVm = new EducationVm()
            {
                EducationModelModels = educationModels.Select(n => new EducationModel()
                {
                    Id = n.Id,
                    Name = n.Name,
                    City = n.City,
                    University = n.University,
                    Description = n.Description,
                    StartDate = n.StartDate,
                    EndDate = n.EndDate == null ? null : n.EndDate.Value
                }).ToList()
            };

            return View(educationVm);
        }

        // - Education


        // Working Experience

        public async Task<IActionResult> AddWorkingExperience()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkingExperience([FromForm] CreateWorkingExperienceDto model)
        {
                if (!ModelState.IsValid)
                return RedirectToAction("AddWorkingExperience");

            LoadUserModel();

            var command = new CreateWorkingExperienceCommand()
            {
                Name = model.Name,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Company = model.Company,
                City = model.City,
                UserId = UserModel.UserId,
            };
            await _cvService.AddWorkingExperienceAsync(command);

            return RedirectToAction("WorkingExperience");
        }

        public async Task<IActionResult> WorkingExperience()
        {
            LoadUserModel();

            var workingExperiences = _cvService.GetCV(UserModel.UserId).Result.WorkingExperience;

            WorkingExperienceVM workingExperienceVM = new WorkingExperienceVM()
            {
                WorkingExperienceModels = workingExperiences.Select(n => new WorkingExperienceModel()
                {
                    Id = n.Id,
                    Name = n.Name,
                    City = n.City,
                    Company = n.Company,
                    Description = n.Description,
                    StartDate = n.StartDate,
                    EndDate = n.EndDate == null ? null : n.EndDate.Value
                }).ToList()
            };

            return View(workingExperienceVM);
        }

        public async Task<IActionResult> DeleteWorkingExperience(int id)
        {
            LoadUserModel();

            var cv = await _cvService.GetCV(UserModel.UserId);

            if (cv != null)
            {
                if (cv.UserId == UserModel.UserId)
                {
                    await _cvService.DeleteWorkingExperience(id);
                }
            }

            return RedirectToAction("WorkingExperience");
        }

        // - Working Experience


    }
}
