
using Microsoft.EntityFrameworkCore;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Domain.Entities;
using SalesFlow.Persistence.Context;
using SalesFlow.Persistence.Repositories.Generic;

namespace SalesFlow.Persistence.Repositories
{
    public class PaymentsRepository : BaseRepository<Payments>, IPaymentsRepository
    {
        public PaymentsRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<decimal> GetTotalPaidByOrder(int idOrder)
        {
            return await _dbContext.Payments
                .Where(p => p.IdOrder == idOrder)
                .SumAsync(p => p.AmountPaid);
        }

        public async Task<List<GetPaymentsDetail>> GetPaymentsDetail()
        {
            return await _dbContext.Payments.Select(p => new GetPaymentsDetail 
            { 
               Id = p.Id,
               AmountPaid = p.AmountPaid,
               IdOrder = p.IdOrder,
               PaymentDate = p.PaymentDate,
               CustomerName = p.Order.Customer.Name,
               EmployeName = p.Order.User.Names,
               OrderType = p.Order.OrderType
            }).ToListAsync();
        }
    }
}
