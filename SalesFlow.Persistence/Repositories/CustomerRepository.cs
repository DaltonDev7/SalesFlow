

using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Domain.Entities;
using SalesFlow.Persistence.Context;
using SalesFlow.Persistence.Repositories.Generic;

namespace SalesFlow.Persistence.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
