

using MediatR;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Wrappers;
using SalesFlow.Domain.Enums;

namespace SalesFlow.Application.Feature.Orders.Commands
{
    public class UpdateOrderStatusCommand : IRequest<ApiResponse<string>>
    {
        public int OrderId { get; set; }
        public OrderStatus NewStatus { get; set; }
    }

    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, ApiResponse<string>>
    {
        private readonly IOrderRepository _repository;

        public UpdateOrderStatusCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse<string>> Handle(UpdateOrderStatusCommand command, CancellationToken cancellationToken)
        {
            var order = await _repository.Get(x => x.Id == command.OrderId);
            if (order == null)
                return new ApiResponse<string>()
                {
                    Message = "Pedido no encontrado",
                    Succeeded = false,
                };

            if (order.StatusOrder == OrderStatus.CANCELADO)
            {
                return new ApiResponse<string>
                {
                    Message = "No se puede actualizar un pedido cancelado",
                    Succeeded = false
                };
            }

            order.StatusOrder = command.NewStatus;
            order.LastModified = DateTime.UtcNow;

            await _repository.UpdateAndSave(order);

            return new ApiResponse<string>("Estado del pedido actualizado correctamente");
        }
    }

}
