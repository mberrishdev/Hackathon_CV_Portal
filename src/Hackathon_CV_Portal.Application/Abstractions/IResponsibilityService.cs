using Hackathon_CV_Portal.Application.Implementations.Responsibilities.Models;
using Hackathon_CV_Portal.Domain.Responsibilities.Commands;

namespace Hackathon_CV_Portal.Application.Abstractions
{
    public interface IResponsibilityService
    {
        Task AddResponsibility(AddResponsibilityCommand command);
        Task<List<ResponsibilityVM>> GetByVacancyId(int vacancyId);
        Task DeleteResponsibility(DeleteResponsibilityCommand command);
    }
}
