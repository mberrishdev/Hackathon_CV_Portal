using Hackathon_CV_Portal.Domain;
using Hackathon_CV_Portal.Domain.Vcancies;
using System.Linq.Expressions;

namespace Hackathon_CV_Portal.Application.Implementations.Vacancies.Queries
{
    public class ListVacancyQuery
    {
        public int Page { get; set; }
        public Expression<Func<Vacancy, bool>>? Expression { get; set; }
        public UserModel? UserModel { get; set; }
        public int CompanyId { get; set; }
        public bool WithFav { get; set; }
    }
}
