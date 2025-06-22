using Microsoft.AspNetCore.Mvc;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Services;

namespace SalesFlow.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITablesServices _tablesServices;

        public TableController(ITablesServices tablesServices)
        {
            _tablesServices = tablesServices;
        }

        // Obtener todas las mesas
        [HttpGet]
        public async Task<IActionResult> GetTables()
        {
            var response = await _tablesServices.GetTables();
            return Ok(response);
        }

        // Agregar nueva mesa
        [HttpPost]
        public async Task<IActionResult> AddTable([FromBody] AddEditTablesDto dto)
        {
            var response = await _tablesServices.Add(dto);
            return Ok(response);
        }

        // Actualizar mesa existente
        [HttpPut]
        public async Task<IActionResult> UpdateTable([FromBody] AddEditTablesDto dto)
        {
            var response = await _tablesServices.Update(dto);
            return Ok(response);
        }
    }
}
