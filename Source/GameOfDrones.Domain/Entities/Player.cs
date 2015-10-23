using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameOfDrones.Domain.Entities
{
    public class Player
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<PlayerStats> PlayerStatses { get; set; }
        public double GetPerformance
        {
            get { return Utils.CalculatePerformance(this); }
        }
    }
}
