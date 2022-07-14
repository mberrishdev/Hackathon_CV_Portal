using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Implementations.Vacancies.Models;
using Hackathon_CV_Portal.Data.Abstractions;
using Hackathon_CV_Portal.Domain.Vcancies;
using Mapster;

namespace Hackathon_CV_Portal.Application.Implementations.Vacancies
{
    public class VacancyService : IVacancyService
    {
        private readonly IBaseRepository<Vacancy> _baseRepository;

        public VacancyService(IBaseRepository<Vacancy> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<List<VacancyModel>> ListVacancyQuery(int page)
        {
            var vacancies = await _baseRepository.GetAllAsyncByPage(page);
            if (vacancies == null)
                return null;

            return vacancies.Items.Adapt<List<VacancyModel>>();
        }
    }
}
