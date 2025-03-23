using AutoMapper;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Feature.Products.Commands.CreateProduct;
using SalesFlow.Domain.Entities;


namespace SalesFlow.Application.Mappings
{
    public class ProductProfile : Profile
    {

        public ProductProfile()
        {
            CreateMap<CreateProductCommand, Product>();
            CreateMap<Product, GetProductDto>()
               .ReverseMap()
               .ForMember(x => x.LastModified, opt => opt.Ignore())
               .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
               .ForMember(x => x.Created, opt => opt.Ignore())
               .ForMember(x => x.CreatedBy, opt => opt.Ignore());
        }
    }
}
