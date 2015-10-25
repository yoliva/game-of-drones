using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameOfDrones.API.Models
{
    public class PlayerStatsViewModel
    {
        public string PlayerName { get; set; }
        public int WonRounds { get; set; }
        public int LostRounds { get; set; }
        public int TiedRounds { get; set; }
    }
}