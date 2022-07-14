using Hackathon_CV_Portal.Domain.Categories;
using Hackathon_CV_Portal.Domain.Users;
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
        public int Salary { get; set; }
        [MaxLength(15)]
        public string Currency { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }
        [Required]
        public DateTime DeadLine { get; set; }
        [Required, Column(TypeName = "NVARCHAR"), MaxLength(500)]
        public string Description { get; set; }

        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public ApplicationUser User { get; set; }
        public Category Category { get; set; }
        //public ICollection<FavouriteVacancie> FavouriteVacancies { get; set; }
    }
}
