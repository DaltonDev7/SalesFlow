using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Interfaces.Services;
using SalesFlow.Application.Wrappers;
using SalesFlow.Domain.Entities;

namespace SalesFlow.Application.Services
{
    public class HistoryOrdersServices : IHistoryOrdersServices
    {
        private readonly IHistoryOrderRepository repository;
        private readonly IOrderRepository _orderRepository;

        public HistoryOrdersServices(IHistoryOrderRepository repository, IOrderRepository orderRepository)
        {
            this.repository = repository;
            _orderRepository = orderRepository;
        }

        public async Task<ApiResponse<List<GetHistoryOrdersDto>>> GetHistorial()
        {
            var response =  await repository.GetHistorial();
            return new ApiResponse<List<GetHistoryOrdersDto>>(response);
        }
        public async Task<ApiResponse<string>> CreateHistorial(CreateHistoryOrdersDto data)
        {

            var history = new HistoryOrders();
            history.Total = data.Total;
            history.IdOrder = data.IdOrder;
            history.Fecha = data.Fecha; 
            history.NameCustomer = data.NameCustomer;   
            history.MethodPayment = data.MethodPayment;
            history.OrderType = data.OrderType;
            history.Fecha = DateTime.Now;

            await repository.InsertAndSave(history);

            await repository.SaveChangesAsync();

            return new ApiResponse<string>("Orden registrada correctamente");
        }


        public async Task<List<GetOrdersDto>> GetOrdersByCustomerId(int customerId)
        {
            return await _orderRepository.GetOrdersByCustomerId(customerId);
        }




    }
}
