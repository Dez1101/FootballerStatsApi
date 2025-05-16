namespace FootballerStatsApi.Models
{
    public class Footballer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Club { get; set; }
        public string Position { get; set; }

        public ICollection<MatchStatistic> MatchStatistics { get; set; }
    }
}
