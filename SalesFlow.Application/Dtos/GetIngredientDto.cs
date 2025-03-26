

namespace SalesFlow.Application.Dtos
{
    public class GetIngredientDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public string UnitMeasurement { get; set; }

        public Boolean Available { get; set; }
    }
}
