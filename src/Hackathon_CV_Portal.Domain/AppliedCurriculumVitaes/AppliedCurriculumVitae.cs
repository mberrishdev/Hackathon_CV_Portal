using Hackathon_CV_Portal.Domain.AppliedCurriculumVitaes.Commands;
using System.ComponentModel.DataAnnotations;

namespace Hackathon_CV_Portal.Domain.AppliedCCVs
{
    public class AppliedCurriculumVitae
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int VacancyId { get; private set; }
        [Required]
        public int CurriculumVitaeId { get; private set; }
        [Required]
        public DateTime AppliedDate { get; set; }

        public AppliedCurriculumVitae() { }

        public AppliedCurriculumVitae(ApplyCurriculimVataeCommand command)
        {
            VacancyId = command.VacansyId;
            CurriculumVitaeId = command.CurriculimVataeId;
            AppliedDate = DateTime.Now;
        }
    }
}
