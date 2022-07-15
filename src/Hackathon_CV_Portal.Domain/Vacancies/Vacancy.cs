using Hackathon_CV_Portal.Domain.Categories;
using Hackathon_CV_Portal.Domain.Enums;
using Hackathon_CV_Portal.Domain.Users;
using Hackathon_CV_Portal.Domain.Vacancies.Commands;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hackathon_CV_Portal.Domain.Vcancies
{
    public class Vacancy
    {
        [Key]
        public int Id { get; set; }
        [Required, Column(TypeName = "NVARCHAR"), MaxLength(50)]
        public string Title { get; set; }
        [Required, MaxLength(50)]
        public string SalaryRange { get; set; }
        [Required, MaxLength(50)]
        public string CompanyName { get; set; }
        [Required, MaxLength(50)]
        public string Location { get; set; }
        [Required]
        public VacancyType Type { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }
        [Required]
        public DateTime DeadLine { get; set; }
        [Required, Column(TypeName = "NVARCHAR"), MaxLength(500)]
        public string Description { get; set; }
        [Required, Column(TypeName = "NVARCHAR"), MaxLength(500)]
        public string Responsibility { get; set; }
        [Required, Column(TypeName = "NVARCHAR"), MaxLength(500)]
        public string Qualifications { get; set; }

        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public ApplicationUser User { get; set; }
        public Category Category { get; set; }

        public Vacancy()
        {
        }

        public Vacancy(CreateVacancyCommand command)
        {
            Title = command.Title;
            SalaryRange = command.SalaryRange;
            CompanyName = command.CompanyName;
            Location = command.Location;
            PublishDate = DateTime.Now;
            DeadLine = command.DeadLine;
            Description = command.Description;
            Qualifications = command.Qualifications;
            Responsibility = command.Responsibility;
            CategoryId = command.CategoryId;
            UserId = command.UserModel.UserId;
            Type = command.Type;
        }
    }
}
