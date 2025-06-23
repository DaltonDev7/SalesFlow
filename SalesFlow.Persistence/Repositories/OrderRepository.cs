
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
                .OrderByDescending(x => x.DateOrder) // 🔁 Ordena por fecha descendente
                .Select(x => new {
                    x.Id,
                    CustomerName = x.Customer.Names,
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

        public async Task<ReporteToday> GetTodayPaymentsAsync()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var paymentsToday = _dbContext.Payments
                .Where(p => p.Created >= today && p.Created < tomorrow);

            var totalPayments = await paymentsToday.SumAsync(p => (decimal?)p.AmountPaid) ?? 0;
            var paymentCount = await paymentsToday.CountAsync();

            return new ReporteToday
            {
                totalPayments = totalPayments,
                paymentCount = paymentCount
            };
        }

        public async Task<List<CategorySalesDto>> GetTodaySalesByCategoryAsync(DateTime? date = null)
        {
            var targetDate = date?.Date ?? DateTime.Today;
            var nextDate = targetDate.AddDays(1);

            return await _dbContext.OrderDetail
                .Where(od => od.Order.DateOrder >= targetDate && od.Order.DateOrder < nextDate)
                .GroupBy(od => new
                {
                    od.Product.Category.Id,
                    od.Product.Category.Name
                })
                .Select(g => new CategorySalesDto
                {
                    CategoryId = g.Key.Id,
                    CategoryName = g.Key.Name,
                    TotalSales = g.Sum(x => x.SubTotal),
                    TotalItemsSold = g.Sum(x => x.Amount)
                })
                .OrderByDescending(x => x.TotalSales)
                .ToListAsync();
        }

        public async Task<List<ProductSalesDto>> GetTodaySalesByProductAsync(DateTime? date = null)
        {
            var targetDate = date?.Date ?? DateTime.Today;
            var nextDate = targetDate.AddDays(1);

            return await _dbContext.OrderDetail
                .Where(od => od.Order.DateOrder >= targetDate && od.Order.DateOrder < nextDate)
                .GroupBy(od => od.Product.Name)
                .Select(g => new ProductSalesDto
                {
                    ProductName = g.Key,
                    CantidadVendidas = g.Sum(x => x.Amount)
                })
                .OrderByDescending(x => x.CantidadVendidas)
                .ToListAsync();
        }







    }
}
