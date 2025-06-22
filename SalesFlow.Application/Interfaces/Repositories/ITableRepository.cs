
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Common;
using SalesFlow.Domain.Entities;

namespace SalesFlow.Application.Interfaces.Repositories
{
    public interface ITableRepository : IBaseRepository<Tables>
    {
        Task<List<GetTablesDto>> GetTables();
    }
}
