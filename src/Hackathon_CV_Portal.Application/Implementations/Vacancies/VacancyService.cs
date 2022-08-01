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
        private readonly ICvService _cvService;

        public VacancyService(IBaseRepository<Vacancy> baseRepository, IFavouriteVacancyService favouriteVacancyService,
            ICvService cvService)
        {
            _baseRepository = baseRepository;
            _favouriteVacancyService = favouriteVacancyService;
            _cvService = cvService;
        }

        public async Task CreateVacancy(CreateVacancyCommand command)
        {
            await _baseRepository.CreateAsync(new Vacancy(command));
        }

        public async Task<VacancyModel> GetVacancyById(int id)
        {
            var vm = await _baseRepository.GetAsync(new Expression<Func<Vacancy, object>>[2] { x => x.Category, x => x.Location }, x => x.Id == id);

            return new VacancyModel()
            {
                Id = vm.Id,
                Category = vm.Category.Name,
                Title = vm.Title,
                Location = vm.Location.Country + ", " + vm.Location.City,
                SalaryRange = vm.SalaryRange,
                Type = vm.Type.ToString(),
                CompanyName = vm.CompanyName,
                PublishDate = vm.PublishDate,
                DeadLine = vm.DeadLine,
                Description = vm.Description,
                Responsibility = vm.Responsibility,
                Qualifications = vm.Qualifications,
                UserId = vm.UserId,
            };
        }

        public async Task<VacansyVM> ListVacancyQuery(ListVacancyQuery query)
        {
            var vacancies = await _baseRepository.GetAllAsyncByPage(query.Page, expression: query.Expression,
                includeProperties: new Expression<Func<Vacancy, object>>[2] { x => x.Category, x => x.Location });

            if (vacancies == null)
                return null;

            var vacancyModels = new List<VacancyModel>();
            foreach (var item in vacancies.Items)
            {
                var isFavoutire = query.UserModel == null ? false : await _favouriteVacancyService.AnyAsync(predicate: x => x.VacansyId == item.Id && x.UserId == query.UserModel.UserId);
                if (query.WithFav && !isFavoutire)
                    continue;

                vacancyModels.Add(new VacancyModel()
                {
                    Id = item.Id,
                    Category = item.Category.Name,
                    Title = item.Title,
                    SalaryRange = item.SalaryRange,
                    CompanyName = item.CompanyName,
                    Location = item.Location.Country + ", " + item.Location.City,
                    Type = item.Type.ToString(),
                    PublishDate = item.PublishDate,
                    DeadLine = item.DeadLine,
                    Description = item.Description,
                    Responsibility = item.Responsibility,
                    Qualifications = item.Qualifications,
                    IsFavourite = isFavoutire,
                    UserId = item.UserId,
                });
            }

            return new VacansyVM()
            {
                VacancyModels = vacancyModels,
                TottalPages = vacancies.TotalPages,
                IsEmpty = vacancies.IsEmpty,
                CurrentPageIndex = query.Page
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

        public async Task Delete(int id)
        {
            var entity = await _baseRepository.GetAsync(predicate: x => x.Id == id);
            await _baseRepository.RemoveAsync(entity);
        }

        public async Task CleanVacancies()
        {
            var list = await _baseRepository.GetListAsync(x => DateTime.Now > x.DeadLine);
            foreach (var item in list)
            {
                await _baseRepository.RemoveAsync(item);
            }
        }
    }
}
