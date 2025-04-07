using Microsoft.EntityFrameworkCore;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Domain.Entities;
using SalesFlow.Persistence.Context;
using SalesFlow.Persistence.Repositories.Generic;
using System.Linq.Expressions;

namespace SalesFlow.Persistence.Repositories
{
    public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> Exists(Expression<Func<Recipe, bool>> predicate)
        {
            return await _dbContext.Recipe.AnyAsync(predicate);
        }

        public async Task<List<GetRecipesDto>> GetRecipes()
        {
            return await _dbContext.Recipe
                .Select(r => new GetRecipesDto
                {
                    Id = r.Id,
                    ProductName = r.Product.Name,
                    IngredientName = r.Ingredient.Name,
                    Amount = r.Amount,
                    UnitMeasurement = r.UnitMeasurement
                })
                .ToListAsync();
        }

    }
}
