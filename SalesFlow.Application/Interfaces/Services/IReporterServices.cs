using SalesFlow.Application.Wrappers;

namespace SalesFlow.Application.Interfaces.Services
{
    public interface IReporterServices
    {
        Task<ApiResponse<decimal>> GetTodayRevenueAsync();

        Task<ApiResponse<decimal>> GetTodayPaymentsAsync();
    }
}
