using System.Data.Entity;
using GameOfDrones.Domain.Entities;

namespace GameOfDrones.Data
{
    public class GameOfDronesDbContext : DbContext
    {
        public GameOfDronesDbContext():
            base("DefaultConnection")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>()
                .HasOptional(u => u.Player1Stats)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Match>()
                .HasOptional(u => u.Player2Stats)
                .WithMany()
                .WillCascadeOnDelete(false);
        }

        public IDbSet<Player> Players { get; set; }
        public IDbSet<PlayerStats> PlayerStatses { get; set; }
        public IDbSet<Match> Matches { get; set; }
        public IDbSet<Rule> Rules { get; set; }
    }
}
