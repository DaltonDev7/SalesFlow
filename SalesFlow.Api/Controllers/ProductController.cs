using Microsoft.AspNetCore.Mvc;
using SalesFlow.Application.Feature.Products.Commands;
using SalesFlow.Application.Feature.Products.Commands.CreateProduct;
using SalesFlow.Application.Feature.Products.Queries;

namespace SalesFlow.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Add(CreateProductCommand command)
        {
            var dataId = await Mediator.Send(command);
            return Ok(dataId);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllProductQuery()));
        }

        [HttpGet]
        [Route("GetIngredients")]
        public async Task<IActionResult> GetIngridients()
        {
            return Ok(await Mediator.Send(new GetAllIngredientsQuery()));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

    }
}
