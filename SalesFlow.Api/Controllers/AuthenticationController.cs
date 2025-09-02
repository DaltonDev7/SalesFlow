using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Dtos.Authentication;
using SalesFlow.Application.Interfaces.Services;
using SalesFlow.Application.Services;
using SalesFlow.Domain.Entities;
using System.Net;
using System.Security.Claims;

namespace SalesFlow.Api.Controllers
{

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationServices _authenticationServices;
        private readonly IRolesServices _roleService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISmtpEmailSender _emailSender; // tu servicio de email

        public AuthenticationController(
            IAuthenticationServices authenticationServices,
            IRolesServices roleService,
            ISmtpEmailSender emailSender,
            UserManager<ApplicationUser> userManager
            )
        {
            _authenticationServices = authenticationServices;
            _roleService = roleService;
            _emailSender = emailSender;
            _userManager = userManager;
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


        // 1) Solicitar restablecimiento de contraseña
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto req)
        {
            if (string.IsNullOrWhiteSpace(req.Email))
                return BadRequest("Email requerido.");

            var user = await _userManager.FindByEmailAsync(req.Email);

            // Seguridad: devuelve 200 aunque el correo no exista/confirmado para no filtrar usuarios
            if (user == null /*|| !(await _userManager.IsEmailConfirmedAsync(user))*/)
                return Ok(new { message = "Si el correo existe, se envió un código de restablecimiento." });

            // Generar token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // IMPORTANTE: codificar para enviar por URL / email sin romper caracteres
            var encodedToken = WebUtility.UrlEncode(token); // o HttpUtility.UrlEncode

            // Opción A: enviar un link (lo más común)
            //var resetLink = $"https://tu-frontend.com/reset-password?email={WebUtility.UrlEncode(req.Email)}&token={encodedToken}";

            // Opción B: enviar solo el token (si tendrás un input para “código”)
            var subject = "Restablecer contraseña";
            var body = $@"
            <p>Solicitaste restablecer tu contraseña.</p>
          
            <p>O copiar este código en la app:</p>
            <pre>{encodedToken}</pre>
            <p>Si no fuiste tú, ignora este mensaje.</p>
            ";

            await _emailSender.SendAsync(req.Email, subject, body);

            return Ok(new { message = "Si el correo existe, se envió un código de restablecimiento." });
        }

        // 2) Confirmar restablecimiento de contraseña
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto req)
        {
            if (string.IsNullOrWhiteSpace(req.Email) || string.IsNullOrWhiteSpace(req.Token) || string.IsNullOrWhiteSpace(req.NewPassword))
                return BadRequest("Datos incompletos.");

            var user = await _userManager.FindByEmailAsync(req.Email);
            if (user == null)
                return Ok(new { message = "Contraseña restablecida si el correo es válido." }); // no revelar existencia

            // El token llega URL-encoded desde el correo
            var decodedToken = WebUtility.UrlDecode(req.Token);

            var result = await _userManager.ResetPasswordAsync(user, decodedToken, req.NewPassword);
            if (!result.Succeeded)
            {
                // Devuelve errores útiles al front (p.e. password policy)
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { message = "No se pudo restablecer la contraseña.", errors });
            }

            return Ok(new { message = "Contraseña restablecida correctamente." });
        }

    }
}
