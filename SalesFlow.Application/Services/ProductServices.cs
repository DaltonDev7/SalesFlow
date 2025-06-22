

using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Interfaces.Services;

namespace SalesFlow.Application.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository productRepository;

        public ProductServices(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<List<GetProductDto>> GetProductsByCategoryAsync(int categoryId)
        {
           return await productRepository.GetProductsByCategoryAsync(categoryId);
        }
    }
}
