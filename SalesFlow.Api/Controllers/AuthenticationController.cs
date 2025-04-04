using Microsoft.AspNetCore.Mvc;
using SalesFlow.Application.Dtos.Authentication;
using SalesFlow.Application.Interfaces.Services;

namespace SalesFlow.Api.Controllers
{

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationServices _authenticationServices;

        public AuthenticationController(IAuthenticationServices authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUser registerUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authenticationServices.RegisterUser(registerUser);
            return Ok(result);
           
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest signInRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authenticationServices.SignIn(signInRequest);
            return Ok(result);

        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> Get()
        {
           
            var result = await _authenticationServices.GetUsers();
            return Ok(result);

        }

    }
}
