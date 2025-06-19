using Microsoft.AspNetCore.Mvc;
using SalesFlow.Application.Feature.Payments.Commands;
using SalesFlow.Application.Feature.Payments.Queries;
using SalesFlow.Application.Interfaces.Services;

namespace SalesFlow.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PaymentController : BaseApiController
    {

        private readonly IPaymentServices _paymentServices;

        public PaymentController(IPaymentServices paymentServices)
        {
            _paymentServices = paymentServices;
        }

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

        [HttpGet]
        [Route("GetPaymentsMethods")]
        public async Task<IActionResult> GetPaymentsMethods()
        {
            var response = await _paymentServices.GetPaymentsMethod();

            return Ok(response);
        }

    }
}
