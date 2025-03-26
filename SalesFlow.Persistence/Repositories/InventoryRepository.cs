

using Microsoft.EntityFrameworkCore;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Domain.Entities;
using SalesFlow.Persistence.Context;
using SalesFlow.Persistence.Repositories.Generic;

namespace SalesFlow.Persistence.Repositories
{
    public class InventoryRepository : BaseRepository<Inventory>, IInventoryRepository
    {
        public InventoryRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<GetInventoryDto>> GetInvetoryData()
        {
            return await _dbContext.Inventory.Select(i => new GetInventoryDto
            {
                Id = i.Id,
                AvailableQuantity = i.AvailableQuantity,   
                IdProduct = i.IdProduct,   
                LastModified = i.LastModified,  
                ProductName = i.Product.Name,
                UnitMeasurement = i.UnitMeasurement
            }).ToListAsync();
        }
    }

}
