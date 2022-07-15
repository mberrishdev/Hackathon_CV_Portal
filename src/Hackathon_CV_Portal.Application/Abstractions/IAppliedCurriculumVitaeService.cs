using Hackathon_CV_Portal.Application.Implementations.AppliedCurriculumVitaes.Models;
using Hackathon_CV_Portal.Application.Implementations.AppliedCurriculumVitaes.Queries;
using Hackathon_CV_Portal.Domain;

namespace Hackathon_CV_Portal.Application.Abstractions
{
    public interface IAppliedCurriculumVitaeService
    {
        Task ApplyVacancy(int vacansyId, UserModel userModel);
        Task<AppliedCurriculumVitaesVM> GetCurriculumVitaeByVacancyId(GetCurriculumVitaeByVacancyIdQuery query);
    }
}
