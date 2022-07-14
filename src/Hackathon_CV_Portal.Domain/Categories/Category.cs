using Hackathon_CV_Portal.Domain.Vcancies;
using System.ComponentModel.DataAnnotations;

namespace Hackathon_CV_Portal.Domain.Categories
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Vacancy> Vacancies { get; set; }
    }
}
