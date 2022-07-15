using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Implementations.Vacancies.Models;
using Hackathon_CV_Portal.Application.Implementations.Vacancies.Queries;
using Hackathon_CV_Portal.Data.Abstractions;
using Hackathon_CV_Portal.Domain.FavouriteVacancies.Commands;
using Hackathon_CV_Portal.Domain.Vacancies.Commands;
using Hackathon_CV_Portal.Domain.Vcancies;
using System.Linq.Expressions;

namespace Hackathon_CV_Portal.Application.Implementations.Vacancies
{
    public class VacancyService : IVacancyService
    {
        private readonly IBaseRepository<Vacancy> _baseRepository;
        private readonly IFavouriteVacancyService _favouriteVacancyService;

        public VacancyService(IBaseRepository<Vacancy> baseRepository, IFavouriteVacancyService favouriteVacancyService)
        {
            _baseRepository = baseRepository;
            _favouriteVacancyService = favouriteVacancyService;
        }

        public async Task CreateVacancy(CreateVacancyCommand command)
        {
            await _baseRepository.CreateAsync(new Vacancy(command));
        }

        public async Task<VacancyModel> GetVacancyById(int id)
        {
            var vm = await _baseRepository.GetAsync(new Expression<Func<Vacancy, object>>[1] { x => x.Category }, x => x.Id == id);

            return new VacancyModel()
            {
                Id = vm.Id,
                Category = vm.Category.Name,
                Title = vm.Title,
                Location = vm.Location,
                SalaryRange = vm.SalaryRange,
                Type = vm.Type.ToString(),
                CompanyName = vm.CompanyName,
                PublishDate = vm.PublishDate,
                DeadLine = vm.DeadLine,
                Description = vm.Description,
                Responsibility = vm.Responsibility,
                Qualifications = vm.Qualifications,
            };
        }

        public async Task<VacansyVM> ListVacancyQuery(ListVacancyQuery query)
        {
            var vacancies = await _baseRepository.GetAllAsyncByPage(query.Page, expression: query.Expression,
                includeProperties: new Expression<Func<Vacancy, object>>[1] { x => x.Category });

            if (vacancies == null)
                return null;

            var vacancyModels = new List<VacancyModel>();
            foreach (var item in vacancies.Items)
            {
                var isFavoutire = query.UserModel == null ? false : await _favouriteVacancyService.AnyAsync(predicate: x => x.VacansyId == item.Id && x.UserId == query.UserModel.UserId);

                vacancyModels.Add(new VacancyModel()
                {
                    Id = item.Id,
                    Category = item.Category.Name,
                    Title = item.Title,
                    SalaryRange = item.SalaryRange,
                    CompanyName = item.CompanyName,
                    Type = item.Type.ToString(),
                    PublishDate = item.PublishDate,
                    DeadLine = item.DeadLine,
                    Description = item.Description,
                    Responsibility = item.Responsibility,
                    Qualifications = item.Qualifications,
                    IsFavourite = isFavoutire
                });
            }
            return new VacansyVM()
            {
                VacancyModels = vacancyModels,
                TottalPages = vacancies.TotalPages
            };
        }

        public async Task AddFavourite(AddFavouriteCommand command)
        {
            await _favouriteVacancyService.AddFavourite(command);
        }

        public async Task RemoveFavourite(RemoveFavouriteCommand command)
        {
            await _favouriteVacancyService.RemoveFavourite(command);
        }
    }
}
