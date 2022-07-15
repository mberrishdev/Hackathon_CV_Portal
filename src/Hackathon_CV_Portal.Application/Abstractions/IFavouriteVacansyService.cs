using Hackathon_CV_Portal.Domain.FavouriteVacancies;
using Hackathon_CV_Portal.Domain.FavouriteVacancies.Commands;
using System.Linq.Expressions;

namespace Hackathon_CV_Portal.Application.Abstractions
{
    public interface IFavouriteVacancyService
    {
        Task AddFavourite(AddFavouriteCommand command);
        Task RemoveFavourite(RemoveFavouriteCommand command);
        Task<bool> AnyAsync(Expression<Func<FavouriteVacancy, bool>> predicate);
    }
}
