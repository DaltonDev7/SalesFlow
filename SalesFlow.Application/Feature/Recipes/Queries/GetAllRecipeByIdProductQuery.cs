

using MediatR;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Wrappers;

namespace SalesFlow.Application.Feature.Recipes.Queries
{
    // Query
    public class GetAllRecipeByIdProductQuery : IRequest<ApiResponse<List<GetRecipesDto>>>
    {
        public int IdProduct { get; set; }
    }

    // Handler
    public class GetAllRecipeByIdProductQueryHandler : IRequestHandler<GetAllRecipeByIdProductQuery, ApiResponse<List<GetRecipesDto>>>
    {
        private readonly IRecipeRepository _recipeRepository;

        public GetAllRecipeByIdProductQueryHandler(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<ApiResponse<List<GetRecipesDto>>> Handle(GetAllRecipeByIdProductQuery request, CancellationToken cancellationToken)
        {
            // Aquí sí le pasas el IdProduct
            var recipes = await _recipeRepository.GetRecipesByIdProduct(request.IdProduct);

            return new ApiResponse<List<GetRecipesDto>>(recipes);
        }
    }


}
