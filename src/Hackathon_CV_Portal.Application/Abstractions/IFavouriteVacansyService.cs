using Hackathon_CV_Portal.Domain.Enums;
using Hackathon_CV_Portal.Domain.FavouriteVacancies;
using Hackathon_CV_Portal.Domain.FavouriteVacancies.Commands;
using System.Linq.Expressions;

namespace Hackathon_CV_Portal.Application.Abstractions
{
    public interface IFavouriteVacancyService
    {
        Task<bool> AnyAsync(Expression<Func<FavouriteVacancy, bool>> predicate);
        Task<AddRemoveVacancyStatus> AddOrRemoveFavourite(AddRemoveFavouriteCommand command);
    }
}
