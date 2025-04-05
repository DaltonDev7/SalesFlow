using MediatR;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Wrappers;

namespace SalesFlow.Application.Feature.Orders.Queries
{
    public class GetAllOrdersQuery : IRequest<ApiResponse<List<GetOrdersDto>>>
    {
      
    }

    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, ApiResponse<List<GetOrdersDto>>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetAllOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ApiResponse<List<GetOrdersDto>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            // Obtener los pedidos desde el repositorio
            var orders = await _orderRepository.GetOrders();


            // Retornar la respuesta API con los datos mapeados
            return new ApiResponse<List<GetOrdersDto>>(orders);
        }
    }


}
