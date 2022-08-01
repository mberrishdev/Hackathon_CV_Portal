using Hackathon_CV_Portal.Application.Implementations.Qualifications.Models;
using Hackathon_CV_Portal.Application.Implementations.Responsibilities.Models;

namespace Hackathon_CV_Portal.Web.Models.VacancyModels
{
    public class AddVacancyDetailModel
    {
        public int VacancyId { get; set; }
        public List<ResponsibilityVM> Responsibilities { get; set; }
        public List<QualificationVM> Qualifications { get; set; }
    }
}
