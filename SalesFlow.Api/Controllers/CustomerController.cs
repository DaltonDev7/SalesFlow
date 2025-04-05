
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesFlow.Application.Feature.Customers.Commands;
using SalesFlow.Application.Feature.Customers.Queries;

namespace SalesFlow.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CustomerController : BaseApiController
    {
        // Endpoint para crear un cliente
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // Endpoint para obtener todos los clientes
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllCustomersQuery()));
        }

        // Endpoint para actualizar un cliente
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCustomerCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
