using AutoMapper;
using MediatR;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Wrappers;

namespace SalesFlow.Application.Feature.Customers.Queries
{
    public class GetAllCustomersQuery : IRequest<ApiResponse<IEnumerable<GetCustomersDto>>>
    {
    }

    public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, ApiResponse<IEnumerable<GetCustomersDto>>>
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _repository;

        public GetAllCustomersHandler(IMapper mapper, ICustomerRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<GetCustomersDto>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            // Obtener todos los clientes desde el repositorio
            var customers = await _repository.GetAll();

            // Mapear los clientes a DTO
            var customerDtos = _mapper.Map<IEnumerable<GetCustomersDto>>(customers);

            // Retornar la respuesta con los datos
            return new ApiResponse<IEnumerable<GetCustomersDto>>(customerDtos);
        }
    }


}
