using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesFlow.Application.Feature.Categories.Commands.CreateCategory;
using SalesFlow.Application.Feature.Categories.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace SalesFlow.Api.Controllers
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de categorias")]
    public class CategoryController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Add(CreateCategoryCommand command)
        {
            var categoryId = await Mediator.Send(command);
            return Ok(categoryId);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllCategoriesQuery()));
        }

    }
}
