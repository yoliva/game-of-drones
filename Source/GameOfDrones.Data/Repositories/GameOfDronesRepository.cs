using System;
using System.Data.Entity;
using System.Linq;
using GameOfDrones.Domain.Common;
using GameOfDrones.Domain.Entities;
using GameOfDrones.Domain.Repositories;

namespace GameOfDrones.Data.Repositories
{
    public class GameOfDronesRepository : IUruItTestRepository
    {
        private readonly GameOfDronesDbContext _ctx;

        public GameOfDronesRepository(GameOfDronesDbContext ctx)
        {
            _ctx = ctx;
        }
        public Player GetPlayerById(int id)
        {
            throw new NotImplementedException();
        }

        public Player GetPlayerByName(string name)
        {
            return _ctx.Players.FirstOrDefault(x => x.Name == name);
        }

        public bool AddPlayer(Player player)
        {
            if (_ctx.Players.Any(x => x.Name == player.Name))
                return false;
            _ctx.Players.Add(player);
            return true;
        }

        public bool AddPlayer(string playerName)
        {
            if (_ctx.Players.Any(x => x.Name == playerName))
                return false;
            _ctx.Players.Add(new Player
            {
                Name = playerName
            });
            return true;
        }

        public void UpdatePlayer(Player player)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Player> GetAllPlayers()
        {
            return _ctx.Players;
        }

        public void RemovePlayer(Player player)
        {
            throw new NotImplementedException();
        }

        public Match GetMatchById(int id)
        {
            return _ctx.Matches.Find(id);
        }

        public Match GetMatchByName(int id)
        {
            throw new NotImplementedException();
        }

        public bool AddMatch(Match match)
        {
            _ctx.Matches.Add(match);
            return true;
        }

        public IQueryable<Match> GetAllMatches()
        {
            return _ctx.Matches.Include(x=>x.Rule);
        }

        public SaveChangesResponse SaveChanges()
        {
            try
            {
                _ctx.SaveChanges();
                return new SaveChangesResponse {Success = true};
            }
            catch (Exception ex)
            {
                //TODO: Log the errors
                return new SaveChangesResponse {Success = true};
            }
        }

        public Rule GetCurrentRule()
        {
            return _ctx.Rules.FirstOrDefault(x => x.IsCurrent);
        }

        public IQueryable<Rule> GetAllRules()
        {
            return _ctx.Rules;
        }

        public Rule GetRuleById(int ruleId)
        {
            return _ctx.Rules.Find(ruleId);
        }

        public PlayerStats GetPlayerStatsById(int id)
        {
            return _ctx.PlayerStatses.Find(id);
        }

        public PlayerStats GetPlayerStatsFromMatchById(int matchId, int playerId)
        {
            throw new NotImplementedException();
        }
    }
}
