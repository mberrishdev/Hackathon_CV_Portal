using Hackathon_CV_Portal.Application.Implementations.Vacancies.Models;
using Hackathon_CV_Portal.Domain.Vcancies;
using System.Linq.Expressions;

namespace Hackathon_CV_Portal.Application.Abstractions
{
    public interface IVacancyService
    {
        Task<VacansyVM> ListVacancyQuery(int page, Expression<Func<Vacancy, bool>>? expression = null);
    }
}
