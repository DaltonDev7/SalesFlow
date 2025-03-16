

using Azure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Wrappers;
using SalesFlow.Persistence.Context;

namespace SalesFlow.Application.Feature.Categories.Queries
{
    public class GetAllCategoriesQuery : IRequest<ApiResponse<IEnumerable<GetCategoryDto>>>
    {

    }

    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, ApiResponse<IEnumerable<GetCategoryDto>>>
    {
        private readonly ApplicationContext _context;

        public GetAllCategoriesHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<IEnumerable<GetCategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            // Consultar las categorías desde la base de datos
            var categories = await _context.Category
                .AsNoTracking() // Para mejorar el rendimiento si no necesitas hacer seguimiento de los cambios
                .ToListAsync(cancellationToken);

            // Mapear las categorías a DTOs
            var categoryDtos = categories.Select(c => new GetCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();

            // Retornar la respuesta API
            return new ApiResponse<IEnumerable<GetCategoryDto>>(categoryDtos);
        }
    }



}
