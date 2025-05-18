

using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Common;
using SalesFlow.Domain.Entities;
using System.Linq.Expressions;

namespace SalesFlow.Application.Interfaces.Repositories
{
    public interface IRecipeRepository : IBaseRepository<Recipe>
    {
        Task<bool> Exists(Expression<Func<Recipe, bool>> predicate);

        Task<List<GetRecipesDto>> GetRecipes();
        Task<List<GetRecipesDto>> GetRecipesByIdProduct(int IdProduct);

    }
}
