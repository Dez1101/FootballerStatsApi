using System.ComponentModel.DataAnnotations;

namespace FootballerStatsApi.Dtos
{
    public class AddMatchStatisticDto
    {
        [Required]
        public Guid FootballerId { get; set; }

        [Required]
        [StringLength(100)]
        public string Opposition { get; set; } = string.Empty;

        [Range(0, 120)]
        public int MinutesPlayed { get; set; }

        [Range(0, int.MaxValue)]
        public int Goals { get; set; }

        [Range(0, int.MaxValue)]
        public int Assists { get; set; }

        [Range(0, 100)]
        public double PassCompletion { get; set; }
    }
}
