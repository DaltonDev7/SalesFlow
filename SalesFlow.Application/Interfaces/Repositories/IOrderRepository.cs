

using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Common;
using SalesFlow.Domain.Entities;

namespace SalesFlow.Application.Interfaces.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {

        Task<SalesByMonthResponseDto> GetSalesByMonthAsync(int year, int month, bool onlyPaid = true);
        Task<SalesByDateResponseDto> GetSalesByDateAsync(DateTime date, bool onlyPaid = true);

        Task<GetOrderWithDetailsDto?> GetOrderWithDetailsById(int orderId);
        Task<List<GetOrderWithDetailsDto>> GetAllOrdersWithDetails();
        Task<List<GetOrdersDto>> GetOrders();
        Task<ReporteToday> GetTodayPaymentsAsync();
        Task<decimal> GetTodayRevenueAsync();
        Task<List<GetOrdersDto>> GetAllOrders();
        Task<List<GetOrdersDto>> GetOrdersByCustomerId(int customerId);

        //Task<List<CategorySalesDto>> GetTodaySalesByCategoryAsync();

        //Task<List<ProductSalesDto>> GetTodaySalesByProductAsync();

        Task<List<CategorySalesDto>> GetTodaySalesByCategoryAsync(DateTime? date = null);

        Task<List<ProductSalesDto>> GetTodaySalesByProductAsync(DateTime? date = null);
    }
}
