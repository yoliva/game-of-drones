using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameOfDrones.API.Models
{
    public class RuleViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string RuleDefinition { get; set; }
    }
}