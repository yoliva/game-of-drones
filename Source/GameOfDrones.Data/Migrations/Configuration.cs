using GameOfDrones.Domain.Entities;

namespace GameOfDrones.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GameOfDronesDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(GameOfDronesDbContext context)
        {
            context.Players.AddOrUpdate(p => p.Name,
                new Player { Name = "Player1" },
                new Player { Name = "Player2" },
                new Player { Name = "Player3" });

            context.Rules.AddOrUpdate(x => x.Name,
                new Rule
                {
                    IsCurrent = true,
                    RuleDefinition =
                        "<moves> <Paper kills=\"Rock\" /><Rock kills=\"Scissors\" /><Scissors kills=\"Paper\" /></moves>",
                    Name = "Standard"
                }, new Rule
                {
                    IsCurrent = false,
                    RuleDefinition =
                        "<moves><Paper kills=\"Rock\" /><Paper kills=\"Spock\" /><Rock kills=\"Scissors\" /><Rock kills=\"Lizard\" /><Scissors kills=\"Paper\" />" +
                        "<Scissors kills=\"Lizard\" /><Lizard kills=\"Spock\" /><Lizard kills=\"Paper\" /><Spock kills=\"Scissors\" /><Spock kills=\"Rock\" /></moves>",
                    Name = "Spock"
                });

            context.SaveChanges();
        }
    }
}
