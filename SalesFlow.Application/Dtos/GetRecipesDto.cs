
namespace SalesFlow.Application.Dtos
{
    public class GetRecipesDto
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string ProductName { get; set; }
        public int IdProduct { get; set; }
        public int IdIngredient { get; set; }
        public string IngredientName { get; set; }
        public string UnitMeasurement { get; set; }

    }
}
