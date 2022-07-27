using Hackathon_CV_Portal.Application.Categories.Vacancies.Models;

namespace Hackathon_CV_Portal.Application.Implementations.Vacancies.Models
{
    public class VacansyVM
    {
        public List<VacancyModel> VacancyModels { get; set; }
        public int TottalPages { get; set; }
        public bool IsEmpty { get; set; }
        public bool WithFil { get; set; }
        public List<CategoryVM> Categories { get; set; }
    }
}
