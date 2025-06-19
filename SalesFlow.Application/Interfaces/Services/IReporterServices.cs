using SalesFlow.Application.Dtos;
using SalesFlow.Application.Wrappers;

namespace SalesFlow.Application.Interfaces.Services
{
    public interface IReporterServices
    {
        Task<ApiResponse<decimal>> GetTodayRevenueAsync();

        Task<ApiResponse<ReporteToday>> GetTodayPaymentsAsync();

        Task<ApiResponse<List<CategorySalesDto>>> GetSalesByCategoryAsync(DateTime? date = null);

        Task<ApiResponse<List<ProductSalesDto>>> GetSalesByProductAsync(DateTime? date = null);
    }
}
