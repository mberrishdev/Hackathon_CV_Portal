namespace Hackathon_CV_Portal.Domain.FavouriteVacancies.Commands
{
    public class AddFavouriteCommand
    {
        public int VacasnyId { get; set; }
        public UserModel UserModel { get; set; }
    }
}
