using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Exceptions;
using Hackathon_CV_Portal.Application.Implementations.AppliedCurriculumVitaes.Models;
using Hackathon_CV_Portal.Application.Implementations.AppliedCurriculumVitaes.Queries;
using Hackathon_CV_Portal.Application.Implementations.Cv.Models;
using Hackathon_CV_Portal.Application.Implementations.Cv.Queries;
using Hackathon_CV_Portal.Data.Abstractions;
using Hackathon_CV_Portal.Domain;
using Hackathon_CV_Portal.Domain.AppliedCCVs;
using Hackathon_CV_Portal.Domain.AppliedCurriculumVitaes.Commands;

namespace Hackathon_CV_Portal.Application.Implementations.AppliedCurriculumVitaes
{
    public class AppliedCurriculumVitaeService : IAppliedCurriculumVitaeService
    {
        private readonly IBaseRepository<AppliedCurriculumVitae> _baseRepository;
        private readonly IVacancyService _vacancyService;
        private readonly ICvService _cvService;

        public AppliedCurriculumVitaeService(IBaseRepository<AppliedCurriculumVitae> baseRepository,
            IVacancyService vacancyService, ICvService cvService)
        {
            _baseRepository = baseRepository;
            _vacancyService = vacancyService;
            _cvService = cvService;
        }

        public async Task ApplyVacancy(int vacansyId, UserModel userModel)
        {

            var userCv = await _cvService.GetCV(new GetCVQuery() { UserModel = userModel });
            if (userCv == null)
                throw new Exception("მომხარებელს არ აქვს cv ატვირთული");

            var command = new ApplyCurriculimVataeCommand()
            {
                VacansyId = vacansyId,
                CurriculimVataeId = userCv.Id
            };

            await _baseRepository.CreateAsync(new AppliedCurriculumVitae(command));
        }

        public async Task<AppliedCurriculumVitaesVM> GetCurriculumVitaeByVacancyId(GetCurriculumVitaeByVacancyIdQuery query)
        {
            var vacanncy = await _vacancyService.GetVacancyById(query.VacancyId);
            if (vacanncy == null)
                throw new NotFoundExcpetion();

            if (vacanncy.UserId != query.UserModel.UserId)
                throw new AccessDeniedException();

            var appliedCurriculumVitaes = await _baseRepository.GetListAsync(x => x.VacancyId == query.VacancyId);

            var result = new AppliedCurriculumVitaesVM
            {
                CurriculumVitaes = new List<CvModel>()
            };


            foreach (var appliedCurriculumVitae in appliedCurriculumVitaes)
            {
                var cvModel = await _cvService.GetCVById(appliedCurriculumVitae.CurriculumVitaeId);
                result.CurriculumVitaes.Add(cvModel);
            }

            result.TottalCurriculumVitaes = result.CurriculumVitaes.Count;
            result.IsEmpty = !result.CurriculumVitaes.Any();
            return result;
        }
    }
}
