using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Implementations.Cv.Models;
using Hackathon_CV_Portal.Application.Implementations.Cv.Queries;
using Hackathon_CV_Portal.Data.Abstractions;
using Hackathon_CV_Portal.Domain.CVs;
using Hackathon_CV_Portal.Domain.CVs.Commands;
using Hackathon_CV_Portal.Domain.Users;
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

        public async Task CreateCv(CreateCvCommand command)
        {
            await _baseRepository.CreateAsync(new CurriculumVitae(command));
        }

        public async Task<CvVM> GetCV(GetCVQuery query)
        {
            CurriculumVitae cv;
            if (query.CvId != 0)
            {
                var myVacs = _context.Vacancies.Where(x => x.UserId == query.UserModel.UserId);

                bool exist = false;
                foreach (var myVac in myVacs)
                {
                    var ac = _context.AppliedCurriculumVitaes.FirstOrDefault(x => x.VacansyId == myVac.Id);
                    if (ac != null)
                        exist = _context.CVs.Any(x => x.UserId == ac.UserId && x.Id == query.CvId);

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
                    Skills = cv.Skills.ToList()
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
