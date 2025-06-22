using SalesFlow.Application.Dtos;
using SalesFlow.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesFlow.Application.Interfaces.Services
{
    public interface IReservationsServices
    {
        Task<ApiResponse<List<GetReservationsDto>>> GetReservations();
        Task<ApiResponse<string>> Add(AddEditReservations dto);

        Task<ApiResponse<string>> Update(AddEditReservations dto);

    }
}
