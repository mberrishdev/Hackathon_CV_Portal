using Hackathon_CV_Portal.Domain.AppliedCurriculumVitaes.Commands;

namespace Hackathon_CV_Portal.Application.Abstractions
{
    public interface IAppliedCurriculumVitaeService
    {
        Task ApplyVacancy(ApplyCurriculimVataeCommand command);
    }
}
