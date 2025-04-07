

using MediatR;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Wrappers;
using SalesFlow.Domain.Entities;

namespace SalesFlow.Application.Feature.Recipes.Commands
{
    public class CreateRecipesCommand : IRequest<ApiResponse<string>>
    {
        public int Amount { get; set; }
        public int IdProduct { get; set; }
        public int IdIngredient { get; set; }
        public string UnitMeasurement { get; set; }
    }

    public class CreateRecipesCommandHandler : IRequestHandler<CreateRecipesCommand, ApiResponse<string>>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IProductRepository _productRepository;

        public CreateRecipesCommandHandler(
            IRecipeRepository recipeRepository,
            IProductRepository productRepository)
        {
            _recipeRepository = recipeRepository;
            _productRepository = productRepository;
        }

        public async Task<ApiResponse<string>> Handle(CreateRecipesCommand request, CancellationToken cancellationToken)
        {
            // Validar existencia del producto final
            var product = await _productRepository.Get(p => p.Id == request.IdProduct);
            if (product == null)
            {
                return new ApiResponse<string>
                {
                    Succeeded = false,
                    Message = "El producto final no existe."
                };
            }

            // Validar existencia del ingrediente
            var ingredient = await _productRepository.Get(p => p.Id == request.IdIngredient);
            if (ingredient == null)
            {
                return new ApiResponse<string>
                {
                    Succeeded = false,
                    Message = "El ingrediente no existe."
                };
            }

            // Validar que no se duplique la receta
            var exists = await _recipeRepository.Exists(r =>
                r.IdProduct == request.IdProduct &&
                r.IdIngredient == request.IdIngredient);

            if (exists)
            {
                return new ApiResponse<string>
                {
                    Succeeded = false,
                    Message = "Este ingrediente ya está registrado para esta receta."
                };
            }

            // Crear receta
            var recipe = new Recipe
            {
                IdProduct = request.IdProduct,
                IdIngredient = request.IdIngredient,
                Amount = request.Amount,
                UnitMeasurement = request.UnitMeasurement
            };

            await _recipeRepository.InsertAndSave(recipe);

            return new ApiResponse<string>("Receta registrada correctamente.");
        }
    }



}
