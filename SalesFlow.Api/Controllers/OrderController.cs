using Microsoft.AspNetCore.Mvc;
using SalesFlow.Application.Feature.Orders.Commands;
using SalesFlow.Application.Feature.Orders.Queries;
using SalesFlow.Application.Feature.OrdersDetails.Queries;

namespace SalesFlow.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class OrderController : BaseApiController
    {
        // Crear un pedido
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrdersCommand command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        // Obtener todos los pedidos
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllOrdersQuery()));
        }

        // Obtener detalles de un pedido por Id
        [HttpGet("{idOrder}/details")]
        public async Task<IActionResult> GetDetails(int idOrder)
        {
            var command = new GetOrderDetailsByIdQuery(idOrder);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // Actualizar el estado de un pedido
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOrderStatusCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
