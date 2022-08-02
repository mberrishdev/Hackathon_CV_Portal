namespace Hackathon_CV_Portal.Domain.Responsibilities.Commands
{
    public class AddResponsibilityCommand
    {
        public string ResponsibilityName { get; set; }
        public UserModel UserModel { get; set; }
        public int VacancyId { get; set; }
    }
}
