using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GameOfDrones.API.Models;
using GameOfDrones.Domain.Entities;
using GameOfDrones.Domain.Enums;
using GameOfDrones.Domain.Repositories;

namespace GameOfDrones.API.Controllers
{
    [RoutePrefix("api/v1/matches")]
    public class MatchesController : ApiController
    {
        private readonly IGameOfDronesRepository _gameOfDronesRepository;

        public MatchesController(IGameOfDronesRepository gameOfDronesRepository)
        {
            _gameOfDronesRepository = gameOfDronesRepository;
        }

        [Route("create")]
        public IHttpActionResult PostMatch([FromBody]CreateMatchViewModel data)
        {
            if (data.P1Name == data.P2Name)
                return BadRequest("Two different players are required for this game");

            //this condition verify if is required the creation of some player
            var p1Created = _gameOfDronesRepository.AddPlayer(data.P1Name);
            var p2Created = _gameOfDronesRepository.AddPlayer(data.P2Name);

            if (p1Created || p2Created)
                _gameOfDronesRepository.SaveChanges();

            var currentRule = _gameOfDronesRepository.GetCurrentRule();

            var match = new Match
            {
                Rule = currentRule,
            };
            
            //initialize default player stats. this data will be filled after the game ends
            var player1Stats = new PlayerStats
            {
                Player = _gameOfDronesRepository.GetPlayerByName(data.P1Name),
                MatchResult = MatchResult.Invalid,
            };
            var player2Stats = new PlayerStats
            {
                Player = _gameOfDronesRepository.GetPlayerByName(data.P2Name),
                MatchResult = MatchResult.Invalid,
            };

            match.AddPlayerStats(player1Stats);
            match.AddPlayerStats(player2Stats);

            _gameOfDronesRepository.AddMatch(match);
            _gameOfDronesRepository.SaveChanges();

            return Ok(match);
        }

        [Route("matchStats")]
        public IHttpActionResult PutMatchStats([FromBody]UpdateMatchStatsViewModel stats)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var match = _gameOfDronesRepository.GetMatchById(stats.MatchId);
            foreach (var item in stats.PlayersStats)
            {
                var s = match.PlayersStatses.First(x => x.Player.Name == item.PlayerName);
                s.Update(
                    item.WinnerRounds,
                    item.LoserRounds,
                    item.DrawRounds,
                    item.WinnerRounds == 3 ? MatchResult.Winner : MatchResult.Loser);
            }

            match.Winner = match.PlayersStatses.First(x => x.MatchResult == MatchResult.Winner).Player;

            _gameOfDronesRepository.SaveChanges();

            return Ok();
        }

        [Route("winner/{matchId}")]
        public IHttpActionResult GetMatchWinner(int matchId)
        {
            var match = _gameOfDronesRepository.GetMatchById(matchId);
            if (match == null)
                return NotFound();

            var winner = match.Winner;
            return Ok(new
            {
                winner
            });
        }

        [Route("getAll")]
        public IHttpActionResult GetAllMatches()
        {
            return Ok(_gameOfDronesRepository.GetAllMatches());
        }

        [Route("match/{matchId}")]
        public IHttpActionResult GetMatch(int matchId)
        {
            var match = _gameOfDronesRepository.GetMatchById(matchId);
            return Ok(new
            {
                match.Id,
                AvailableMoves = _gameOfDronesRepository.GetValidMoves(match.RuleId),
                match.PlayersStatses,
                match.Winner,
                match.RuleId
            });
        }
        [Route("evalRound")]
        public IHttpActionResult PostEvalRound([FromBody]EvalRoundViewModel data)
        {
            //if according to the match rules move(A) kills move(B) and move(B) kills move(A), is returned the first in the xml definition
            if(!ModelState.IsValid)
                return BadRequest();
            var result = _gameOfDronesRepository.EvalRound(data.RuleId, data.Player1Move, data.Player2Move).ToString();
            return Ok(new
            {
                result
            });
        }
    }
}
