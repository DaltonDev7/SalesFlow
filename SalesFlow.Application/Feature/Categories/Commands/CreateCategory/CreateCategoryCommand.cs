

using Azure;
using MediatR;
using SalesFlow.Application.Wrappers;
using SalesFlow.Domain.Entities;
using SalesFlow.Persistence.Context;

namespace SalesFlow.Application.Feature.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<ApiResponse<int>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ApiResponse<int>>
    {
        private readonly ApplicationContext _context;

        public CreateCategoryCommandHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var newCategory = new Category { Name = request.Name, Description = request.Description };
            _context.Category.Add(newCategory);
            await _context.SaveChangesAsync();

            return new ApiResponse<int>(newCategory.Id);
        }
    }

}
