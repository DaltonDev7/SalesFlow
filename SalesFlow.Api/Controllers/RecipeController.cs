
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesFlow.Application.Feature.Recipes.Commands;
using SalesFlow.Application.Feature.Recipes.Queries;

namespace SalesFlow.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RecipeController : BaseApiController
    {

        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipesCommand command)
        {
            // Validamos si el comando es válido (por ejemplo, asegurarse de que IdProduct y IdIngredient no sean nulos)
            if (command == null || command.IdProduct <= 0 || command.IdIngredient <= 0)
            {
                return BadRequest(new { Message = "Datos inválidos para la creación de la receta." });
            }

            // Llamamos al CommandHandler para crear la receta
            var result = await Mediator.Send(command);

            if (!result.Succeeded)
            {
                return BadRequest(new { Message = result.Message });
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecipes()
        {

            var recipes = await Mediator.Send(new GetAllRecipesQuery());

            return Ok(recipes);
        }

    }
}
