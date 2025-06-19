using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesFlow.Application.Dtos.Authentication;
using SalesFlow.Application.Interfaces.Services;
using SalesFlow.Application.Services;
using System.Security.Claims;

namespace SalesFlow.Api.Controllers
{

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationServices _authenticationServices;
        private readonly IRolesServices _roleService;

        public AuthenticationController(IAuthenticationServices authenticationServices, IRolesServices roleService)
        {
            _authenticationServices = authenticationServices;
            _roleService = roleService;
        }

        [HttpGet("IsAuthenticated")]
        [Authorize] // <-- Esto requiere que el JWT esté presente y sea válido
        public async Task<IActionResult> IsAuthenticated()
        {
            // Obtén el ID del usuario desde el JWT
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized(new { message = "Token inválido o sin ID de usuario." });
            }

            // Buscar el usuario en la base de datos (puedes usar un servicio o repositorio aquí)
            var user = await _authenticationServices.GetUserByIdAsync(userId); // <-- Este método lo debes tener implementado

            if (user == null)
            {
                return NotFound(new { message = "Usuario no encontrado." });
            }

         

            return Ok(user);
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

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authenticationServices.UpdateUser(updateUser);
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
