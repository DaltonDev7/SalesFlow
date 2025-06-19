using SalesFlow.Application.Dtos;
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

        public async Task<ApiResponse<ReporteToday>> GetTodayPaymentsAsync()
        {
            var data = await orderRepository.GetTodayPaymentsAsync();
            return new ApiResponse<ReporteToday>(data);
        }

        public async Task<ApiResponse<decimal>> GetTodayRevenueAsync()
        {
            var data = await orderRepository.GetTodayRevenueAsync();
            return new ApiResponse<decimal>(data);
        }

        //public async Task<ApiResponse<List<CategorySalesDto>>> GetTodaySalesByCategoryAsync()
        //{
        //    var data = await orderRepository.GetTodaySalesByCategoryAsync();
        //    return new ApiResponse<List<CategorySalesDto>>(data);
        //} 

        //public async Task<ApiResponse<List<ProductSalesDto>>> GetTodaySalesByProductAsync()
        //{
        //    var data = await orderRepository.GetTodaySalesByProductAsync();
        //    return new ApiResponse<List<ProductSalesDto>>(data);
        //}

        public async Task<ApiResponse<List<CategorySalesDto>>> GetSalesByCategoryAsync(DateTime? date = null)
        {
            var data = await orderRepository.GetTodaySalesByCategoryAsync(date);
            return new ApiResponse<List<CategorySalesDto>>(data);
        }

        public async Task<ApiResponse<List<ProductSalesDto>>> GetSalesByProductAsync(DateTime? date = null)
        {
            var data = await orderRepository.GetTodaySalesByProductAsync(date);
            return new ApiResponse<List<ProductSalesDto>>(data);
        }


    }
}
