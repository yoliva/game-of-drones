using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameOfDrones.API.Models
{
    public class EvalRoundViewModel
    {
        [Required]
        public int RuleId { get; set; }
        [Required]
        public string Player1Move { get; set; }
        [Required]
        public string Player2Move { get; set; }
    }
}