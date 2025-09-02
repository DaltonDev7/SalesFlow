using SalesFlow.Application.Dtos;
using SalesFlow.Application.Wrappers;

namespace SalesFlow.Application.Interfaces.Services
{
    public interface IReporterServices
    {
        Task<ApiResponse<decimal>> GetTodayRevenueAsync();
        Task<ApiResponse<List<GetOrdersDto>>> GetOrders();
        Task<ApiResponse<ReporteToday>> GetTodayPaymentsAsync();
        Task<ApiResponse<SalesByDateResponseDto>> GetSalesByDateAsync(DateTime date, bool onlyPaid = true);
        Task<ApiResponse<List<CategorySalesDto>>> GetSalesByCategoryAsync(DateTime? date = null);

        Task<ApiResponse<List<ProductSalesDto>>> GetSalesByProductAsync(DateTime? date = null);

        Task<ApiResponse<GetOrderWithDetailsDto>> GetOrderWithDetailsById(int orderId);

        Task<ApiResponse<List<GetOrderWithDetailsDto>>> GetAllOrdersWithDetails();

        Task<ApiResponse<SalesByMonthResponseDto>> GetSalesByMonthAsync(int year, int month, bool onlyPaid = true);
    }
}
