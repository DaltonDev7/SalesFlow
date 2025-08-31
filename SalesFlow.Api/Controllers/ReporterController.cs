using Microsoft.AspNetCore.Mvc;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Feature.Recipes.Queries;
using SalesFlow.Application.Interfaces.Services;
using SalesFlow.Application.Services;
using SalesFlow.Application.Wrappers;
using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SalesFlow.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ReporterController : BaseApiController
    {

        private readonly IReporterServices _reporterServices;

        public ReporterController(IReporterServices reporterServices)
        {
            _reporterServices = reporterServices;
        }

        [HttpGet]
        [Route("GetGananciasToday")]
        public async Task<IActionResult> GetGanancias()
        {

            var response = await _reporterServices.GetTodayPaymentsAsync();
            return Ok(response);

        }


        //[HttpGet]
        //[Route("GetTodaySalesByCategoryAsync")]
        //public async Task<IActionResult> GetTodaySalesByCategoryAsync()
        //{

        //    var response = await _reporterServices.GetTodaySalesByCategoryAsync();
        //    return Ok(response);

        //}

        //[HttpGet]
        //[Route("GetTodaySalesByProductAsync")]
        //public async Task<IActionResult> GetTodaySalesByProductAsync()
        //{

        //    var response = await _reporterServices.GetTodaySalesByProductAsync();
        //    return Ok(response);

        //}

        //[HttpPost("GetSalesByCategoryByDate")]
        //public async Task<IActionResult> GetSalesByCategoryByDate([FromBody] DateFilterDto filter)
        //{
        //    var result = await _reporterServices.GetSalesByCategoryByDateAsync(filter.Date);
        //    return Ok(result);
        //}


        [HttpGet("GetSalesByCategory")]
        public async Task<IActionResult> GetSalesByCategoryAsync([FromQuery] DateTime? date)
        {
            var response = await _reporterServices.GetSalesByCategoryAsync(date);
            return Ok(response);
        }

        [HttpGet("GetSalesByProduct")]
        public async Task<IActionResult> GetSalesByProductAsync([FromQuery] DateTime? date)
        {
            var response = await _reporterServices.GetSalesByProductAsync(date);
            return Ok(response);
        }

        [HttpGet("sales-by-date")]
        public async Task<IActionResult> GetSalesByDate([FromQuery] DateTime date, [FromQuery] bool onlyPaid = true)
        {

            //if (!DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var localDate))
            //    return BadRequest("Invalid date format. Use yyyy-MM-dd.");

            var result = await _reporterServices.GetSalesByDateAsync(date, onlyPaid);
            return Ok(result);
        }

        [HttpGet("sales-by-month")]
        public async Task<IActionResult> GetSalesByMonth([FromQuery] int year, [FromQuery] int month, [FromQuery] bool onlyPaid = true)
        {
            if (year < 2000 || month < 1 || month > 12)
                return BadRequest("Parámetros inválidos. Usa year>=2000 y month entre 1 y 12.");

            var result = await _reporterServices.GetSalesByMonthAsync(year, month, onlyPaid);
            return Ok(result);
        }


    }
}
