
using AutoMapper;
using MediatR;
using SalesFlow.Application.Exception;
using SalesFlow.Application.Feature.Categories.Commands.UpdateCategories;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Wrappers;
using System.Net;

namespace SalesFlow.Application.Feature.Products.Commands
{
    public class UpdateProductCommand : IRequest<ApiResponse<string>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int IdCategory { get; set; }
        public Boolean Available { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ApiResponse<string>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existingData = await _repository.Get(c => c.Id == request.Id);
            if (existingData == null)
                throw new ApiException("Product not found", (int)HttpStatusCode.NotFound);

            // Actualizar los valores
            existingData.Name = request.Name;
            existingData.Description = request.Description;
            existingData.Price = request.Price;
            existingData.Available = request.Available;
            existingData.ImageUrl = request.ImageUrl;
            existingData.IdCategory = request.IdCategory;

            // Guardar cambios en el repositorio
            await _repository.UpdateAndSave(existingData);

            return new ApiResponse<string>("Registro actualizado");
        }
    }
}
