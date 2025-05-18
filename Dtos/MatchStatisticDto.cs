using FootballerStatsApi.Models;
using System.Text.Json.Serialization;

namespace FootballerStatsApi.Dtos
{
    public class MatchStatisticDto
    {
        public Guid Id { get; set; }
        public Guid FootballerId { get; set; }
        public string Opposition { get; set; }
        public int MinutesPlayed { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public double PassCompletion { get; set; }

    }
}
