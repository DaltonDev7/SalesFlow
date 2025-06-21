
using AutoMapper;
using MediatR;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Wrappers;
using SalesFlow.Domain.Entities;

namespace SalesFlow.Application.Feature.Customers.Commands
{
    public class CreateCustomerCommand : IRequest<ApiResponse<string>>
    {
        public string Email { get; set; }

        public string Password { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ApiResponse<string>>
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            // Mapeamos el comando a la entidad Customer
            var customer = _mapper.Map<Customer>(command);

            // Guardamos el cliente en el repositorio
            await _repository.InsertAndSave(customer);

            // Retornamos una respuesta de éxito
            return new ApiResponse<string>("Cliente creado correctamente");
        }
    }

}
