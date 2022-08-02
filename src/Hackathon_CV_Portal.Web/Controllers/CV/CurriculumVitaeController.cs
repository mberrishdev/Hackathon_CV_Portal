using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Implementations.Cv.Models;
using Hackathon_CV_Portal.Application.Implementations.Cv.Models.EducationVM;
using Hackathon_CV_Portal.Application.Implementations.Cv.Models.SkillsVM;
using Hackathon_CV_Portal.Application.Implementations.Cv.Models.WorkingExperienceVM;
using Hackathon_CV_Portal.Application.Implementations.Cv.Queries;
using Hackathon_CV_Portal.Domain.CVs.Commands;
using Hackathon_CV_Portal.Domain.Educations.Commands;
using Hackathon_CV_Portal.Domain.Skills.Commands;
using Hackathon_CV_Portal.Domain.Users;
using Hackathon_CV_Portal.Domain.WorkignExperiences.Commands;
using Hackathon_CV_Portal.Web.Models.CvModels;
using Hackathon_CV_Portal.Web.Models.EducationModel;
using Hackathon_CV_Portal.Web.Models.SkillsModel;
using Hackathon_CV_Portal.Web.Models.WorkingExperienceModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon_CV_Portal.Web.Controllers.CV
{
    [Authorize]
    public class CurriculumVitaeController : BaseController
    {
        private readonly ICvService _cvService;

        public CurriculumVitaeController(ICvService cvService, SignInManager<ApplicationUser> signInManager) : base(signInManager)
        {
            _cvService = cvService;
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Index(int cvId = 0)
        {
            CvVM result = null;

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
                return RedirectToAction("Create", "CurriculumVitae");

            return View(result);
        }


        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create()
        {
            CvVM tmpCvVM = new CvVM();

            return View(tmpCvVM);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create([FromForm] CreateCvDTO model)
        {
            if (!ModelState.IsValid)
                return View();

            LoadUserModel();

            var command = new CreateCvCommand()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirtDate = model.BirthDate,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                Address = model.Address,
                AboutMe = model.AboutMe,
                Image = model.Image ?? "",
                UserId = UserModel.UserId
            };

            await _cvService.CreateCv(command);

            return RedirectToAction("Index");
        }


        [Authorize(Roles = "User")]
        public async Task<IActionResult> Update()
        {
            CvVM result = null;
            LoadUserModel();

            if (UserModel != null)
            {
                result = await _cvService.GetCV(UserModel.UserId);
            }

            if (result == null)
                return RedirectToAction("Create", "CurriculumVitae");

            return View(result);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Update([FromForm] CreateCvDTO model)
        {
            

            if (!ModelState.IsValid)
                return View();


            if (model.Image != "" && model.Image != null)
            {
                try
                {
                    ValidateImage(model.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Validation", ex.Message);
                    return View();
                }
            }

            LoadUserModel();

            var command = new CreateCvCommand()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirtDate = model.BirthDate,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                Address = model.Address,
                AboutMe = model.AboutMe,
                Image = model.Image ?? "",
                UserId = UserModel.UserId
            };

            await _cvService.UpdateCv(command, UserModel.UserId);

            return RedirectToAction("Index");
        }

        // Skill
        [Authorize(Roles = "User")]
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

        [Authorize(Roles = "User")]
        public IActionResult AddSkill()
        {
            return View();
        }

        [Authorize(Roles = "User")]
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

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "User")]
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

            return RedirectToAction("Index");
        }

        // - Skill


        // Education
        [Authorize(Roles = "User")]
        public IActionResult AddEducation()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User")]
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

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "User")]
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

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "User")]
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

        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddWorkingExperience()
        {
            return View();
        }

        [Authorize(Roles = "User")]
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

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "User")]
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

        [Authorize(Roles = "User")]
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

            return RedirectToAction("Index");
        }

        // - Working Experience


        public bool ValidateImage(string image)
        {
            int maxFileLengthInBytes = 350000;

            if (!IsBase64String(image.Substring(image.IndexOf(',') + 1)))
                throw new ApplicationException("invalid file encoding");

            if (!IsPngOrJpg(image.Substring(0, image.IndexOf(';'))))
                throw new ApplicationException("არასწორი ფაილის ტიპი ატვირთეთ PNG ან JPG");

            if (!IsSmallerOrEqual(image.Substring(image.IndexOf(',') + 1), maxFileLengthInBytes))
                throw new ApplicationException("ფაილის ზომა 350kb ნაკლები უნდა იყოს ");

            return true;
        }

        public bool IsBase64String(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
        }

        public bool IsSmallerOrEqual(string base64, int lengthInBytes)
        {
            byte[] imageBytes = Convert.FromBase64String(base64);
            return imageBytes.Length <= lengthInBytes;
        }

        private bool IsPngOrJpg(string data)
        {
            return data.Contains("image/png") || data.Contains("image/png") || data.Contains("image/jpeg");
        }

    }
}
