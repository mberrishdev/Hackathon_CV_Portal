using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Exceptions;
using Hackathon_CV_Portal.Application.Implementations.Qualifications.Models;
using Hackathon_CV_Portal.Application.Implementations.Responsibilities.Models;
using Hackathon_CV_Portal.Application.Implementations.Vacancies.Models;
using Hackathon_CV_Portal.Application.Implementations.Vacancies.Queries;
using Hackathon_CV_Portal.Data.Abstractions;
using Hackathon_CV_Portal.Domain.Enums;
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

        public async Task<int> CreateVacancy(CreateVacancyCommand command)
        {
            var id = await _baseRepository.CreateAsync(new Vacancy(command));

            return id;
        }

        public async Task<VacancyModel> GetVacancyById(int id)
        {
            var vm = await _baseRepository.GetAsync(includeProperties: new Expression<Func<Vacancy, object>>[4] { x => x.Category, x => x.Location,
                x=>x.Qualifications,x=>x.Responsibilities }, x => x.Id == id);

            if (vm == null)
                throw new NotFoundExcpetion();

            return new VacancyModel()
            {
                Id = vm.Id,
                Category = vm.Category.Name,
                CategoryId = vm.CategoryId,
                Title = vm.Title,
                Location = vm.Location.Country + ", " + vm.Location.City,
                LocationId = vm.LocationId,
                SalaryRange = vm.SalaryRange,
                Type = vm.Type.ToString(),
                CompanyName = vm.CompanyName,
                PublishDate = vm.PublishDate,
                DeadLine = vm.DeadLine,
                Description = vm.Description,
                Email = vm.Email,
                Responsibilities = vm.Responsibilities.Select(x => new ResponsibilityVM()
                {
                    Id = x.Id,
                    VacancyId = x.VacancyId,
                    ResponsibilityName = x.ResponsibilityName
                }).ToList(),
                Qualifications = vm.Qualifications.Select(x => new QualificationVM()
                {
                    Id = x.Id,
                    VacancyId = x.VacancyId,
                    QualificationName = x.QualificationName
                }).ToList(),
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
                //ToDo
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
                    //Responsibility = item.Responsibility,
                    //Qualifications = item.Qualifications,
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

        public async Task<AddRemoveVacancyStatus> AddOrRemoveFavourite(AddRemoveFavouriteCommand command)
        {
            var vacancy = await _baseRepository.GetAsync(predicate: x => x.Id == command.VacasnyId);
            if (vacancy == null)
                throw new Exception("ასეთი ვაკანსია არ არსებობს");
            return await _favouriteVacancyService.AddOrRemoveFavourite(command);
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

        public async Task UpdateVacancy(UpdateVacancyCommand command)
        {
            var vacancy = await _baseRepository.GetForUpdateAsync(command.Id);

            if (vacancy == null)
                throw new NotFoundExcpetion();

            if (vacancy.UserId != command.UserModel.UserId)
                throw new Exception();

            vacancy.Update(command);
            await _baseRepository.UpdateAsync(vacancy);
        }
    }
}
