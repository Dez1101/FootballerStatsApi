using System.ComponentModel.DataAnnotations;

namespace FootballerStatsApi.Dtos
{
    public class UpdateMatchStatisticDto
    {
        [Required]
        public string Opposition { get; set; } = string.Empty;

        [Range(0, 140)]
        public int MinutesPlayed { get; set; }

        [Range(0, int.MaxValue)]
        public int Goals { get; set; }

        [Range(0, int.MaxValue)]
        public int Assists { get; set; }

        [Range(0, int.MaxValue)]
        public double PassCompletion { get; set; }
    }
}
