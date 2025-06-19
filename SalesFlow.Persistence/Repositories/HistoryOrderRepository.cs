using Microsoft.EntityFrameworkCore;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Domain.Entities;
using SalesFlow.Persistence.Context;
using SalesFlow.Persistence.Repositories.Generic;

namespace SalesFlow.Persistence.Repositories
{
    public class HistoryOrderRepository : BaseRepository<HistoryOrders>, IHistoryOrderRepository
    {
        public HistoryOrderRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<GetHistoryOrdersDto>> GetHistorial()
        {
            var response = await _dbContext.HistoryOrder.Select(x => new GetHistoryOrdersDto
            {
                NameCustomer = x.NameCustomer,
                Fecha = x.Fecha,
                IdOrder = x.IdOrder,
                MethodPayment = x.MethodPayment,
                OrderType = x.OrderType,
                Total = x.Total,
            }).ToListAsync();

            return response;
        }

    }
}
