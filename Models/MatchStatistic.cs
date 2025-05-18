namespace FootballerStatsApi.Models
{
    public class MatchStatistic
    {
        public Guid Id { get; set; }
        public Guid FootballerId { get; set; }
        public string Opposition { get; set; }
        public int MinutesPlayed { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public double PassCompletion { get; set; }

        public Footballer Footballer { get; set; }
    }
}
