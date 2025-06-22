
using Microsoft.EntityFrameworkCore;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Domain.Entities;
using SalesFlow.Domain.Enums;
using SalesFlow.Persistence.Context;
using SalesFlow.Persistence.Repositories.Generic;

namespace SalesFlow.Persistence.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }


        public async Task<List<GetProductDto>> GetProductsSimple()
        {
            // Obtén los productos de la base de datos
            var products = await _dbContext.Product
              .Where(p => p.ProductType == ProductTypeEnum.Simple) // Aplica el filtro en la consulta
              .Include(p => p.Category)               // Incluye la relación con Category
              .Include(p => p.Inventory)              // Incluye la relación con Inventory
              .Include(p => p.Recipes)                // Incluye la relación con Recipes
              .Include(p => p.OrderDetails)           // Incluye la relación con OrderDetails
              .ToListAsync();

            // Mapea los productos a GetProductDto y convierte el ProductType a su nombre
            var productDtos = products.Select(p => new GetProductDto
            {
                Available = p.Available,
                CategoryName = p.Category.Name,
                Name = p.Name,
                Description = p.Description,
                Id = p.Id,
                IdCategory = p.IdCategory,
                Price = p.Price,
                ProductType = (int?)p.ProductType,
                IsIngredient = (bool)p.IsIngredient
            }).ToList();

            return productDtos;
        }


        public async Task<List<GetProductDto>> GetProducts(Boolean isProduct)
        {
            // Obtén los productos de la base de datos
            var products = await _dbContext.Product
              .Where(p => p.IsIngredient == isProduct) // Aplica el filtro en la consulta
              .Include(p => p.Category)               // Incluye la relación con Category
              .Include(p => p.Inventory)              // Incluye la relación con Inventory
              .Include(p => p.Recipes)                // Incluye la relación con Recipes
              .Include(p => p.OrderDetails)           // Incluye la relación con OrderDetails
              .ToListAsync();

            // Mapea los productos a GetProductDto y convierte el ProductType a su nombre
            var productDtos = products.Select(p => new GetProductDto
            {
                Available = p.Available,
                CategoryName = p.Category.Name,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Description = p.Description,
                Id = p.Id,
                IdCategory = p.IdCategory,
                Price = p.Price,
                ProductType = (int?)p.ProductType,
                IsIngredient = (bool)p.IsIngredient
            }).ToList();

            return productDtos;
        }


       public async Task<List<GetProductDto>> GetProductsByCategoryAsync(int categoryId)
        {

            if(categoryId == 0)
            {
                return await _dbContext.Product
                .Where(p => p.IsIngredient == false)
                .Select(p => new GetProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    IdCategory = p.IdCategory
                })
                .ToListAsync();
            }
            else
            {

            var products = await _dbContext.Product
                .Where(p => p.IdCategory == categoryId && p.IsIngredient == false)
                .Select(p => new GetProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    IdCategory = p.IdCategory
                })
                .ToListAsync();

            return products;
            }

        }



    }
}
