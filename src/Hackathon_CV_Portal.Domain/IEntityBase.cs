using System.ComponentModel.DataAnnotations;

namespace Hackathon_CV_Portal.Domain
{
    public interface IEntityBase
    {
        [Key]
        int Id { get; set; }
    }
}
