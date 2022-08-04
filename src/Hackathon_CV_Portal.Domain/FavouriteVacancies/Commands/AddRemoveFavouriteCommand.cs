namespace Hackathon_CV_Portal.Domain.FavouriteVacancies.Commands
{
    public class AddRemoveFavouriteCommand
    {
        public int VacasnyId { get; set; }
        public UserModel UserModel { get; set; }
    }
}
