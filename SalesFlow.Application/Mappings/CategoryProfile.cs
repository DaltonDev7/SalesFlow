
using AutoMapper;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Feature.Categories.Commands.CreateCategory;
using SalesFlow.Domain.Entities;

namespace SalesFlow.Application.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<Category, GetCategoryDto>()
             .ReverseMap()
             .ForMember(x => x.LastModified, opt => opt.Ignore())
             .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
             .ForMember(x => x.Created, opt => opt.Ignore())
             .ForMember(x => x.CreatedBy, opt => opt.Ignore());
        }
    }
}
