using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameOfDrones.API.Models
{
    public class PlayerStatsViewModel
    {
        [Required]
        public string PlayerName { get; set; }
        [Required]
        public int WonRounds { get; set; }
        [Required]
        public int LostRounds { get; set; }
        [Required]
        public int TiedRounds { get; set; }
    }
}