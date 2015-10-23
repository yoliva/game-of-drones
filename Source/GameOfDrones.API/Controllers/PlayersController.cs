using System.Web.Http;
using GameOfDrones.Domain.Entities;
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
            {
                return NotFound();
            }
            return Ok(player);
        }
    }
}
