namespace Hackathon_CV_Portal.Domain.Qualifications.Commands
{
    public class DeleteQualificationCommand
    {
        public int QualificationId { get; set; }
        public UserModel UserModel { get; set; }
        public int VacancyId { get; set; }
    }
}
