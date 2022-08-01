using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Exceptions;
using Hackathon_CV_Portal.Application.Implementations.Responsibilities.Models;
using Hackathon_CV_Portal.Data.Abstractions;
using Hackathon_CV_Portal.Domain.Responsibilities;
using Hackathon_CV_Portal.Domain.Responsibilities.Commands;

namespace Hackathon_CV_Portal.Application.Implementations.Responsibilities
{
    public class ResponsibilityService : IResponsibilityService
    {
        private readonly IBaseRepository<Responsibility> _baseRepository;
        private readonly IVacancyService _vacancyService;

        public ResponsibilityService(IBaseRepository<Responsibility> baseRepository, IVacancyService vacancyService)
        {
            _baseRepository = baseRepository;
            _vacancyService = vacancyService;
        }

        public async Task AddResponsibility(AddResponsibilityCommand command)
        {
            var vacancy = await _vacancyService.GetVacancyById(command.VacancyId);
            if (vacancy == null)
                throw new NotFoundExcpetion();

            if (vacancy.UserId != command.UserModel.UserId)
                throw new Exception();

            var responsibility = new Responsibility(command);

            await _baseRepository.CreateAsync(responsibility);
        }

        public async Task<List<ResponsibilityVM>> GetByVacancyId(int vacancyId)
        {
            var responsibility = await _baseRepository.GetListAsync(predicate: x => x.VacancyId == vacancyId);

            return responsibility.Select(x => new ResponsibilityVM()
            {
                Id = x.Id,
                ResponsibilityName = x.ResponsibilityName,
                VacancyId = x.VacancyId
            }).ToList();
        }
    }
}
