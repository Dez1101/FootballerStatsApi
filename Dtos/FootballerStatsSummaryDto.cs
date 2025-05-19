namespace FootballerStatsApi.Dtos
{
    public class FootballerStatsSummaryDto
    {
        public Guid FootballerId { get; set; }
        public string Name { get; set; }
        public int TotalMatches { get; set; }
        public int TotalGoals { get; set; }
        public int TotalAssists { get; set; }
        public double AveragePassCompletion { get; set; }
        public int TotalMinutesPlayed { get; set; }
    }
}
