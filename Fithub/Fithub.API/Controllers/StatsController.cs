using Fithub.API.Helpers;
using Fithub.API.Interfaces;
using Fithub.API.Models.Stats;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fithub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : BaseController
    {
        private readonly IStatsService _statsService;

        public StatsController(IStatsService statsService)
        {
            _statsService = statsService;
        }

        [HttpGet]
        [Route("day")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ExercisesByDay>>> GetByDay()
        {
            var result = await _statsService.GetExerciseCountByDay(GetUserId());
            return Ok(result);
        }

        [HttpGet]
        [Route("type")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ExercisesByType>>> GetByType()
        {
            var result = await _statsService.GetExerciseCountByType(GetUserId());
            return Ok(result);
        }
    }
}
