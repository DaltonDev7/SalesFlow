using Microsoft.AspNetCore.Mvc;
using SalesFlow.Domain.Entities;
using SalesFlow.Persistence.Context;

namespace SalesFlow.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ApplicationContext _applicationContext;



        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ApplicationContext applicationContext, ILogger<WeatherForecastController> logger)
        {
            _applicationContext = applicationContext;
            _logger = logger;
        }



        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _applicationContext.Category.ToList();
            return Ok(categories);
        }

    }
}
