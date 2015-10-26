using System.ComponentModel.DataAnnotations;

namespace GameOfDrones.API.Models
{
    public class CreateMatchViewModel
    {
        [Required]
        public string P1Name { get; set; }
        [Required]
        public string P2Name { get; set; }
    }
}