using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameOfDrones.Domain.Entities;

namespace GameOfDrones.API.Models
{
    public class UpdateMatchStatsViewModel
    {
        [Required]
        public int MatchId { get; set; }
        public PlayerStatsViewModel[] PlayersStats { get; set; }
    }
}