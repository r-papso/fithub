using Fithub.API.Helpers;
using Fithub.API.Interfaces;
using Fithub.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fithub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : BaseController
    {
        private readonly IExerciseService _exerciseService;

        public ExerciseController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        // GET api/<ExerciseController>/5
        [HttpGet("{categoryId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Exercise>>> Get(int categoryId)
        {
            var result = await _exerciseService.GetExercises(GetUserId(), categoryId);
            return Ok(result);
        }

        // POST api/<ExerciseController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Exercise>> Post([FromBody] Exercise exercise)
        {
            if (exercise.UserId != GetUserId())
                return Unauthorized();

            var result = await _exerciseService.AddExercise(exercise);
            return GetActionResult(result);
        }

        // PUT api/<ExerciseController>/5
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<Exercise>> Put([FromBody] Exercise exercise)
        {
            if (exercise.UserId != GetUserId())
                return Unauthorized();

            var result = await _exerciseService.UpdateExercise(exercise);
            return GetActionResult(result);
        }

        // DELETE api/<ExerciseController>/5
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<Exercise>> Delete([FromBody] Exercise exercise)
        {
            if (exercise.UserId != GetUserId())
                return Unauthorized();

            var result = await _exerciseService.DeleteExercise(exercise);
            return GetActionResult(result);
        }
    }
}
