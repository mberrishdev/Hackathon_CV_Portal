using Hackathon_CV_Portal.Domain.Responsibilities.Commands;
using Hackathon_CV_Portal.Domain.Vcancies;
using System.ComponentModel.DataAnnotations;

namespace Hackathon_CV_Portal.Domain.Responsibilities
{
    public class Responsibility
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string ResponsibilityName { get; set; }

        public int VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }

        public Responsibility() { }
        public Responsibility(AddResponsibilityCommand command)
        {
            ResponsibilityName = command.ResponsibilityName;
            VacancyId = command.VacancyId;
        }
    }
}
