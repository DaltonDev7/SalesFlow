using AutoMapper;
using MediatR;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Wrappers;

namespace SalesFlow.Application.Feature.Customers.Commands
{
    public class UpdateCustomerCommand : IRequest<ApiResponse<string>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }

    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ApiResponse<string>>
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandHandler(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
        {
            // Buscar el cliente en la base de datos
            var customer = await _repository.Get(x => x.Id == command.Id);

            if (customer == null)
            {
                return new ApiResponse<string>()
                {
                    Message = "Cliente no encontrado",
                    Succeeded = false
                };
            }

            // Actualizar los datos del cliente
            customer.Name = command.Name;
            customer.PhoneNumber = command.PhoneNumber;
            customer.Address = command.Address;

            // Guardar los cambios
            await _repository.UpdateAndSave(customer);

            // Retornar la respuesta de éxito
            return new ApiResponse<string>("Cliente actualizado correctamente");
        }
    }



}
