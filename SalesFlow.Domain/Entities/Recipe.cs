

using SalesFlow.Domain.Common;

namespace SalesFlow.Domain.Entities
{
    public class Recipe : BaseEntity
    {
        public int Amount { get; set; }
        public int IdProduct { get; set; }
        public int IdIngredient { get; set; }

        public string UnitMeasurement { get; set; }

        public Product Product { get; set; }  // Relación con producto final
        public Product Ingredient { get; set; }  // Relación con ingrediente (también es un producto)

    }
}
