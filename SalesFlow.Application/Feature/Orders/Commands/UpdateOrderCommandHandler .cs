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
        public OrderStatus StatusOrder { get; set; }
        public string OrderType { get; set; }

        public List<CreateOrderDetailCommandDto> OrderDetails { get; set; } = new List<CreateOrderDetailCommandDto>();
    }

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, ApiResponse<string>>
    {
        private readonly IOrderRepository _repository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(
            IOrderRepository repository,
            IOrderDetailRepository orderDetailRepository,
            IMapper mapper)
        {
            _repository = repository;
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            // Obtener la orden existente
            Order existingOrder = await _repository.Get(x => x.Id == command.Id);
            if (existingOrder == null)
            {
                return new ApiResponse<string>
                {
                    Message = "Pedido no encontrado",
                    Succeeded = false
                };
            }

            // Actualizar propiedades básicas
            existingOrder.IdCustomer = command.IdCustomer;
            existingOrder.IdEmploye = command.IdEmploye;
            existingOrder.DateOrder = DateTime.UtcNow;
            existingOrder.StatusOrder = command.StatusOrder;
            existingOrder.OrderType = command.OrderType;
            existingOrder.LastModified = DateTime.UtcNow;

            // Guardar cambios preliminares en la orden
            await _repository.UpdateAndSave(existingOrder);

            // 🔁 Actualizar detalles de la orden
            var existingDetails = await _orderDetailRepository.GetAll(x => x.IdOrder == existingOrder.Id);
            var newDetails = command.OrderDetails;

            // 1. Eliminar los detalles que ya no están
            foreach (var existingDetail in existingDetails)
            {
                var stillExists = newDetails.Any(d => d.IdProduct == existingDetail.IdProduct);
                if (!stillExists)
                {
                    await _orderDetailRepository.Delete(existingDetail.Id);
                }
            }

            // 2. Agregar o actualizar los detalles
            foreach (var newDetail in newDetails)
            {
                var existingDetail = existingDetails.FirstOrDefault(d => d.IdProduct == newDetail.IdProduct);

                if (existingDetail != null)
                {
                    existingDetail.Amount = newDetail.Amount;
                    existingDetail.UnitPrice = newDetail.UnitPrice;
                    existingDetail.SubTotal = newDetail.Amount * newDetail.UnitPrice;
                    await _orderDetailRepository.Update(existingDetail);
                }
                else
                {
                    var detailToAdd = new OrderDetail
                    {
                        IdOrder = existingOrder.Id,
                        IdProduct = newDetail.IdProduct,
                        Amount = newDetail.Amount,
                        UnitPrice = newDetail.UnitPrice,
                        SubTotal = newDetail.Amount * newDetail.UnitPrice
                    };
                    await _orderDetailRepository.Insert(detailToAdd);
                }
            }

            // 3. Guardar cambios en los detalles
            await _orderDetailRepository.SaveChangesAsync();

            // 4. Calcular y actualizar el total de la orden
            var updatedDetails = await _orderDetailRepository.GetAll(x => x.IdOrder == existingOrder.Id);
            var totalOrder = updatedDetails.Sum(d => d.SubTotal);

            existingOrder.Total = totalOrder;
            await _repository.UpdateAndSave(existingOrder);

            return new ApiResponse<string>("Pedido actualizado correctamente");
        }
    }
}