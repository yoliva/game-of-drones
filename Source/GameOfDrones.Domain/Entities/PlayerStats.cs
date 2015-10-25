using GameOfDrones.Domain.Enums;

namespace GameOfDrones.Domain.Entities
{
    public class PlayerStats
    {
        public int Id { get; set; }
        public Player Player { get; set; }
        public int WonRounds { get; set; }
        public int LostRounds { get; set; }
        public int TiedRounds { get; set; }
        public Match Match { get; set; }
        public MatchResult MatchResult { get; set; }

        public void Update(int w, int l, int d, MatchResult matchResult)
        {
            WonRounds = w;
            LostRounds = l;
            TiedRounds = d;
            MatchResult = matchResult;
        }
    }
}
