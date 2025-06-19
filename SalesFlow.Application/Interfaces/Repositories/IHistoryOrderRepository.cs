

using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Common;
using SalesFlow.Domain.Entities;

namespace SalesFlow.Application.Interfaces.Repositories
{
    public interface IHistoryOrderRepository: IBaseRepository<HistoryOrders>
    {
        Task<List<GetHistoryOrdersDto>> GetHistorial();
    }
}
