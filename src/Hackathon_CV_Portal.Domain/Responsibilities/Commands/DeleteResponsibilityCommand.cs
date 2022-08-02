namespace Hackathon_CV_Portal.Domain.Responsibilities.Commands
{
    public class DeleteResponsibilityCommand
    {
        public int ResponsibilityId { get; set; }
        public UserModel UserModel { get; set; }
        public int VacancyId { get; set; }
    }
}
