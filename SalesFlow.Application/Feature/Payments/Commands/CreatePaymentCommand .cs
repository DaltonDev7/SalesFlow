

using MediatR;
using SalesFlow.Application.Exception;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Wrappers;
using SalesFlow.Domain.Enums;
using EntityPayment = SalesFlow.Domain.Entities.Payments;


namespace SalesFlow.Application.Feature.Payments.Commands
{
    public class CreatePaymentCommand : IRequest<ApiResponse<string>>
    {
        public int IdOrder { get; set; }
        public decimal AmountPaid { get; set; }
        public int IdPaymentMethod { get; set; }
    }

    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, ApiResponse<string>>
    {
        private readonly IPaymentsRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;

        public CreatePaymentCommandHandler(IPaymentsRepository paymentRepository, IOrderRepository orderRepository)
        {
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
        }

        public async Task<ApiResponse<string>> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.Get(o => o.Id == command.IdOrder);

            if (order == null)
            {
                throw new ApiException($"Orden no encontrada.", 404);
            }

            if (command.AmountPaid <= 0)
            {
                throw new ApiException($"El monto pagado debe ser mayor a cero.", 500);
            }

            var payment = new EntityPayment
            {
                IdOrder = command.IdOrder,
                AmountPaid = command.AmountPaid,
                IdPaymentMethod = command.IdPaymentMethod,
                PaymentDate = DateTime.UtcNow
            };

            await _paymentRepository.InsertAndSave(payment);

            // 👉 Validar si el total pagado cubre la orden
            var totalPagado = await _paymentRepository.GetTotalPaidByOrder(command.IdOrder);

            if (totalPagado >= order.Total)
            {
                order.StatusOrder = OrderStatus.PAGADO; 
                await _orderRepository.UpdateAndSave(order);
            }
            else
            {
                var restante = order.Total - totalPagado;
                throw new ApiException($"Pago insuficiente. Faltan ${restante} para completar el total de la orden.", 400);
            }

            return new ApiResponse<string>("Pago registrado correctamente.");
        }
    }


}

