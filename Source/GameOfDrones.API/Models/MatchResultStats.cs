namespace GameOfDrones.API.Models
{
    public class MatchResultStats
    {
        public int PlayerId { get; set; }
        public int WinnerRounds { get; set; }
        public int LoserRounds { get; set; }
        public int DrawRounds { get; set; }
    }
}