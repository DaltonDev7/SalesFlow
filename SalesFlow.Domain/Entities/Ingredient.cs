

using SalesFlow.Domain.Common;

namespace SalesFlow.Domain.Entities
{
    public class Ingredient : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public string UnitMeasurement { get; set; }

        public Boolean Available { get; set; }

        public ICollection<Recipe> Recipes { get; set; }

    }
}
