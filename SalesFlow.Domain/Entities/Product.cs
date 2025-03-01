

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
    }
}
