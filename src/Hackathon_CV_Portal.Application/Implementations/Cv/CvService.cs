using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Implementations.Cv.Models;
using Hackathon_CV_Portal.Application.Implementations.Cv.Queries;
using Hackathon_CV_Portal.Data.Abstractions;
using Hackathon_CV_Portal.Domain.CVs;
using Hackathon_CV_Portal.Domain.CVs.Commands;
using Hackathon_CV_Portal.Domain.Educations.Commands;
using Hackathon_CV_Portal.Domain.Skills.Commands;
using Hackathon_CV_Portal.Domain.Users;
using Hackathon_CV_Portal.Domain.WorkignExperiences.Commands;
using Hackathon_CV_Portal.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace Hackathon_CV_Portal.Application.Implementations.Cv
{
    public class CvService : ICvService
    {
        private readonly IBaseRepository<CurriculumVitae> _baseRepository;
        private readonly CvPortalDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CvService(CvPortalDbContext context, UserManager<ApplicationUser> userManager, IBaseRepository<CurriculumVitae> baseRepository)
        {
            _context = context;
            _userManager = userManager;
            _baseRepository = baseRepository;
        }

        public async Task AddEducationAsync(CreateEducationCommand command)
        {
            var education = _context.CVs.Where(cv => cv.UserId == command.UserId).FirstOrDefault();

            if (education != null)
            {
                await _context.Educations.AddAsync(new Domain.Educations.Education()
                {
                    Name = command.Name,
                    StartDate = command.StartDate,
                    EndDate = command.EndDate,
                    Description = command.Description,
                    City = command.City,
                    University = command.University,
                    CV = education
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task AddSkillAsync(CreateSkillCommand command)
        {
            var cv = _context.CVs.Where(cv => cv.UserId == command.UserId).FirstOrDefault();

            if (cv != null)
            {
                await _context.Skills.AddAsync(new Domain.Skills.Skill()
                {
                    Title = command.Title,
                    CV = cv
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task AddWorkingExperienceAsync(CreateWorkingExperienceCommand command)
        {
            var workingExperience = _context.CVs.Where(cv => cv.UserId == command.UserId).FirstOrDefault();

            if (workingExperience != null)
            {
                await _context.WorkignExperiences.AddAsync(new Domain.WorkignExperiences.WorkingExperience()
                {
                    Name = command.Name,
                    StartDate = command.StartDate,
                    EndDate = command.EndDate,
                    Description = command.Description,
                    City = command.City,
                    Company = command.Company,
                    CV = workingExperience
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task CreateCv(CreateCvCommand command)
        {
            await _baseRepository.CreateAsync(new CurriculumVitae(command));
        }

        public async Task UpdateCv(CreateCvCommand command, int userId)
        {
            var cv = _context.CVs.Where(cv => cv.UserId == userId).FirstOrDefault();

            if (cv != null)
            {
                cv.FirstName = command.FirstName;
                cv.LastName = command.LastName;
                cv.PhoneNumber = command.PhoneNumber;
                cv.Address = command.Address;
                cv.BirtDate = command.BirtDate;
                cv.Email = command.Email;
                cv.AboutMe = command.AboutMe;
                cv.Image = command.Image ?? "";
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteEducation(int id)
        {
            var education = _context.Educations.FirstOrDefault(x => x.Id == id);

            if (education != null)
            {
                _context.Remove(education);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteSkill(int id)
        {
            var skill = _context.Skills.FirstOrDefault(x => x.Id == id);

            if (skill != null)
            {
                _context.Remove(skill);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteWorkingExperience(int id)
        {
            var workingExperience = _context.WorkignExperiences.FirstOrDefault(x => x.Id == id);

            if (workingExperience != null)
            {
                _context.Remove(workingExperience);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<CvVM> GetCV(GetCVQuery query)
        {
            CurriculumVitae cv;
            if (query.CvId != 0)
            {
                var myVacs = _context.Vacancies.Where(x => x.UserId == query.UserModel.UserId).ToList();
                var ac = _context.AppliedCurriculumVitaes.Where(x => x.CurriculumVitaeId == query.CvId).ToList();

                bool exist = false;
                foreach (var myVac in myVacs)
                {
                    exist = ac.Any(x => x.VacancyId == myVac.Id && x.CurriculumVitaeId == query.CvId);
                    if (exist)
                        break;
                }

                if (!exist)
                    return null;

                cv = await _baseRepository.GetAsync(
                        new Expression<Func<CurriculumVitae, object>>[3] { x => x.WorkingExperience, x => x.Educations, x => x.Skills },
                        x => x.Id == query.CvId);
            }
            else
            {
                cv = await _baseRepository.GetAsync(
                    new Expression<Func<CurriculumVitae, object>>[3] { x => x.WorkingExperience, x => x.Educations, x => x.Skills },
                    x => x.UserId == query.UserModel.UserId);
            }

            if (cv != null)
            {
                var cvVm = new CvVM()
                {
                    Id = cv.Id,
                    FirstName = cv.FirstName,
                    LastName = cv.LastName,
                    BirthDate = cv.BirtDate,
                    Age = cv.Age,
                    PhoneNumber = cv.PhoneNumber,
                    Email = cv.Email,
                    Address = cv.Address,
                    AboutMe = cv.AboutMe,
                    WorkingExperience = cv.WorkingExperience.ToList(),
                    Education = cv.Educations.ToList(),
                    Skills = cv.Skills.ToList(),
                    UserId = cv.UserId,
                    Image = cv.Image,
                };

                return cvVm;
            }

            return null;
        }

        public async Task<CvVM> GetCV(int userId)
        {
            var cv = await _baseRepository.GetAsync(
                new Expression<Func<CurriculumVitae, object>>[3] { x => x.WorkingExperience, x => x.Educations, x => x.Skills },
                x => x.UserId == userId);

            if (cv != null)
            {
                var cvVm = new CvVM()
                {
                    FirstName = cv.FirstName,
                    LastName = cv.LastName,
                    BirthDate = cv.BirtDate,
                    Age = cv.Age,
                    PhoneNumber = cv.PhoneNumber,
                    Email = cv.Email,
                    Address = cv.Address,
                    AboutMe = cv.AboutMe,
                    WorkingExperience = cv.WorkingExperience.ToList(),
                    Education = cv.Educations.ToList(),
                    Skills = cv.Skills.ToList(),
                    UserId = cv.UserId,
                    Image = cv.Image
                };

                return cvVm;
            }

            return null;
        }

        public async Task<CvModel> GetCVById(int cvId)
        {
            var cv = await _baseRepository.GetAsync(predicate: x => x.Id == cvId);

            if (cv == null)
                return null;

            var cvModel = new CvModel()
            {
                Id = cvId,
                FirstName = cv.FirstName,
                LastName = cv.LastName,
                BirthDate = cv.BirtDate,
                Age = cv.Age,
                PhoneNumber = cv.PhoneNumber,
                Email = cv.Email,
                Address = cv.Address,
                AboutMe = cv.AboutMe,

            };

            return cvModel;

        }
    }
}
