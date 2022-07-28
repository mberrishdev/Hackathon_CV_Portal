﻿using System.ComponentModel.DataAnnotations;

namespace Hackathon_CV_Portal.Web.Models.VacancyModels
{
    public class CreateVacancyDTO
    {
        [RequiredGreaterThanZero(ErrorMessage = "კატეგორიის შეყვანა აუცილებელია")]
        [Required(ErrorMessage = "კატეგორიის შეყვანა აუცილებელია")]
        [Display(Name = "კატეგორია")]
        public int CategoryId { get; set; }
        [RequiredGreaterThanZero(ErrorMessage = "ტიპის შეყვანა აუცილებელია")]
        [Required(ErrorMessage = "ტიპის შეყვანა აუცილებელია")]
        [Display(Name = "ტიპი")]
        public int Type { get; set; }
        [Required(ErrorMessage = "კომპანიის სახელი შეყვანა აუცილებელია")]
        [Display(Name = "კომპანიის სახელი")]
        public string CompanyName { get; set; }
        [RequiredGreaterThanZero(ErrorMessage = "მისამართი შეყვანა აუცილებელია")]
        [Required(ErrorMessage = "მისამართის შეყვანა აუცილებელია")]
        [Display(Name = "მისამართი")]
        public int LocationId { get; set; }
        [Required(ErrorMessage = "სათაურის შეყვანა აუცილებელია")]
        [Display(Name = "სათაური")]
        public string Title { get; set; }
        [Display(Name = "სახელფასო მოლოდინი")]
        public string SalaryRange { get; set; }
        [Required, CheckDateRange(ErrorMessage = "გამოგზავნის ვადა უნდა იყოს მეტი დღევნადელ თარიღზე")]
        [Display(Name = "ვაკანსიის გამოგზავნის ბოლო ვადა")]
        public DateTime DeadLine { get; set; }
        [Required(ErrorMessage = "აღწერის შეყვანა აუცილებელია")]
        [Display(Name = "აღწერა")]
        public string Description { get; set; }
        [Required(ErrorMessage = "მოვალეობების შეყვანა აუცილებელია")]
        [Display(Name = "მოვალეობები")]
        public string Responsibility { get; set; }
        [Required(ErrorMessage = "მოთხოვნების შეყვანა აუცილებელია")]
        [Display(Name = "მოთხოვნები")]
        public string Qualifications { get; set; }
    }

    public class CheckDateRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dt = (DateTime)value;
            if (dt >= DateTime.UtcNow)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "Make sure your date is >= than today");
        }
    }

    public class RequiredGreaterThanZero : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int i;
            return value != null && int.TryParse(value.ToString(), out i) && i > 0;
        }
    }
}
