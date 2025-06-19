using Microsoft.AspNetCore.Mvc;
using SalesFlow.Application.Feature.Recipes.Queries;
using SalesFlow.Application.Interfaces.Services;
using SalesFlow.Application.Services;
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
    }
}
