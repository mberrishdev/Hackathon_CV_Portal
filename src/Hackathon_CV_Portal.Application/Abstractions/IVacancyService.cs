using Hackathon_CV_Portal.Application.Implementations.Vacancies.Models;

namespace Hackathon_CV_Portal.Application.Abstractions
{
    public interface IVacancyService
    {
        Task<List<VacancyModel>> ListVacancyQuery(int page);
    }
}
