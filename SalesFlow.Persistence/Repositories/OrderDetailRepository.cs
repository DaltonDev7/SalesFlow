

using Microsoft.EntityFrameworkCore;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Domain.Entities;
using SalesFlow.Persistence.Context;
using SalesFlow.Persistence.Repositories.Generic;

namespace SalesFlow.Persistence.Repositories
{
    public class OrderDetailRepository : BaseRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<GetOrderDetailDto>> GetOrderDetailByOrder(int idOrder)
        {
            return await _dbContext.OrderDetail.Select(x => new GetOrderDetailDto
            {
                Id = x.Id,
                IdOrder = x.IdOrder,
                IdProduct = x.Product.Id,
                Amount = x.Amount,
                UnitPrice = x.UnitPrice,
                SubTotal = x.SubTotal,
                ProductName = x.Product.Name,
            }).Where(x => x.IdOrder == idOrder).ToListAsync();
        }

    }
}
