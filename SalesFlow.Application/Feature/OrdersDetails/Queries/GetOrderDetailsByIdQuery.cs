
using AutoMapper;
using MediatR;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Wrappers;

namespace SalesFlow.Application.Feature.OrdersDetails.Queries
{
    public class GetOrderDetailsByIdQuery : IRequest<ApiResponse<List<GetOrderDetailDto>>>
    {
        public int IdOrder { get; set; }

        public GetOrderDetailsByIdQuery(int idOrder)
        {
            IdOrder = idOrder;
        }
    }

    public class GetOrderDetailsByIdQueryHandler : IRequestHandler<GetOrderDetailsByIdQuery, ApiResponse<List<GetOrderDetailDto>>>
    {
        private readonly IOrderDetailRepository _orderDetailRepository; // Repositorio de los detalles del pedido

        public GetOrderDetailsByIdQueryHandler(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<ApiResponse<List<GetOrderDetailDto>>> Handle(GetOrderDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            // Llamamos al repositorio para obtener los detalles del pedido
            var orderDetails = await _orderDetailRepository.GetOrderDetailByOrder(request.IdOrder);

            // Retornar la respuesta en formato ApiResponse
            return new ApiResponse<List<GetOrderDetailDto>>(orderDetails);
        }
    }

}
