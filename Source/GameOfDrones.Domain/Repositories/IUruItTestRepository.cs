using System.Linq;
using GameOfDrones.Domain.Common;
using GameOfDrones.Domain.Entities;

namespace GameOfDrones.Domain.Repositories
{
    public interface IUruItTestRepository
    {
        //Player methods
        Player GetPlayerById(int id);
        Player GetPlayerByName(string name);
        bool AddPlayer(Player player);
        bool AddPlayer(string playerName);
        void UpdatePlayer(Player player);
        IQueryable<Player> GetAllPlayers();
        void RemovePlayer(Player player);

        //Match Methods
        Match GetMatchById(int id);
        Match GetMatchByName(int id);
        bool AddMatch(Match match);
        IQueryable<Match> GetAllMatches();

        //Db Save Changes
        SaveChangesResponse SaveChanges();
        Rule GetCurrentRule();
        IQueryable<Rule> GetAllRules();
        Rule GetRuleById(int ruleId);
        PlayerStats GetPlayerStatsById(int id);
        PlayerStats GetPlayerStatsFromMatchById(int matchId, int playerId);
    }
}
