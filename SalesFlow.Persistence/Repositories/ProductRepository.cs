
using Microsoft.EntityFrameworkCore;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Domain.Entities;
using SalesFlow.Persistence.Context;
using SalesFlow.Persistence.Repositories.Generic;

namespace SalesFlow.Persistence.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }


        public async Task<List<GetProductDto>> GetProducts(Boolean isProduct)
        {
            return await  _dbContext.Product.Select(p => new GetProductDto
            {
                Available = p.Available,
                CategoryName = p.Category.Name,
                Name = p.Name,
                Description = p.Description,
                Id = p.Id,
                IdCategory = p.IdCategory,  
                Price = p.Price,
                IsIngredient = (bool)p.IsIngredient
            }).Where(p => p.IsIngredient == isProduct).ToListAsync();
        }
    }
}
