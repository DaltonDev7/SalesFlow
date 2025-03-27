
using AutoMapper;
using MediatR;
using SalesFlow.Application.Exception;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Wrappers;
using System.Net;

namespace SalesFlow.Application.Feature.Categories.Commands.UpdateCategories
{
    public class UpdateCategoriesCommand : IRequest<ApiResponse<string>>
    {
        public int Id { get; set; }
        public  string Name { get; set; }
        public  string Description { get; set; }
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoriesCommand, ApiResponse<string>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> Handle(UpdateCategoriesCommand request, CancellationToken cancellationToken)
        {
            var existingCategory = await _categoryRepository.Get(c => c.Id == request.Id);
            if (existingCategory == null)
                throw new ApiException("Category not found", (int)HttpStatusCode.NotFound);

            // Actualizar los valores
            existingCategory.Name = request.Name;
            existingCategory.Description = request.Description;

            // Guardar cambios en el repositorio
            await _categoryRepository.UpdateAndSave(existingCategory);

            return new ApiResponse<string>("Registro actualizado");
        }
    }

}
