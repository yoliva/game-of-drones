using System.Collections.Generic;
using GameOfDrones.Domain.Entities;

namespace GameOfDrones.API.Models
{
    public class UpdateMatchStatsViewModel
    {
        public int MatchId { get; set; }
        public PlayerStatsViewModel[] PlayersStats { get; set; }
    }
}