using Hackathon_CV_Portal.Application.Implementations.Qualifications.Models;
using Hackathon_CV_Portal.Application.Implementations.Responsibilities.Models;
using System.ComponentModel.DataAnnotations;

namespace Hackathon_CV_Portal.Application.Implementations.Vacancies.Models
{
    public class VacancyModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Display(Name = "კატეგორია")]
        public string Category { get; set; }
        [Display(Name = "ტიპი")]
        public string Type { get; set; }
        [Display(Name = "კომპანიის სახელი")]
        public string CompanyName { get; set; }
        [Display(Name = "მისამართი")]
        public string Location { get; set; }
        [Display(Name = "სათაური")]
        public string Title { get; set; }
        [Display(Name = "სახელფასო მოლოდინი")]
        public string SalaryRange { get; set; }
        public DateTime PublishDate { get; set; }
        [Display(Name = "ვაკანსიის გამოგზავნის ბოლო ვადა")]
        public DateTime DeadLine { get; set; }
        public string Description { get; set; }
        [Display(Name = "მოთხოვნები")]
        public List<ResponsibilityVM> Responsibilities { get; set; }
        [Display(Name = "კვალიფიკაცია")]
        public List<QualificationVM> Qualifications { get; set; }
        public bool IsFavourite { get; set; }
        [Display(Name = "საკონტაქტო მეილი")]
        public string Email { get; set; }
        [Display(Name = "კატეგორია")]
        public int CategoryId { get; set; }
        [Display(Name = "ლოკაცია")]
        public int LocationId { get; set; }
    }
}
