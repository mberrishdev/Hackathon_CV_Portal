using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Implementations.Vacancies.Models;
using Hackathon_CV_Portal.Data.Abstractions;
using Hackathon_CV_Portal.Domain.Vacancies.Commands;
using Hackathon_CV_Portal.Domain.Vcancies;
using System.Linq.Expressions;

namespace Hackathon_CV_Portal.Application.Implementations.Vacancies
{
    public class VacancyService : IVacancyService
    {
        private readonly IBaseRepository<Vacancy> _baseRepository;

        public VacancyService(IBaseRepository<Vacancy> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task CreateVacancy(CreateVacancyCommand command)
        {
            await _baseRepository.CreateAsync(new Vacancy(command));
        }

        public async Task<VacansyVM> ListVacancyQuery(int page, Expression<Func<Vacancy, bool>>? expression = null)
        {
            var vacancies = await _baseRepository.GetAllAsyncByPage(page, expression: expression,
                includeProperties: new Expression<Func<Vacancy, object>>[1] { x => x.Category });

            if (vacancies == null)
                return null;

            return new VacansyVM()
            {
                VacancyModels = vacancies.Items.Select(x => new VacancyModel()
                {
                    Id = x.Id,
                    Category = x.Category.Name,
                    Title = x.Title,
                    Salary = x.Salary,
                    Currency = x.Currency,
                    PublishDate = x.PublishDate,
                    DeadLine = x.DeadLine,
                    Description = x.Description,
                }).ToList(),
                TottalPages = vacancies.TotalPages
            };
        }
    }
}
