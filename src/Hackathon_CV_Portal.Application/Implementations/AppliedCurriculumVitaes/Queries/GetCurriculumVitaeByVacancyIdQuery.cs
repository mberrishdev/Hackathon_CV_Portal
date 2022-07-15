using Hackathon_CV_Portal.Domain;

namespace Hackathon_CV_Portal.Application.Implementations.AppliedCurriculumVitaes.Queries
{
    public class GetCurriculumVitaeByVacancyIdQuery
    {
        public UserModel UserModel { get; set; }
        public int VacancyId { get; set; }
    }
}
