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
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            var result = await _categoryService.GetCategories(GetUserId());
            return Ok(result);
        }

        // POST api/<CategoryController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Category>> Post([FromBody] Category category)
        {
            if (category.UserId != GetUserId())
                return Unauthorized();

            var result = await _categoryService.AddCategory(category);
            return GetActionResult(result);
        }

        // PUT api/<CategoryController>
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<Category>> Put([FromBody] Category category)
        {
            if (category.UserId != GetUserId())
                return Unauthorized();

            var result = await _categoryService.UpdateCategory(category);
            return GetActionResult(result);
        }

        // DELETE api/<CategoryController>
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<Category>> Delete([FromBody] Category category)
        {
            if (category.UserId != GetUserId())
                return Unauthorized();

            var result = await _categoryService.DeleteCategory(category);
            return GetActionResult(result);
        }
    }
}
