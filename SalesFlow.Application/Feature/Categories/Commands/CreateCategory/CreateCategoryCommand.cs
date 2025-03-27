using AutoMapper;
using MediatR;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Wrappers;
using SalesFlow.Domain.Entities;


namespace SalesFlow.Application.Feature.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<ApiResponse<int>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ApiResponse<int>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<int>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {

            var newCategory = _mapper.Map<Category>(command);
            await _categoryRepository.InsertAndSave(newCategory);
        
            return new ApiResponse<int>(newCategory.Id);

        }
    }

}
