namespace Hackathon_CV_Portal.Domain.Enums
{
    public enum VacancyType
    {
        FullTime = 1,
        PartTime = 2
    }

    public class VacancyTypeClass
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }
}
