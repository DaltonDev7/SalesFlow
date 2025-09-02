

namespace SalesFlow.Application.Dtos
{
    public class GetProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int IdCategory { get; set; }
        public int? IdPaymentMethod { get; set; }
        public int? ProductType { get; set; }
        public string? ImageUrl { get; set; }
        public string CategoryName { get; set; }

        public Boolean IsIngredient { get; set; }
        public Boolean Available { get; set; }
    }

    public class GetOrderWithDetailsDto : GetOrdersDto
    {
        public List<GetOrderDetailDto> OrderDetails { get; set; } = new();
    }
}
