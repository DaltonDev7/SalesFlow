using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Common;
using SalesFlow.Domain.Entities;

namespace SalesFlow.Application.Interfaces.Repositories
{
    public interface IPaymentsRepository : IBaseRepository<Payments>
    {
        Task<decimal> GetTotalPaidByOrder(int idOrder);
        Task<List<GetPaymentsDetail>> GetPaymentsDetail();
    }
}
