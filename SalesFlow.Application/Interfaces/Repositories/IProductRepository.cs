

using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Common;
using SalesFlow.Domain.Entities;

namespace SalesFlow.Application.Interfaces.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<List<GetProductDto>> GetProducts(Boolean isProduct);
        Task<List<GetProductDto>> GetProductsSimple();

        Task<List<GetProductDto>> GetProductsByCategoryAsync(int categoryId);
    }
}
