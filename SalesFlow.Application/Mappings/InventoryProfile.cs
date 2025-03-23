

using AutoMapper;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Feature.Categories.Commands.CreateCategory;
using SalesFlow.Application.Feature.Inventories.Commands;
using SalesFlow.Domain.Entities;

namespace SalesFlow.Application.Mappings
{
    public class InventoryProfile : Profile
    {
        public InventoryProfile()
        {
            CreateMap<CreateInventoryCommand, Inventory>();
            CreateMap<Inventory, GetInventoryDto>()
              .ReverseMap()
              .ForMember(x => x.LastModified, opt => opt.Ignore())
              .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
              .ForMember(x => x.Created, opt => opt.Ignore())
              .ForMember(x => x.CreatedBy, opt => opt.Ignore());
        }
    }
}
