using Microsoft.AspNetCore.Mvc;
using SalesFlow.Application.Dtos.Authentication;
using SalesFlow.Application.Interfaces.Services;
using SalesFlow.Application.Wrappers;

namespace SalesFlow.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RoleController : ControllerBase
    {

        private readonly IRolesServices _roleService; 
        public RoleController(IRolesServices roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _roleService.GetAllRoles();
            return Ok(response);
        }

        [HttpPost("addrole")]
        public async Task<IActionResult> AssignRole([FromBody] AddRoleRequest command)
        {
            if (command == null || command.IdUser <= 0 || command.IdRol <= 0)
            {
                return BadRequest(new ApiResponse<string>("El UserId y RoleId son obligatorios."));
            }

            var response = await _roleService.AssignRoleToUser(command.IdUser, command.IdRol);
            return Ok(response);

        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddOrUpdateRol command)
        {
            if (command == null || string.IsNullOrEmpty(command.Name))
            {
                return BadRequest(new ApiResponse<string>("El nombre del rol es obligatorio."));
            }

            var response = await _roleService.CreateRol(command); 
            return Ok(response); 
        }

        [HttpPut]
        public async Task<IActionResult> Update(AddOrUpdateRol command)
        {
            if (command == null)
            {
                return BadRequest(new ApiResponse<string>("El objeto de rol es nulo."));
            }

            if (string.IsNullOrEmpty(command.Name))
            {
                return BadRequest(new ApiResponse<string>("El nombre del rol es obligatorio."));
            }

            var response = await _roleService.UpdateRol(command);
            return Ok(response);
        }
    }
}
