namespace Hackathon_CV_Portal.Application.Settings
{
    public class EmailSettings
    {
        public string FromName { get; set; }
        public string PlatformMailAddress { get; set; }
        public string ContactUsMailAddress { get; set; }
        public string ServerAddress { get; set; }
        public int ServerPort { get; set; }
        public bool ServerUseSsl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
