using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly RecipeService _recipeService;

        public RecipesController(RecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        // POST: api/recipes
        [HttpPost]
        public async Task<IActionResult> AddRecipe([FromBody] RecipeModel recipe)
        {
            if (string.IsNullOrEmpty(recipe.Title) || string.IsNullOrEmpty(recipe.Instructions))
            {
                return BadRequest(new { message = "Title and instructions are required." });
            }

            // Automatically set the date
            recipe.DateAdded = DateTime.UtcNow;

            await _recipeService.AddRecipeAsync(recipe);
            return Ok();
        }

        // GET: api/recipes
        [HttpGet]
        public async Task<ActionResult<List<RecipeModel>>> GetAllRecipes()
        {
            var recipes = await _recipeService.GetAllRecipesAsync();
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipeById(string id)
        {
            try
            {
                var recipe = await _recipeService.GetRecipeByIdAsync(id);

                if (recipe == null)
                {
                    return NotFound(new { message = "Recipe not found." });
                }

                return Ok(recipe);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
