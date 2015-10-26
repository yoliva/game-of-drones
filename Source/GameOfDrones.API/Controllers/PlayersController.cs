using System.Collections.Generic;
using System.Dynamic;
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

                wonRounds += playerStat.WonRounds;
                tiedRounds += playerStat.TiedRounds;
                loseRounds += playerStat.LostRounds;
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

        [Route("statsComparison/{player1Name}/{player2Name}")]
        public IHttpActionResult GetStatsComparison(string player1Name, string player2Name)
        {

            var player1 = _gameOfDronesRepository.GetPlayerByName(player1Name);
            var player2 = _gameOfDronesRepository.GetPlayerByName(player2Name);


            var playerStatsFaceToFace =
                player1.PlayerStatses.Where(
                    x =>
                        x.MatchResult != MatchResult.Invalid &&
                        x.Match.PlayersStatses.Any(ps => ps.Player.Name == player2.Name)).ToArray();

            dynamic player1Details = new ExpandoObject();
            player1Details.roundsWon = 0;
            player1Details.roundsLost = 0;
            player1Details.roundsTied = 0;
            foreach (var playerStat in playerStatsFaceToFace)
            {
                player1Details.roundsWon += playerStat.WonRounds;
                player1Details.roundsLost += playerStat.LostRounds;
                player1Details.roundsTied += playerStat.TiedRounds;
            }
            int totalRounds = player1Details.roundsWon + player1Details.roundsTied + player1Details.roundsLost;

            var totalMatchs = playerStatsFaceToFace.Count();
            var matchesWonByPlayer1 = playerStatsFaceToFace.Count(ps => ps.MatchResult == MatchResult.Winner);
            return Ok(new
            {
                player1 = new
                {
                    name = player1.Name,
                    played = totalMatchs,
                    won = matchesWonByPlayer1,
                    lost = totalMatchs - matchesWonByPlayer1,
                    player1Details.roundsWon,
                    player1Details.roundsTied,
                    player1Details.roundsLost,
                },
                player2 = new
                {
                    name = player2.Name,
                    played = totalMatchs,
                    won = totalMatchs - matchesWonByPlayer1,
                    lost = matchesWonByPlayer1,
                    roundsWon = player1Details.roundsLost,
                    player1Details.roundsTied,
                    roundsLost = player1Details.roundsWon
                }
            });
        }
    }
}
