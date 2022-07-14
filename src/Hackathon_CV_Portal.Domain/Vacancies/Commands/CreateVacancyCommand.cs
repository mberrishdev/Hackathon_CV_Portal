namespace Hackathon_CV_Portal.Domain.Vacancies.Commands
{
    public class CreateVacancyCommand : UserModel
    {
        public string Title { get; set; }
        public int Salary { get; set; }
        public string Currency { get; set; }
        public DateTime DeadLine { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}
