
using Microsoft.EntityFrameworkCore;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Domain.Entities;
using SalesFlow.Domain.Enums;
using SalesFlow.Persistence.Context;
using SalesFlow.Persistence.Repositories.Generic;

namespace SalesFlow.Persistence.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<GetOrdersDto>> GetOrders()
        {

            return await _dbContext.Order.Select(x => new GetOrdersDto { 
              Id = x.Id,
              CustomerName = x.Customer.Name,
              DateOrder = x.DateOrder,
              EmployeName = x.User.Names + " " + x.User.LastNames,
              OrderType = x.OrderType,
              StatusOrder = Enum.GetName(typeof(OrderStatus), x.StatusOrder) ?? "",
              Total = x.Total,
            }).ToListAsync();

        }

    }
}
