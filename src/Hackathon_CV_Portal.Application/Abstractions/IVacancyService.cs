using Hackathon_CV_Portal.Application.Implementations.Vacancies.Models;
using Hackathon_CV_Portal.Application.Implementations.Vacancies.Queries;
using Hackathon_CV_Portal.Domain.FavouriteVacancies.Commands;
using Hackathon_CV_Portal.Domain.Vacancies.Commands;

namespace Hackathon_CV_Portal.Application.Abstractions
{
    public interface IVacancyService
    {
        Task<VacansyVM> ListVacancyQuery(ListVacancyQuery query);
        Task CreateVacancy(CreateVacancyCommand command);
        Task<VacancyModel> GetVacancyById(int id);
        Task AddFavourite(AddFavouriteCommand command);
        Task RemoveFavourite(RemoveFavouriteCommand command);
    }
}
