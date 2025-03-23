

using AutoMapper;
using MediatR;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Wrappers;

namespace SalesFlow.Application.Feature.Products.Queries
{
    public class GetAllProductQuery : IRequest<ApiResponse<IEnumerable<GetProductDto>>>
    {

    }

    public class GetAllProductsHandler : IRequestHandler<GetAllProductQuery, ApiResponse<IEnumerable<GetProductDto>>>
    {

        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;

        public GetAllProductsHandler(IMapper mapper, IProductRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<GetProductDto>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {

            var data = _mapper.Map<List<GetProductDto>>(await _repository.GetAll());
          
            // Retornar la respuesta API
            return new ApiResponse<IEnumerable<GetProductDto>>(data);
        }

    }
}
