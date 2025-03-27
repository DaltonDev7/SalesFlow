using Microsoft.AspNetCore.Mvc;
using SalesFlow.Application.Feature.Inventories.Commands;
using SalesFlow.Application.Feature.Inventories.Queries;

namespace SalesFlow.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class InventoryController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Add(CreateInventoryCommand command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllInventoryQuery()));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateInventoryCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
