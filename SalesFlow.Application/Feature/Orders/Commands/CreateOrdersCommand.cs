
using AutoMapper;
using MediatR;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Wrappers;
using SalesFlow.Domain.Entities;
using SalesFlow.Domain.Enums;

namespace SalesFlow.Application.Feature.Orders.Commands
{
    public class CreateOrdersCommand : IRequest<ApiResponse<string>>
    {
        public int IdCustomer { get; set; }
        public int IdEmploye { get; set; }
        public DateTime DateOrder { get; set; }
        public decimal Total { get; set; }
        public OrderStatus StatusOrder { get; set; } = OrderStatus.PENDIENTE;
        public string OrderType { get; set; }

        // Agregar una lista de detalles de la orden
        public List<CreateOrderDetailCommandDto> OrderDetails { get; set; } = new List<CreateOrderDetailCommandDto>();
    }

    public class CreateOrderDetailCommandDto
    {
        public int IdProduct { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
    }


    public class CreateOrderCommandHandler : IRequestHandler<CreateOrdersCommand, ApiResponse<string>>
    {
        private readonly IOrderRepository _repository;
        private readonly IOrderDetailRepository _orderDetailRepository; // Repositorio para los detalles de la orden
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(
            IOrderRepository repository,
            IOrderDetailRepository orderDetailRepository, // Inyección de dependencia del repositorio de detalles de la orden
            IMapper mapper)
        {
            _repository = repository;
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> Handle(CreateOrdersCommand command, CancellationToken cancellationToken)
        {
            var newOrder = _mapper.Map<Order>(command);
            await _repository.InsertAndSave(newOrder);

            // Mapeo y creación de los detalles de la orden
            foreach (var detail in command.OrderDetails)
            {
                var newDetail = new OrderDetail
                {
                    IdOrder = newOrder.Id,  // Asocia el detalle con la orden
                    IdProduct = detail.IdProduct,
                    Amount = detail.Amount,
                    UnitPrice = detail.UnitPrice,
                    SubTotal = detail.Amount * detail.UnitPrice
                };

                // Guardar cada detalle
                await _orderDetailRepository.InsertAndSave(newDetail);
            }


            return new ApiResponse<string>("Registro creado correctamente");
        }
    }
}
