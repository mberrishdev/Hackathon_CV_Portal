using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Data.Abstractions;
using Hackathon_CV_Portal.Domain.AppliedCCVs;
using Hackathon_CV_Portal.Domain.AppliedCurriculumVitaes.Commands;

namespace Hackathon_CV_Portal.Application.Implementations.AppliedCurriculumVitaes
{
    public class AppliedCurriculumVitaeService : IAppliedCurriculumVitaeService
    {
        private readonly IBaseRepository<AppliedCurriculumVitae> _baseRepository;

        public AppliedCurriculumVitaeService(IBaseRepository<AppliedCurriculumVitae> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task ApplyVacancy(ApplyCurriculimVataeCommand command)
        {
            await _baseRepository.CreateAsync(new AppliedCurriculumVitae(command));
        }
    }
}
