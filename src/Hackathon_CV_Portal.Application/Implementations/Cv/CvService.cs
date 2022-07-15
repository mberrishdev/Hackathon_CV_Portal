using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hackathon_CV_Portal.Domain.CVs;
using Hackathon_CV_Portal.Application.Implementations.Cv.Models;
using Hackathon_CV_Portal.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Hackathon_CV_Portal.Persistence.Context;
using Hackathon_CV_Portal.Domain.CVs.Commands;
using Hackathon_CV_Portal.Domain.CVs;
using Hackathon_CV_Portal.Data.Implementations;
using System.Linq.Expressions;
using Hackathon_CV_Portal.Domain.Educations.Commands;
using Hackathon_CV_Portal.Domain.Skills.Commands;
using Hackathon_CV_Portal.Domain.WorkignExperiences.Commands;

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

        public Task AddEducationAsync(CreateEducationCommand command)
        {
            throw new NotImplementedException();
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
        }

        public Task AddWorkingExperienceAsync(CreateWorkingExperienceCommand command)
        {
            throw new NotImplementedException();
        }

        public async Task CreateCv(CreateCvCommand command)
        {
            await _baseRepository.CreateAsync(new CurriculumVitae(command));
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
                    Skills = cv.Skills.ToList()
                };

                return cvVm;
            }

            return null;
        }
    }
}
