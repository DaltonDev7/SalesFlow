using Microsoft.AspNetCore.Mvc;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Feature.Orders.Commands;
using SalesFlow.Application.Feature.Orders.Queries;
using SalesFlow.Application.Feature.OrdersDetails.Queries;
using SalesFlow.Application.Interfaces.Services;
using SalesFlow.Application.Wrappers;

namespace SalesFlow.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class OrderController : BaseApiController
    {

        private readonly IHistoryOrdersServices _historyOrderRepository;
        private readonly IReporterServices _reporterServices;

        public OrderController(IHistoryOrdersServices historyOrderRepository, IReporterServices reporterServices)
        {
            _historyOrderRepository = historyOrderRepository;
            _reporterServices = reporterServices;
        }



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
        [HttpPut("updateStatusOrder")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateOrderStatusCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }


       [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("by-customer/{customerId}")]
        public async Task<IActionResult> GetOrdersByCustomerId(int customerId)
        {
            var result = await _historyOrderRepository.GetOrdersByCustomerId(customerId);
            return Ok(result);
        }


        [HttpGet("getallorders")]
        public async Task<IActionResult> Getallorders()
        {
            var result = await _reporterServices.GetOrders();
            return Ok(result);
        }

        /// <summary>
        /// Obtener todas las órdenes con sus detalles
        /// </summary>
        [HttpGet("with-details")]
        public async Task<ActionResult<ApiResponse<List<GetOrderWithDetailsDto>>>> GetAllOrdersWithDetails()
        {
            var response = await _reporterServices.GetAllOrdersWithDetails();
            return Ok(response);
        }

        /// <summary>
        /// Obtener una orden específica con sus detalles
        /// </summary>
        [HttpGet("with-details/{orderId:int}")]
        public async Task<ActionResult<ApiResponse<GetOrderWithDetailsDto>>> GetOrderWithDetailsById(int orderId)
        {
            var response = await _reporterServices.GetOrderWithDetailsById(orderId);

            return Ok(response);
        }

    }
}
