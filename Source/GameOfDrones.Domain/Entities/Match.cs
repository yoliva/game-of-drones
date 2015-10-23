using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameOfDrones.Domain.Entities
{
    public class Match
    {
        public int Id { get; set; }
        [Required]
        public Rule Rule { get; set; }
        public int RuleId { get; set; }
        public ICollection<PlayerStats> PlayersStatses { get; set; }
        public Player Winner { get; set; }

        public void AddPlayerStats(PlayerStats playerStats)
        {
            if(PlayersStatses == null)
                PlayersStatses = new LinkedList<PlayerStats>();
            PlayersStatses.Add(playerStats);
        }
    }
}
