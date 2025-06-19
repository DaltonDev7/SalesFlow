using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Feature.Inventories.Commands;
using SalesFlow.Application.Feature.Inventories.Queries;
using SalesFlow.Application.Interfaces.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SalesFlow.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class HistorialOrders : BaseApiController
    {
        private readonly IHistoryOrdersServices historyOrdersService;

        public HistorialOrders(IHistoryOrdersServices historyOrdersService)
        {
            this.historyOrdersService = historyOrdersService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateHistoryOrdersDto command)
        {
            var data = await historyOrdersService.CreateHistorial(command);
            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await historyOrdersService.GetHistorial());
        }
    }
}
