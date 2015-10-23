using GameOfDrones.Domain.Enums;

namespace GameOfDrones.Domain.Entities
{
    public class PlayerStats
    {
        public int Id { get; set; }
        public Player Player { get; set; }
        public int WinnerRounds { get; set; }
        public int LoserRounds { get; set; }
        public int DrawRounds { get; set; }
        public MatchResult MatchResult { get; set; }

        public void Update(int w, int l, int d)
        {
            WinnerRounds = w;
            LoserRounds = l;
            DrawRounds = d;
        }
    }
}
