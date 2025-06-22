using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Services;

namespace SalesFlow.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ReservationsController : BaseApiController
    {
        private readonly IReservationsServices _reservationsServices;

        public ReservationsController(IReservationsServices reservationsServices)
        {
            _reservationsServices = reservationsServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetReservations()
        {
            var response = await _reservationsServices.GetReservations();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddEditReservations dto)
        {
            var response = await _reservationsServices.Add(dto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AddEditReservations dto)
        {
            var response = await _reservationsServices.Update(dto);
            return Ok(response);
        }
    }
}
