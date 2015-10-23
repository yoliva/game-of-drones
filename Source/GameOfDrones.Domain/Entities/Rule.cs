using System.ComponentModel.DataAnnotations;

namespace GameOfDrones.Domain.Entities
{
    public class Rule
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string RuleDefinition { get; set; }
        public bool IsCurrent { get; set; }
    }
}
