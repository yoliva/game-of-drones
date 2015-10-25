using System.Linq;
using System.Web.Http;
using GameOfDrones.Domain.Entities;
using GameOfDrones.Domain.Enums;
using GameOfDrones.Domain.Repositories;

namespace GameOfDrones.API.Controllers
{
    [RoutePrefix("api/v1/players")]
    public class PlayersController : ApiController
    {
        private readonly IGameOfDronesRepository _gameOfDronesRepository;

        public PlayersController(IGameOfDronesRepository gameOfDronesRepository)
        {
            _gameOfDronesRepository = gameOfDronesRepository;
        }

        [Route("allPlayers")]
        public IHttpActionResult GetPlayers()
        {
            return Ok(_gameOfDronesRepository.GetAllPlayers());
        }
        
        [Route("{playerId}")]
        public IHttpActionResult GetPlayer(int playerId)
        {
            var player = _gameOfDronesRepository.GetPlayerById(playerId);

            if (player == null)
                return NotFound();

            return Ok(player);
        }

        [Route("stats/{playerName}")]
        public IHttpActionResult GetPlayerStats(string playerName)
        {
            var player = _gameOfDronesRepository.GetPlayerByName(playerName);

            if (player == null)
                return NotFound();

            var validStats = player.PlayerStatses.Where(x => x.MatchResult != MatchResult.Invalid);
            int wins = 0, loses = 0, wonRounds = 0, tiedRounds = 0, loseRounds = 0;
            foreach (var playerStat in validStats)
            {
                if (playerStat.Match.Winner.Id == player.Id)
                    wins++;
                else
                    loses++;

                wonRounds += playerStat.WinnerRounds;
                tiedRounds += playerStat.DrawRounds;
                loseRounds += playerStat.LoserRounds;
            }
            return Ok(new
            {
                id = player.Id,
                name=player.Name,
                wins,
                loses,
                wonRounds,
                tiedRounds,
                loseRounds
            });
        }
    }
}
