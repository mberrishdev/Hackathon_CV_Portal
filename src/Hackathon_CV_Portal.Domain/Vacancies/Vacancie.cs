using Hackathon_CV_Portal.Domain.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hackathon_CV_Portal.Domain.Vcancies
{
    public class Vacancie
    {
        [Key]
        public int Id { get; private set; }
        public int CategoryId { get; private set; }
        [Required, Column(TypeName = "NVARCHAR"), MaxLength(50)]
        public string Title { get; private set; }
        public int Salary { get; private set; }
        [MaxLength(15)]
        public string Currency { get; private set; }
        [Required]
        public DateTime PublishDate { get; private set; }
        [Required]
        public DateTime DeadLine { get; private set; }
        [Required, Column(TypeName = "NVARCHAR"), MaxLength(500)]
        public string Description { get; private set; }

        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        //public ICollection<FavouriteVacancie> FavouriteVacancies { get; set; }
    }
}
