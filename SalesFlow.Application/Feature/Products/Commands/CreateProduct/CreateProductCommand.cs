

using AutoMapper;
using MediatR;
using SalesFlow.Application.Feature.Categories.Commands.CreateCategory;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Wrappers;
using SalesFlow.Domain.Entities;

namespace SalesFlow.Application.Feature.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<ApiResponse<int>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int IdCategory { get; set; }

        public int ProductType { get; set; }

        public Boolean IsIngredient { get; set; }
        public Boolean Available { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResponse<int>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<int>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var newProduct = _mapper.Map<Product>(command);
            await _repository.InsertAndSave(newProduct);

            return new ApiResponse<int>(newProduct.Id);
        }
    }


}
