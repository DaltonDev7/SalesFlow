

using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Common;
using SalesFlow.Domain.Entities;

namespace SalesFlow.Application.Interfaces.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {

        Task<List<GetOrdersDto>> GetOrders();
        Task<decimal> GetTodayPaymentsAsync();
        Task<decimal> GetTodayRevenueAsync();
    }
}
