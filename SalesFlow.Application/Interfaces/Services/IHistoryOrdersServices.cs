using SalesFlow.Application.Dtos;
using SalesFlow.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesFlow.Application.Interfaces.Services
{
    public interface IHistoryOrdersServices
    {
        Task<ApiResponse<List<GetHistoryOrdersDto>>> GetHistorial();

        Task<ApiResponse<string>> CreateHistorial(CreateHistoryOrdersDto data);

        Task<List<GetOrdersDto>> GetOrdersByCustomerId(int customerId);

    }
}
