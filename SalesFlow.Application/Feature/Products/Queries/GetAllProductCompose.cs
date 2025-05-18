using AutoMapper;
using MediatR;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesFlow.Application.Feature.Products.Queries
{
    public class GetAllProductCompose : IRequest<ApiResponse<IEnumerable<GetProductDto>>>
    {

    }

    public class GetAllProductComposeHandler : IRequestHandler<GetAllProductCompose, ApiResponse<IEnumerable<GetProductDto>>>
    {

        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;

        public GetAllProductComposeHandler(IMapper mapper, IProductRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<GetProductDto>>> Handle(GetAllProductCompose request, CancellationToken cancellationToken)
        {

            var data = _mapper.Map<List<GetProductDto>>(await _repository.GetProducts(true));

            // Retornar la respuesta API
            return new ApiResponse<IEnumerable<GetProductDto>>(data);
        }

    }
}
