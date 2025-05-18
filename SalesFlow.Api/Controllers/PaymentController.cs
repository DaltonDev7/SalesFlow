using Microsoft.AspNetCore.Mvc;
using SalesFlow.Application.Feature.Payments.Commands;
using SalesFlow.Application.Feature.Payments.Queries;

namespace SalesFlow.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PaymentController : BaseApiController
    {

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result); 
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentsDetail()
        {
            var response = await Mediator.Send(new GetPaymentsDetailQuery());
            
           return Ok(response);
        }

    }
}
