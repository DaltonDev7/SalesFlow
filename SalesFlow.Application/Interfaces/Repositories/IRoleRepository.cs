

using SalesFlow.Application.Dtos.Authentication;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Wrappers;

namespace SalesFlow.Application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<ApiResponse<string>> CreateRol(AddOrUpdateRol registerRol);

        Task<ApiResponse<string>> UpdateRol(AddOrUpdateRol registerRol);

        Task<ApiResponse<bool>> AssignRoleToUser(int userId, int roleId);

        Task<ApiResponse<List<GetRolesUserDto>>> GetAllRoles();


        Task<List<RolDto>> GetUserRolesAsync(int userId);
    }
}
