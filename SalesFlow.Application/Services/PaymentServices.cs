using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Interfaces.Services;

namespace SalesFlow.Application.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IPaymentMethodRepository paymentMethodRepository;

        public PaymentServices(IPaymentMethodRepository paymentMethodRepository)
        {
            this.paymentMethodRepository = paymentMethodRepository;
        }

        public async Task<dynamic> GetPaymentsMethod()
        {
            return await paymentMethodRepository.GetAll();
        }
    }
}
