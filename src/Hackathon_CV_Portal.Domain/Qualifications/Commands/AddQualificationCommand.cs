namespace Hackathon_CV_Portal.Domain.Qualifications.Commands
{
    public class AddQualificationCommand
    {
        public string QualificationName { get; set; }
        public UserModel UserModel { get; set; }
        public int VacancyId { get; set; }
    }
}
