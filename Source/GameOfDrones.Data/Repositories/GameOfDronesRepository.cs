using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using GameOfDrones.Domain.Common;
using GameOfDrones.Domain.Entities;
using GameOfDrones.Domain.Enums;
using GameOfDrones.Domain.Repositories;

namespace GameOfDrones.Data.Repositories
{
    public class GameOfDronesRepository : IGameOfDronesRepository
    {
        private readonly GameOfDronesDbContext _ctx;

        public GameOfDronesRepository(GameOfDronesDbContext ctx)
        {
            _ctx = ctx;
        }

        //Players
        public Player GetPlayerById(int id)
        {
            return _ctx.Players.
                       Include(x => x.PlayerStatses.Select(ps => ps.Match)).
                       FirstOrDefault(p => p.Id == id);
        }
        public Player GetPlayerByName(string name)
        {
            return _ctx.Players.
                Include(x => x.PlayerStatses.Select(ps => ps.Match)).
                FirstOrDefault(x => x.Name == name);
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
            //TODO:implement this
            throw new NotImplementedException();
        }
        public IQueryable<Player> GetAllPlayers()
        {
            return _ctx.Players;
        }
        public void RemovePlayer(Player player)
        {
            //TODO: implement this
            throw new NotImplementedException();
        }
        public PlayerStats GetPlayerStatsById(int id)
        {
            return _ctx.PlayerStatses.Find(id);
        }
        public PlayerStats GetPlayerStatsFromMatchById(int matchId, int playerId)
        {
            var player = _ctx.Players.Include(x => x.PlayerStatses).First(x => x.Id == playerId);
            return player.PlayerStatses.First(ps => ps.Match.Id == matchId);
        }
        public PlayerStats GetPlayerStatsFromMatchByName(int matchId, string playerName)
        {
            var player = _ctx.Players
                .Where(x => x.Name == playerName)
                .Include(x => x.PlayerStatses.Select(ps => ps.Match))
                .FirstOrDefault();
            return player.PlayerStatses.First(ps => ps.Match.Id == matchId);
        }

        //Matches
        public Match GetMatchById(int id)
        {
            return
                _ctx.Matches.Where(x => x.Id == id)
                    .Include(x => x.PlayersStatses.Select(ps=>ps.Player))
                    .Include(x=>x.Winner)
                    .FirstOrDefault();
        }
        public bool AddMatch(Match match)
        {
            _ctx.Matches.Add(match);
            return true;
        }
        public IQueryable<Match> GetAllMatches()
        {
            return _ctx.Matches
                .Include(x=>x.Rule)
                .Include(x=>x.Winner);
        }

        public RoundResult EvalRound(int ruleId, string player1Move, string player2Move)
        {
            var rule = GetRuleById(ruleId);

            var doc = new XmlDocument();
            doc.LoadXml(rule.RuleDefinition);
            foreach (XmlNode child in doc.FirstChild)
            {
                if (child.Name == player1Move && child.Attributes["kills"].Value == player2Move)
                    return RoundResult.Player1Wins;

                if (child.Name == player2Move && child.Attributes["kills"].Value == player1Move)
                    return RoundResult.Player2Wins;
            }
            return RoundResult.Draw;
        }

        //Rules
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
        public ICollection<string> GetValidMoves(int ruleId)
        {
            var rule = GetRuleById(ruleId);
            var moves = new SortedSet<string>();
            var doc = new XmlDocument();
            doc.LoadXml(rule.RuleDefinition);
            foreach (XmlNode child in doc.FirstChild)
            {
                moves.Add(child.Name);
            }
            return moves.ToArray();
        }

        public void AddRule(Rule rule)
        {
            _ctx.Rules.Add(rule);
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
    }
}
