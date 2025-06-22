using Microsoft.EntityFrameworkCore;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Domain.Entities;
using SalesFlow.Persistence.Context;
using SalesFlow.Persistence.Repositories.Generic;

namespace SalesFlow.Persistence.Repositories
{
    public class TableRepository : BaseRepository<Tables>, ITableRepository
    {
        public TableRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }


        public async Task<List<GetTablesDto>> GetTables()
        {
            return await _dbContext.Tables.Select(x => new GetTablesDto
            {
                Id = x.Id,
                Capacity = x.Capacity,
                Name = x.Name,
                StatusTable = x.StatusTable,
            }).ToListAsync();
        }


}
}
