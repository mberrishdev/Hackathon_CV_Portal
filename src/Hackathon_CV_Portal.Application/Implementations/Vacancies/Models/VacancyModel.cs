namespace Hackathon_CV_Portal.Application.Implementations.Vacancies.Models
{
    public class VacancyModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public string Title { get; set; }
        public string SalaryRange { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime DeadLine { get; set; }
        public string Description { get; set; }
        public string Responsibility { get; set; }
        public string Qualifications { get; set; }
        public bool IsFavourite { get; set; }
    }
}
