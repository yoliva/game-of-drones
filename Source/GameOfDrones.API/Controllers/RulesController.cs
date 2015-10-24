using System.Linq;
using System.Web.Http;
using GameOfDrones.API.Models;
using GameOfDrones.Domain.Entities;
using GameOfDrones.Domain.Repositories;

namespace GameOfDrones.API.Controllers
{
    [RoutePrefix("api/v1/rules")]
    public class RulesController : ApiController
    {
        private readonly IGameOfDronesRepository _gameOfDronesRepository;

        public RulesController(IGameOfDronesRepository gameOfDronesRepository)
        {
            _gameOfDronesRepository = gameOfDronesRepository;
        }
        
        [Route("GetAll")]
        public IHttpActionResult GetAllRules()
        {
            return Ok(_gameOfDronesRepository.GetAllRules());
        }

        [Route("UpdateDefault/{ruleId}")]
        public IHttpActionResult PutUpdateDefaultRule(int ruleId)
        {
            var rule = _gameOfDronesRepository.GetRuleById(ruleId);

            if (rule == null)
                return NotFound();

            _gameOfDronesRepository.GetCurrentRule().IsCurrent = false;
            rule.IsCurrent = true;

            _gameOfDronesRepository.SaveChanges();

            return Ok(rule);
        }
        
        [Route("GetCurrent")]
        public IHttpActionResult GetCurrentRule()
        {
            return Ok(_gameOfDronesRepository.GetAllRules().FirstOrDefault(x => x.IsCurrent));
        }

        [Route("create")]
        public IHttpActionResult PostCreateRule([FromBody]RuleViewModel data)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var rule = new Rule()
            {
                Name = data.Name,
                RuleDefinition = data.RuleDefinition
            };
            _gameOfDronesRepository.AddRule(rule);
            _gameOfDronesRepository.SaveChanges();
            return Ok(rule);
        }
    }
}
