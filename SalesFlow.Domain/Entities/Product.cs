

using SalesFlow.Domain.Common;

namespace SalesFlow.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int IdCategory { get; set; }
        public Boolean Available { get; set; }
        public string? ImageUrl { get; set; }

        public Boolean? IsIngredient { get; set; }

        public Category Category { get; set; }

        public Inventory Inventory { get; set; }

        public ICollection<Recipe> Recipes { get; set; }

        // Relación con OrderDetails
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
