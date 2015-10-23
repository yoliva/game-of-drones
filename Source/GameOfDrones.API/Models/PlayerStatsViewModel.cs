using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameOfDrones.API.Models
{
    public class PlayerStatsViewModel
    {
        public string PlayerName { get; set; }
        public int WinnerRounds { get; set; }
        public int LoserRounds { get; set; }
        public int DrawRounds { get; set; }
    }
}