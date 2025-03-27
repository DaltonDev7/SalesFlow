

using AutoMapper;
using MediatR;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Wrappers;

namespace SalesFlow.Application.Feature.Inventories.Queries
{
    public class GetAllInventoryQuery : IRequest<ApiResponse<IEnumerable<GetInventoryDto>>>
    {

    }
    public class GetAllProductsHandler : IRequestHandler<GetAllInventoryQuery, ApiResponse<IEnumerable<GetInventoryDto>>>
    {

        private readonly IMapper _mapper;
        private readonly IInventoryRepository _repository;

        public GetAllProductsHandler(IMapper mapper, IInventoryRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<GetInventoryDto>>> Handle(GetAllInventoryQuery request, CancellationToken cancellationToken)
        {

            var data = _mapper.Map<List<GetInventoryDto>>(await _repository.GetInvetoryData());
      
            // Retornar la respuesta API
            return new ApiResponse<IEnumerable<GetInventoryDto>>(data);
        }

    }

}
