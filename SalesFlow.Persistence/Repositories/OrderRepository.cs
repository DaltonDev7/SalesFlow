
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

            var rawOrders = await _dbContext.Order
             .Select(x => new {
                 x.Id,
                 CustomerName = x.Customer.Name,
                 IdCustomer = x.Customer.Id,
                 x.DateOrder,
                 EmployeName = x.User.Names + " " + x.User.LastNames,
                 x.OrderType,
                 x.StatusOrder,
                 x.Total
             })
             .ToListAsync();

            var result = rawOrders.Select(x => new GetOrdersDto
            {
                Id = x.Id,
                CustomerName = x.CustomerName,
                IdCustomer = x.IdCustomer,
                DateOrder = x.DateOrder,
                EmployeName = x.EmployeName,
                OrderType = x.OrderType,
                StatusOrder = (int)x.StatusOrder,
                Total = x.Total
            }).ToList();

            return result;

        }


        public async Task<decimal> GetTodayRevenueAsync()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var totalSales = await _dbContext.Order
                .Where(o => o.Created >= today && o.Created < tomorrow && o.StatusOrder == OrderStatus.PAGADO)
                .SumAsync(o => (decimal?)o.Total) ?? 0;
            return totalSales;
        }

        public async Task<decimal> GetTodayPaymentsAsync()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var totalPayments = await _dbContext.Payments
                .Where(p => p.Created >= today && p.Created < tomorrow)
                .SumAsync(p => (decimal?)p.AmountPaid) ?? 0;

            return totalPayments;
        }



    }
}
