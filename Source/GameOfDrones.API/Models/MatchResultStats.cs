using System.ComponentModel.DataAnnotations;

namespace GameOfDrones.API.Models
{
    public class MatchResultStats
    {
        [Required]
        public int PlayerId { get; set; }
        public int WinnerRounds { get; set; }
        public int LoserRounds { get; set; }
        public int DrawRounds { get; set; }
    }
}