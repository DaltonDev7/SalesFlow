using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Dtos.Authentication;
using SalesFlow.Application.Exception;
using SalesFlow.Application.Interfaces.Services;
using SalesFlow.Application.Wrappers;
using SalesFlow.Identity.Context;
using SalesFlow.Identity.Entities;
using System.Net;

namespace SalesFlow.Identity.Services
{
    public class RoleServices : IRolesServices
    {
        private readonly RoleManager<ApplicationUserRol> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IdentityContext _identityContext;

        public RoleServices(RoleManager<ApplicationUserRol> roleManager, UserManager<ApplicationUser> userManager, IdentityContext identityContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _identityContext = identityContext;
        }

        public async Task<ApiResponse<bool>> AssignRoleToUser(int userId, int roleId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                    throw new ApiException("Usuario no encontrado", (int)HttpStatusCode.NotFound);

                var role = await _roleManager.FindByIdAsync(roleId.ToString());
                if (role == null)
                    throw new ApiException("Rol no encontrado", (int)HttpStatusCode.NotFound);

                // Verifica si el usuario ya tiene el rol
                var userRoleExists = await _identityContext.UserRoles
                    .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

                if (userRoleExists)
                    throw new ApiException("El usuario ya tiene este rol", (int)HttpStatusCode.Conflict);

                // Agregar la relación en la tabla AspNetUserRoles
                var userRole = new ApplicationUserRoles
                {
                    UserId = userId,
                    RoleId = roleId
                };

                _identityContext.UserRoles.Add(userRole);
                await _identityContext.SaveChangesAsync();

                return new ApiResponse<bool>(true, "Rol asignado correctamente.");
            }
            catch (ApiException ex)
            {
                return new ApiResponse<bool>(false, ex.Message);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>()
                {
                    Message = "Error interno del servidor",
                    Succeeded = false,
                    Data = false
                };
            }
        }

        public async Task<List<RolDto>> GetUserRolesAsync(int userId)
        {
            var roles = await _identityContext.UserRoles
                .Where(ur => ur.UserId == userId)  // Filtrar por el usuario específico
                .Select( ur => new RolDto
                {
                    Id = ur.RoleId,  // Obtener el ID del rol
                    Name = _identityContext.Roles.FirstOrDefault(x => x.Id == ur.RoleId).Name,
                })
                .ToListAsync(); // Ejecutar la consulta

            return roles;
        }


        public async Task<ApiResponse<string>> CreateRol(AddOrUpdateRol registerRol)
        {
            var response = await _roleManager.FindByNameAsync(registerRol.Name);

            if (response != null) throw new ApiException("El rol ya existe." , (int)HttpStatusCode.Conflict);

            var rol = new ApplicationUserRol();
            rol.Name = registerRol.Name;
            
            IdentityResult result = await _roleManager.CreateAsync(rol);

            if (!result.Succeeded)
            {
                var errores = result.Errors.Select(e => e.Description).ToList();
                return new ApiResponse<string>
                {
                    Succeeded = false,
                    Message = "Error al crear el rol.",
                    Errors = errores
                };
            }

            return new ApiResponse<string>("Rol creado exitosamente.");
        }

        public async Task<ApiResponse<List<GetRolesUserDto>>> GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();

        
            if (roles == null || roles.Count == 0)
            {
                return new ApiResponse<List<GetRolesUserDto>>("No se encontraron roles.") { Succeeded = false };
            }

            var rolesDto = roles.Select(r => new GetRolesUserDto
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();

            return new ApiResponse<List<GetRolesUserDto>>(rolesDto);
        }

        public async Task<ApiResponse<string>> UpdateRol(AddOrUpdateRol dataRol)
        {
            var existingRole  = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == dataRol.Id);

            if (existingRole == null)
                return new ApiResponse<string>("El rol no existe.") { Succeeded = false };

            // Actualizar el nombre del rol
            existingRole.Name = dataRol.Name;
            var result = await _roleManager.UpdateAsync(existingRole);

            if (!result.Succeeded)
            {
                var errores = result.Errors.Select(e => e.Description).ToList();
                return new ApiResponse<string>
                {
                    Succeeded = false,
                    Message = "Error al actualizar el rol.",
                    Errors = errores
                };
            }

            return new ApiResponse<string>("Rol actualizado exitosamente.");
        }
    }
}
