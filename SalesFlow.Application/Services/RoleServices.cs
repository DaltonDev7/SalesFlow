
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Dtos.Authentication;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Interfaces.Services;
using SalesFlow.Application.Wrappers;


namespace SalesFlow.Application.Services
{
    public class RoleServices : IRolesServices
    {

        public readonly IRoleRepository _roleRepository;

        public RoleServices(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<ApiResponse<bool>> AssignRoleToUser(int userId, int roleId)
        {
            return await _roleRepository.AssignRoleToUser(userId, roleId);
        }

        public async Task<ApiResponse<string>> CreateRol(AddOrUpdateRol registerRol)
        {
            return await _roleRepository.CreateRol(registerRol);
        }

        public async Task<ApiResponse<List<GetRolesUserDto>>> GetAllRoles()
        {
            return await _roleRepository.GetAllRoles();
        }

        public async Task<List<RolDto>> GetUserRolesAsync(int userId)
        {
           return await _roleRepository.GetUserRolesAsync(userId);
        }

        public async Task<ApiResponse<string>> UpdateRol(AddOrUpdateRol registerRol)
        {
           return await _roleRepository.UpdateRol(registerRol); 
        }
    }
}
