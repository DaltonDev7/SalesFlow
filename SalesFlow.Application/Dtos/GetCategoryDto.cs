
namespace SalesFlow.Application.Dtos
{
    public class GetCategoryDto
    {
        public int Id { get; set; }
        public  string Name { get; set; }
        public  string Description { get; set; }
        public int? Status { get; set; }
    }
}
