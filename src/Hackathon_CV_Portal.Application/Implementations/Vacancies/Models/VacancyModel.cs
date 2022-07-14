namespace Hackathon_CV_Portal.Application.Implementations.Vacancies.Models
{
    public class VacancyModel
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public int Salary { get; set; }
        public string Currency { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime DeadLine { get; set; }
        public string Description { get; set; }
    }
}
