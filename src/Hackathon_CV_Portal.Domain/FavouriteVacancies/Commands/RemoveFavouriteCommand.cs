namespace Hackathon_CV_Portal.Domain.FavouriteVacancies.Commands
{
    public class RemoveFavouriteCommand
    {
        public int VacasnyId { get; set; }
        public UserModel UserModel { get; set; }
    }
}
