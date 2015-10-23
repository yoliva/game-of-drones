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
        public PlayerStats Player1Stats { get; set; }
        public PlayerStats Player2Stats { get; set; }
    }
}
