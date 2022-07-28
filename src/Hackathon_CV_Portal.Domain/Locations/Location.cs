using Hackathon_CV_Portal.Domain.Vcancies;
using System.ComponentModel.DataAnnotations;

namespace Hackathon_CV_Portal.Domain.Locations
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Country { get; set; }
        [Required, MaxLength(50)]
        public string City { get; set; }

        public ICollection<Vacancy> Vacancies { get; set; }
    }
}
