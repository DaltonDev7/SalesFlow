

using AutoMapper;
using MediatR;
using SalesFlow.Application.Feature.Products.Commands.CreateProduct;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Wrappers;
using SalesFlow.Domain.Entities;

namespace SalesFlow.Application.Feature.Inventories.Commands
{
    public class CreateInventoryCommand : IRequest<ApiResponse<string>>
    {
        public int IdProduct { get; set; }
        public int AvailableQuantity { get; set; }
        public string UnitMeasurement { get; set; }
    }

    public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, ApiResponse<string>>
    {
        private readonly IInventoryRepository _repository;
        private readonly IMapper _mapper;

        public CreateInventoryCommandHandler(IInventoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> Handle(CreateInventoryCommand command, CancellationToken cancellationToken)
        {
            var newProduct = _mapper.Map<Inventory>(command);
            await _repository.InsertAndSave(newProduct);

            return new ApiResponse<string>("Registro creado correctamente.");
        }
    }
}
