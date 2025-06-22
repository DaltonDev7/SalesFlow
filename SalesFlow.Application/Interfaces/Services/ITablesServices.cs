using SalesFlow.Application.Dtos;
using SalesFlow.Application.Wrappers;

namespace SalesFlow.Application.Interfaces.Services
{
    public interface ITablesServices
    {
        Task<ApiResponse<List<GetTablesDto>>> GetTables();

        Task<ApiResponse<string>> Add(AddEditTablesDto dto);

        Task<ApiResponse<string>> Update(AddEditTablesDto dto);
    }
}
