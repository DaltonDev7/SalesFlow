using SalesFlow.Application.Dtos.Authentication;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Interfaces.Services;
using SalesFlow.Application.Wrappers;

namespace SalesFlow.Application.Services
{
    public class ReporterServices : IReporterServices
    {
        private readonly IOrderRepository orderRepository;

        public ReporterServices(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<ApiResponse<decimal>> GetTodayPaymentsAsync()
        {
            var data = await orderRepository.GetTodayPaymentsAsync();
            return new ApiResponse<decimal>(data);
        }

        public async Task<ApiResponse<decimal>> GetTodayRevenueAsync()
        {
            var data = await orderRepository.GetTodayRevenueAsync();
            return new ApiResponse<decimal>(data);
        }

    }
}
