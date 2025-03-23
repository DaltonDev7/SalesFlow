

using AutoMapper;
using MediatR;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Exception;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Wrappers;
using System.Net;

namespace SalesFlow.Application.Feature.Categories.Queries
{
    public class GetAllCategoriesQuery : IRequest<ApiResponse<IEnumerable<GetCategoryDto>>>
    {

    }

    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, ApiResponse<IEnumerable<GetCategoryDto>>>
    {
 
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoriesHandler( IMapper mapper, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<GetCategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
           
            var categories = _mapper.Map<List<GetCategoryDto>>(await _categoryRepository.GetAll());
        
            // Retornar la respuesta API
            return new ApiResponse<IEnumerable<GetCategoryDto>>(categories);
        }
    }



}
