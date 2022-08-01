using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Exceptions;
using Hackathon_CV_Portal.Application.Implementations.Qualifications.Models;
using Hackathon_CV_Portal.Data.Abstractions;
using Hackathon_CV_Portal.Domain.Qualifications;
using Hackathon_CV_Portal.Domain.Qualifications.Commands;

namespace Hackathon_CV_Portal.Application.Implementations.Qualifications
{
    public class QualificationService : IQualificationService
    {
        private readonly IBaseRepository<Qualification> _baseRepository;
        private readonly IVacancyService _vacancyService;

        public QualificationService(IBaseRepository<Qualification> baseRepository, IVacancyService vacancyService)
        {
            _baseRepository = baseRepository;
            _vacancyService = vacancyService;
        }

        public async Task AddQualification(AddQualificationCommand command)
        {
            var vacancy = await _vacancyService.GetVacancyById(command.VacancyId);
            if (vacancy == null)
                throw new NotFoundExcpetion();

            if (vacancy.UserId != command.UserModel.UserId)
                throw new Exception();

            var qualification = new Qualification(command);

            await _baseRepository.CreateAsync(qualification);
        }

        public async Task<List<QualificationVM>> GetByVacancyId(int vacancyId)
        {
            var qualifications = await _baseRepository.GetListAsync(predicate: x => x.VacancyId == vacancyId);

            return qualifications.Select(x => new QualificationVM()
            {
                Id = x.Id,
                QualificationName = x.QualificationName,
                VacancyId = x.VacancyId
            }).ToList();
        }
    }
}
