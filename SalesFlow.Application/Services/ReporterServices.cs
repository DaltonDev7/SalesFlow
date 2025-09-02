using SalesFlow.Application.Dtos;
using SalesFlow.Application.Dtos.Authentication;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Interfaces.Services;
using SalesFlow.Application.Wrappers;
using SalesFlow.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task<ApiResponse<SalesByDateResponseDto>> GetSalesByDateAsync(DateTime date, bool onlyPaid = true)
        {
            var data = await orderRepository.GetSalesByDateAsync(date, onlyPaid);
            return new ApiResponse<SalesByDateResponseDto>(data);
        }

        public async Task<ApiResponse<SalesByMonthResponseDto>> GetSalesByMonthAsync(int year, int month, bool onlyPaid = true)
        {
            var data = await orderRepository.GetSalesByMonthAsync(year, month, onlyPaid);
            return new ApiResponse<SalesByMonthResponseDto>(data);
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


        public async Task<ApiResponse<List<GetOrdersDto>>> GetOrders()
        {
            var data = await orderRepository.GetAllOrders();
            return new ApiResponse<List<GetOrdersDto>>(data);
        }

        public async Task<ApiResponse<GetOrderWithDetailsDto>> GetOrderWithDetailsById(int orderId)
        {
            var data = await orderRepository.GetOrderWithDetailsById(orderId);
            return new ApiResponse<GetOrderWithDetailsDto>(data);
        }

        public async Task<ApiResponse<List<GetOrderWithDetailsDto>>> GetAllOrdersWithDetails()
        {
            var data = await orderRepository.GetAllOrdersWithDetails();
            return new ApiResponse<List<GetOrderWithDetailsDto>>(data);
        }
    }
}
