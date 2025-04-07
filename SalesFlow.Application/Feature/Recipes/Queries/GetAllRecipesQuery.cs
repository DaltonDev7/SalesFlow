using MediatR;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Wrappers;

namespace SalesFlow.Application.Feature.Recipes.Queries
{
    public class GetAllRecipesQuery : IRequest<ApiResponse<List<GetRecipesDto>>>
    {
    }

    public class GetAllRecipesQueryHandler : IRequestHandler<GetAllRecipesQuery, ApiResponse<List<GetRecipesDto>>>
    {
        private readonly IRecipeRepository _recipeRepository;

        public GetAllRecipesQueryHandler(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<ApiResponse<List<GetRecipesDto>>> Handle(GetAllRecipesQuery request, CancellationToken cancellationToken)
        {
            var recipes = await _recipeRepository.GetRecipes();

            return new ApiResponse<List<GetRecipesDto>>(recipes);
        }
    }

}
