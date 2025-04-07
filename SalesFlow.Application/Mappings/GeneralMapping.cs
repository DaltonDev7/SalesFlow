using AutoMapper;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Feature.Customers.Commands;
using SalesFlow.Application.Feature.Orders.Commands;
using SalesFlow.Domain.Entities;


namespace SalesFlow.Application.Mappings
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            //CreateMap<CreateOrdersCommand, Order>();
            CreateMap<Order, GetOrdersDto>()
             .ReverseMap()
             .ForMember(x => x.LastModified, opt => opt.Ignore())
             .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
             .ForMember(x => x.Created, opt => opt.Ignore())
             .ForMember(x => x.CreatedBy, opt => opt.Ignore());



            CreateMap<CreateCustomerCommand, Customer>();
            CreateMap<Customer, GetCustomersDto>()
              .ReverseMap()
              .ForMember(x => x.LastModified, opt => opt.Ignore())
              .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
              .ForMember(x => x.Created, opt => opt.Ignore())
              .ForMember(x => x.CreatedBy, opt => opt.Ignore());
        }
    }
}
