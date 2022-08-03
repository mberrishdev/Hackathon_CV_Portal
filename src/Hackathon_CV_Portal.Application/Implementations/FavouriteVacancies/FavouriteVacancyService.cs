using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Data.Abstractions;
using Hackathon_CV_Portal.Domain.FavouriteVacancies;
using Hackathon_CV_Portal.Domain.FavouriteVacancies.Commands;
using System.Linq.Expressions;

namespace Hackathon_CV_Portal.Application.Implementations.FavouriteVacancies
{
    public class FavouriteVacancyService : IFavouriteVacancyService
    {
        private readonly IBaseRepository<FavouriteVacancy> _baseRepository;

        public FavouriteVacancyService(IBaseRepository<FavouriteVacancy> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<bool> AnyAsync(Expression<Func<FavouriteVacancy, bool>> predicate)
        {
            return await _baseRepository.AnyAsync(predicate);
        }

        public async Task AddFavourite(AddFavouriteCommand command)
        {
            var favouriteVacacy = new FavouriteVacancy(command);
            await _baseRepository.CreateAsync(favouriteVacacy);
        }

        public async Task RemoveFavourite(RemoveFavouriteCommand command)
        {
            var fv = await _baseRepository.GetAsync(predicate: x => x.UserId == command.UserModel.UserId && x.VacansyId == command.VacasnyId);
            if (fv == null)
                throw new Exception("ვაკანსია არ არსებობს");
            await _baseRepository.RemoveAsync(fv.Id);
        }
    }
}
