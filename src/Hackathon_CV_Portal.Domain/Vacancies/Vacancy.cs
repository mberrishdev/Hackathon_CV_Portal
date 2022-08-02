using Hackathon_CV_Portal.Domain.Categories;
using Hackathon_CV_Portal.Domain.Enums;
using Hackathon_CV_Portal.Domain.Locations;
using Hackathon_CV_Portal.Domain.Qualifications;
using Hackathon_CV_Portal.Domain.Responsibilities;
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
        [Required]
        public VacancyType Type { get; set; }
        public string Email { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }
        [Required]
        public DateTime DeadLine { get; set; }
        [Required, Column(TypeName = "NVARCHAR"), MaxLength(500)]
        public string Description { get; set; }

        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int LocationId { get; set; }
        public ApplicationUser User { get; set; }
        public Category Category { get; set; }
        public Location Location { get; set; }
        public ICollection<Responsibility> Responsibilities { get; set; }
        public ICollection<Qualification> Qualifications { get; set; }

        public Vacancy()
        {
        }

        public Vacancy(CreateVacancyCommand command)
        {
            Title = command.Title;
            SalaryRange = command.SalaryRange;
            CompanyName = command.CompanyName;
            PublishDate = DateTime.Now;
            DeadLine = command.DeadLine;
            Description = command.Description;
            LocationId = command.LocationId;
            CategoryId = command.CategoryId;
            UserId = command.UserModel.UserId;
            Email = command.Email;
            Type = command.Type;
        }

        public void Update(UpdateVacancyCommand command)
        {
            Title = command.Title;
            SalaryRange = command.SalaryRange;
            CompanyName = command.CompanyName;
            PublishDate = DateTime.Now;
            DeadLine = command.DeadLine;
            Description = command.Description;
            LocationId = command.LocationId;
            Email = command.Email;
            CategoryId = command.CategoryId;
            Type = command.Type;
        }
    }
}
