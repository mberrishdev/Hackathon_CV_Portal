using Hackathon_CV_Portal.Domain.Qualifications.Commands;
using Hackathon_CV_Portal.Domain.Vcancies;
using System.ComponentModel.DataAnnotations;

namespace Hackathon_CV_Portal.Domain.Qualifications
{
    public class Qualification
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string QualificationName { get; set; }

        public int VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }

        public Qualification() { }
        public Qualification(AddQualificationCommand command)
        {
            QualificationName = command.QualificationName;
            VacancyId = command.VacancyId;
        }
    }
}
