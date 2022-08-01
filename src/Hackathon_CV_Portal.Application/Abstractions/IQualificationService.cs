using Hackathon_CV_Portal.Application.Implementations.Qualifications.Models;
using Hackathon_CV_Portal.Domain.Qualifications.Commands;

namespace Hackathon_CV_Portal.Application.Abstractions
{
    public interface IQualificationService
    {
        Task AddQualification(AddQualificationCommand command);
        Task<List<QualificationVM>> GetByVacancyId(int vacancyId);
    }
}
