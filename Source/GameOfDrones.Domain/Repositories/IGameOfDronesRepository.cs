using System.Collections.Generic;
using System.Linq;
using GameOfDrones.Domain.Common;
using GameOfDrones.Domain.Entities;
using GameOfDrones.Domain.Enums;

namespace GameOfDrones.Domain.Repositories
{
    public interface IGameOfDronesRepository
    {
        //Players
        Player GetPlayerById(int id);
        Player GetPlayerByName(string name);
        bool AddPlayer(Player player);
        bool AddPlayer(string playerName);
        void UpdatePlayer(Player player);
        IQueryable<Player> GetAllPlayers();
        void RemovePlayer(Player player);
        PlayerStats GetPlayerStatsById(int id);
        PlayerStats GetPlayerStatsFromMatchById(int matchId, int playerId);
        PlayerStats GetPlayerStatsFromMatchByName(int matchId, string playerName);

        //Matches
        Match GetMatchById(int id);
        bool AddMatch(Match match);
        IQueryable<Match> GetAllMatches();
        RoundResult EvalRound(int ruleId, string player1Move, string player2Move);


        //Rules
        Rule GetCurrentRule();
        IQueryable<Rule> GetAllRules();
        Rule GetRuleById(int ruleId);
        void AddRule(Rule rule);
        bool ValidateRule(int ruleId);
        ICollection<string> GetValidMoves(int ruleId);


        //Db Save Changes
        SaveChangesResponse SaveChanges();

    }
}
