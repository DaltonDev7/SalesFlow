
using AutoMapper;
using MediatR;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Wrappers;

namespace SalesFlow.Application.Feature.Products.Queries
{
    public class GetAllIngredientsQuery : IRequest<ApiResponse<IEnumerable<GetProductDto>>>
    {


    }
    public class GetAllIngredientsHandler : IRequestHandler<GetAllIngredientsQuery, ApiResponse<IEnumerable<GetProductDto>>>
    {

        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;

        public GetAllIngredientsHandler(IMapper mapper, IProductRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<GetProductDto>>> Handle(GetAllIngredientsQuery request, CancellationToken cancellationToken)
        {

            var data = _mapper.Map<List<GetProductDto>>(await _repository.GetProducts(true));

            // Retornar la respuesta API
            return new ApiResponse<IEnumerable<GetProductDto>>(data);
        }

    }
}
