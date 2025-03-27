

using AutoMapper;
using MediatR;
using SalesFlow.Application.Exception;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Wrappers;
using System.Net;

namespace SalesFlow.Application.Feature.Inventories.Commands
{
    public class UpdateInventoryCommand : IRequest<ApiResponse<string>>
    {
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public int AvailableQuantity { get; set; }
        public string UnitMeasurement { get; set; }
    }

    public class UpdateInventoryCommandHandler : IRequestHandler<UpdateInventoryCommand, ApiResponse<string>>
    {
        private readonly IInventoryRepository _repository;

        public UpdateInventoryCommandHandler(IInventoryRepository repository, IMapper mapper)
        {
            _repository = repository;
        }

        public async Task<ApiResponse<string>> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
        {
            var dataUpdate = await _repository.Get(c => c.Id == request.Id);
            if (dataUpdate == null)
                throw new ApiException("Category not found", (int)HttpStatusCode.NotFound);

            // Actualizar los valores
            dataUpdate.IdProduct = request.IdProduct;
            dataUpdate.AvailableQuantity = request.AvailableQuantity;
            dataUpdate.UnitMeasurement = request.UnitMeasurement;

            // Guardar cambios en el repositorio
            await _repository.UpdateAndSave(dataUpdate);

            return new ApiResponse<string>("Registro actualizado");
        }
    }

}
