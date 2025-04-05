using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Common;
using SalesFlow.Domain.Entities;


namespace SalesFlow.Application.Interfaces.Repositories
{
    public interface IOrderDetailRepository : IBaseRepository<OrderDetail>
    {

        Task<List<GetOrderDetailDto>> GetOrderDetailByOrder(int idOrder);
    }
}
