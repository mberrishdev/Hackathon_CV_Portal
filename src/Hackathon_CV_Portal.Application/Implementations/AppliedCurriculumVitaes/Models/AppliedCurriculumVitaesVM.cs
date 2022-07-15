using Hackathon_CV_Portal.Application.Implementations.Cv.Models;

namespace Hackathon_CV_Portal.Application.Implementations.AppliedCurriculumVitaes.Models
{
    public class AppliedCurriculumVitaesVM
    {
        public List<CvModel> CurriculumVitaes { get; set; }
        public int TottalCurriculumVitaes { get; set; }
        public bool IsEmpty { get; internal set; }
    }
}
