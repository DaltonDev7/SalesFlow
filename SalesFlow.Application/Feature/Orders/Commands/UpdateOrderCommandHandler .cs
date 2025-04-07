

using AutoMapper;
using MediatR;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Wrappers;
using SalesFlow.Domain.Entities;
using SalesFlow.Domain.Enums;

namespace SalesFlow.Application.Feature.Orders.Commands
{
    public class UpdateOrderCommand : IRequest<ApiResponse<string>>
    {
        public int Id { get; set; } // ID del pedido a actualizar
        public int IdCustomer { get; set; }
        public int IdEmploye { get; set; }
        public DateTime DateOrder { get; set; }
        public decimal Total { get; set; }
        public OrderStatus StatusOrder { get; set; } = OrderStatus.PENDIENTE;
        public string OrderType { get; set; }
    }

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, ApiResponse<string>>
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            Order existingOrder = await _repository.Get(x => x.Id == command.Id);
            if (existingOrder == null)
            {
                return new ApiResponse<string>
                {
                    Message = "Pedido no encontrado",
                    Succeeded = false
                };
            }

            // Mapear propiedades actualizadas, pero conservando valores que no deben tocarse
            _mapper.Map(command, existingOrder);

            existingOrder.IdCustomer = command.IdCustomer;
            existingOrder.IdEmploye = command.IdEmploye;
            existingOrder.DateOrder = DateTime.UtcNow;
            existingOrder.Total = command.Total;
            existingOrder.StatusOrder = command.StatusOrder;
            existingOrder.OrderType = command.OrderType;
            existingOrder.LastModified = DateTime.UtcNow;

            await _repository.UpdateAndSave(existingOrder);

            return new ApiResponse<string>("Pedido actualizado correctamente");
        }
    }
}
